using Eskon.Core.Response;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;

namespace Eskon.Core.Features.ReviewFeatures.Commands.Command
{
    public record EditReviewCommand(Guid customerId, Guid reviewId, ReviewWriteDTO reviewWriteDTO) : IRequest<Response<ReviewReadDTO>>;
}
