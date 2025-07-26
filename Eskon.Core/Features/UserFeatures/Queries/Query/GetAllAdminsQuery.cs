using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Queries.Query
{
    public record GetAllAdminsQuery(int pageNumber, int itemsPerPage) : IRequest<Response<Paginated<AdminsReadDTO>>>;
}
