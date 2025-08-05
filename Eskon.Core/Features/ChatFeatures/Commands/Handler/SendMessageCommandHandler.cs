using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Handler
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessageDto>, IRequestHandler<MarkMessagesAsReadCommand, Unit>
    {
        private readonly IRepositoryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IChatMessageRepository _chatMessageRepository;

        public SendMessageCommandHandler(IRepositoryUnitOfWork unitOfWork, IMapper mapper, IChatRepository chatRepository, IChatMessageRepository chatMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatMessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
           
            var message = await _chatRepository.SendMessageAsync(request.MessageDto);
            await _chatMessageRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ChatMessageDto>(message);
        }

        public async Task<Unit> Handle(MarkMessagesAsReadCommand request, CancellationToken cancellationToken)
        {
            await _chatMessageRepository.MarkMessagesAsReadAsync(request.ChatId, request.UserId);
            return Unit.Value;
        }
    }
}