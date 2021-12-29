using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Treningi.Core.Repositories
{
    public interface IActivitiesRepository
    {
        Task<Activity> GetAsync(int id);
        Task<IEnumerable<Activity>> BrowseAllAsync();
    }
}
