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
        private int lastId = 0;
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

        public async Task AddAsync(Activity c)
        {
            try
            {
                _appDbContext.Activity.Add(c);
                _appDbContext.SaveChanges();
                lastId++;
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }

        public async Task DelAsync(int sid)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Activity.FirstOrDefault(x => x.ID == sid));
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }
        public int getAvailableId()
        {
            return lastId;
        }
    }
}
