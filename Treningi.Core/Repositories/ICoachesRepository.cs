using System.Collections.Generic;
using System.Threading.Tasks;

namespace Treningi.Core.Repositories
{
    public interface ICoachesRepository
    {
        Task UpdateAsync(Coach z);
        Task AddAsync(Coach z);
        Task DelAsync(int id);
        Task<Coach> GetAsync(int id);
        Task<IEnumerable<Coach>> BrowseAllAsync();
        int getAvailableId();
    }
}
