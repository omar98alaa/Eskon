using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Handler
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessageDto>, IRequestHandler<MarkMessagesAsReadCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitOfWork;

        public SendMessageCommandHandler(IServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChatMessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
           
            var message = await _unitOfWork.ChatService.SendMessageAsync(request.MessageDto);
            await _unitOfWork.ChatMessagesService.AddMessageAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ChatMessageDto>(message);

        }

        public async Task<Unit> Handle(MarkMessagesAsReadCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ChatMessagesService.MarkMessagesAsReadAsync(request.ChatId, request.UserId);
            return Unit.Value;
        }
    }
}