using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Treningi.Core;

namespace Treningi.Core.Repositories
{
    public interface ICompetitorsRepository
    {
        Task UpdateAsync(Competitor c);
        Task AddAsync(Competitor c);
        Task DelAsync(int id);
        Task<Competitor> GetAsync(int id);
        Task<IEnumerable<Competitor>> BrowseAllAsync();
        Task<IEnumerable<Competitor>> GetByFilterSync(string name, string country);
        int getAvailableId();
    }
}
