using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepositoryAsync<ChatMessage>, IChatMessageRepository
    {
        #region Fields
        private readonly DbSet<ChatMessage> _chatMessagesDbSet;
        #endregion

        #region Constructors
        public ChatMessageRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _chatMessagesDbSet = myDbContext.Set<ChatMessage>();
        }
        #endregion


    }
}
