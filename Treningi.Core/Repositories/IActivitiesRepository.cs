using System.Collections.Generic;
using System.Threading.Tasks;

namespace Treningi.Core.Repositories
{
    public interface IActivitiesRepository
    {
        Task<Activity> GetAsync(int id);
        Task<IEnumerable<Activity>> BrowseAllAsync();
        Task DelAsync(int id);
        Task AddAsync(Activity c);
        int getAvailableId();
    }
}
