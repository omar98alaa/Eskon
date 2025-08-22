using AutoMapper;
using Eskon.Core.Features.ReviewFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.ReviewDTOs;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.ReviewFeatures.Commands.Handler
{
    public class ReviewCommandHandler : ResponseHandler, IReviewCommandHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructors
        public ReviewCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        #endregion

        #region Handlers
        public async Task<Response<ReviewReadDTO>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);

            // Check booking null
            if (booking == null)
            {
                return NotFound<ReviewReadDTO>("Booking does not exist");
            }

            // Check same customer
            if (booking.CustomerId != request.customerId)
            {
                return Forbidden<ReviewReadDTO>();
            }

            // Check valid booking
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (!booking.IsPayed || booking.EndDate >= today)
            {
                return BadRequest<ReviewReadDTO>("Cannot add review. Invalid booking");
            }

            // Create new booking
            var review = _mapper.Map<Review>(request.reviewWriteDTO);
            review.CustomerId = request.customerId;
            review.PropertyId = booking.PropertyId;

            // Update property rating
            var property = booking.Property;
            property.AverageRating = ((property.AverageRating * property.TimesRated) + review.Rating) / ++(property.TimesRated);

            await _serviceUnitOfWork.ReviewService.CreatePropertyReviewAsync(review);
            await _serviceUnitOfWork.PropertyService.UpdatePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();

            var reviewReadDTO = _mapper.Map<ReviewReadDTO>(review);

            return Created(reviewReadDTO, "New review added successfully");
        }

        public async Task<Response<ReviewReadDTO>> Handle(EditReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _serviceUnitOfWork.ReviewService.GetReviewByIdAsync(request.reviewId);
            
            // Check review exists
            if (review == null)
            {
                return NotFound<ReviewReadDTO>("Review does not exist");
            }

            // Check same customer
            if (review.CustomerId != request.customerId)
            {
                return Forbidden<ReviewReadDTO>();
            }

            // Update property rating
            var property = review.Property;
            property.AverageRating = ((property.AverageRating * property.TimesRated) - review.Rating + request.reviewWriteDTO.Rating) / (property.TimesRated);

            // Update rating and content
            review.Rating = request.reviewWriteDTO.Rating;
            review.Content = request.reviewWriteDTO.Content;

            await _serviceUnitOfWork.ReviewService.UpdatePropertyReviewAsync(review);
            await _serviceUnitOfWork.PropertyService.UpdatePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();

            var reviewReadDTO = _mapper.Map<ReviewReadDTO>(review);

            return Success(reviewReadDTO, message: "Review updated successfully");
        }

        public async Task<Response<ReviewReadDTO>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _serviceUnitOfWork.ReviewService.GetReviewByIdAsync(request.reviewId);

            // Check review exists
            if (review == null)
            {
                return NotFound<ReviewReadDTO>("Review does not exist");
            }

            // Check same customer
            if (review.CustomerId != request.customerId)
            {
                return Forbidden<ReviewReadDTO>();
            }

            // Update property rating
            var property = review.Property;
            property.AverageRating = ((property.AverageRating * property.TimesRated) - review.Rating) / --(property.TimesRated);

            await _serviceUnitOfWork.ReviewService.DeletePropertyReviewAsync(review);
            await _serviceUnitOfWork.PropertyService.UpdatePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();

            var reviewReadDTO = _mapper.Map<ReviewReadDTO>(review);

            return Success(reviewReadDTO, message: "Review deleted successfully");
        }
        #endregion
    }
}
