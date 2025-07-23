
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;


namespace Eskon.Infrastructure.Interfaces
{
    public interface IImageRepository : IGenericRepositoryAsync<Image>
    {
        //public Task<List<Image>> GetAllImagesByPropertyIdAsync(Guid id);
    }
}
