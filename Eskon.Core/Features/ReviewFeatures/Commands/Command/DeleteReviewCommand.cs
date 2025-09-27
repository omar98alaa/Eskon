using Eskon.Core.Response;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;

namespace Eskon.Core.Features.ReviewFeatures.Commands.Command
{
    public record DeleteReviewCommand(Guid customerId, Guid reviewId) : IRequest<Response<ReviewReadDTO>>;
}
