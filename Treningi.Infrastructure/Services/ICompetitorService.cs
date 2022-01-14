using System.Collections.Generic;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;

namespace Treningi.Infrastructure.Services
{
    public interface ICompetitorService
    {
        Task<IEnumerable<CompetitorDTO>> BrowseAll();
        Task<CompetitorDTO> Get(int id);
        Task<IEnumerable<CompetitorDTO>> GetByFilter(string name, string country);
        Task<CompetitorDTO> Add(CreateCompetitor s);
        Task<CompetitorDTO> Delete(int s);
        Task<CompetitorDTO> Update(UpdateCompetitor u);
    }
}
