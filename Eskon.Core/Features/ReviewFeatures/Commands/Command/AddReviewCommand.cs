using Eskon.Core.Response;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;

namespace Eskon.Core.Features.ReviewFeatures.Commands.Command
{
    public record AddReviewCommand(Guid customerId, Guid bookingId, ReviewWriteDTO reviewWriteDTO) : IRequest<Response<ReviewReadDTO>>;
}
