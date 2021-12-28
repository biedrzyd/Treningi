using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;

namespace Treningi.Infrastructure.Services
{
    public interface ICoachService
    {
        Task<IEnumerable<CoachDTO>> BrowseAll();
        Task<CoachDTO> Get(int id);
        Task<CoachDTO> Add(CreateCoach c);
        Task<CoachDTO> Delete (int s);
        Task<CoachDTO> Update(UpdateCoach u);
    }
}
