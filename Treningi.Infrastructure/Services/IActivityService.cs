using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;

namespace Treningi.Infrastructure.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDTO>> BrowseAll();
        Task<ActivityDTO> Get(int id);
    }
}
