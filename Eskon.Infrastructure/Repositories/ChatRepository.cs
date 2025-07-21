using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class ChatRepository : GenericRepositoryAsync<Chat>, IChatRepository
    {
        #region Fields
        private readonly DbSet<Chat> _chatDbSet;
        #endregion

        #region Constructors
        public ChatRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _chatDbSet = myDbContext.Set<Chat>();
        }
        #endregion

        public async Task<List<Chat>> GetChatsForUserAsync(Guid userId)
        {
            return await _chatDbSet
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();
        }
        public async Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id)
        {
            return await _chatDbSet.AnyAsync(c =>
                (c.User1Id == user1Id && c.User2Id == user2Id) ||
                (c.User1Id == user2Id && c.User2Id == user1Id));
        }
    }
}
