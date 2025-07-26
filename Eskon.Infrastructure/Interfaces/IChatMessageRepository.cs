using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IChatMessageRepository : IGenericRepositoryAsync<ChatMessage>
    {
    }
}
