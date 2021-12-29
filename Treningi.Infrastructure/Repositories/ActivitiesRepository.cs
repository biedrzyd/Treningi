using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Treningi.Core.Repositories;

using System.Linq;
using Treningi.Core;

namespace Treningi.Infrastructure.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private AppDbContext _appDbContext;
        public ActivitiesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Activity>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Activity);
        }
        public async Task<Activity> GetAsync(int cid)
        {
            return await Task.FromResult(_appDbContext.Activity.FirstOrDefault(x => x.ID == cid));
        }
    }
}
