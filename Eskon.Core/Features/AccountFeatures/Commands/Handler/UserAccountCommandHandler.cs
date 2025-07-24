using AutoMapper;
using Eskon.Core.Features.AccountFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.AccountFeatures.Commands.Handler
{
    public class UserAccountCommandHandler : ResponseHandler, IUserAccountCommandHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constructors
        public UserAccountCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Add User Account Command Handler
        public async Task<Response<UserReadDto?>> Handle(AddUserAccountCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.UserRegisterDto);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.UserRegisterDto, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<UserReadDto?>(internalErrorMessages);
            }

            var userToAdd = _mapper.Map<User>(request.UserRegisterDto);
            var result = await _userManager.CreateAsync(userToAdd, request.UserRegisterDto.Password);
            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<UserReadDto?>(dbErrorMessages);
            }
            await _userManager.AddToRoleAsync(userToAdd, "Customer");
            //var userFromDb = await _userService.GetUserByEmailAsync(request.UserRegisterDto.Email);
            var userFromDb = await _userManager.FindByEmailAsync(request.UserRegisterDto.Email);
            return Created(_mapper.Map<UserReadDto>(userFromDb));

        }
        #endregion

        #region Sign In User Command Handler
        public async Task<Response<TokenResponseDto>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserSignInDto.Email);

            if (user == null)
            {
                return BadRequest<TokenResponseDto>("Incorrect email or password.");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.UserSignInDto.Password, false);

            if (!signInResult.Succeeded)
            {
                return BadRequest<TokenResponseDto>("Incorrect email or password");
            }
            var oldToken = await _serviceUnitOfWork.RefreshTokenService.GetTokenByUserIdAsync(user.Id);
            if (oldToken != null)
            {
                oldToken.IsRevoked = true;
                //await _serviceUnitOfWork.RefreshTokenService.SaveChangesAsync();
            }
            var userManagerClaims = await _userManager.GetClaimsAsync(user);
            var userManagerRoles = await _userManager.GetRolesAsync(user);

            var accessToken = _serviceUnitOfWork.AuthenticationService.GenerateJWTTokenAsync(user, userManagerRoles, userManagerClaims);

            var refreshToken = _serviceUnitOfWork.AuthenticationService.GenerateRefreshToken();
            var userRefreshToken = new UserRefreshToken
            {
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            await _serviceUnitOfWork.RefreshTokenService.AddNewRefreshTokenForUserAsync(userRefreshToken);

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        #endregion

        #region Sign Out User Command Handler
        public async Task<Response<string>> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            var stored = await _serviceUnitOfWork.RefreshTokenService.GetStoredTokenAsync(request.CurrentRefreshToken.CurrentRefreshToken);
            if (stored != null)
            {
                stored.IsRevoked = true;
                //await _serviceUnitOfWork.RefreshTokenService.SaveChangesAsync();
            }
            else
            {
                // ToDo
                // track suspicious activity here
                // for example:
                // _logger.LogWarning("Unknown or already revoked refresh token during logout.");

            }

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success("Signed out");
        }
        #endregion
    }
}
