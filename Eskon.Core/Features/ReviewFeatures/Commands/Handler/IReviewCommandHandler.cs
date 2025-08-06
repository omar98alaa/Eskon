using Eskon.Core.Features.ReviewFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;

namespace Eskon.Core.Features.ReviewFeatures.Commands.Handler
{
    public interface IReviewCommandHandler : IRequestHandler<AddReviewCommand, Response<ReviewReadDTO>>,
                                             IRequestHandler<EditReviewCommand, Response<ReviewReadDTO>>,
                                             IRequestHandler<DeleteReviewCommand, Response<ReviewReadDTO>>;
}
