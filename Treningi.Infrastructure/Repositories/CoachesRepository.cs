using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treningi.Core;
using Treningi.Core.Repositories;
using Treningi.Infrastructure.Repositories;

namespace Treningi.Infrastructure.Repositories
{
    public class CoachesRepository : ICoachesRepository
    {
        private AppDbContext _appDbContext;
        private int lastId = 0;
        public CoachesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Coach c)
        {
            try
            {
                _appDbContext.Coach.Add(c);
                _appDbContext.SaveChanges();
                lastId++;
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }
        public async Task<IEnumerable<Coach>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Coach);
        }
        public async Task DelAsync(int cid)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Coach.FirstOrDefault(x => x.ID == cid));
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }
        public async Task<Coach> GetAsync(int cid)
        {
            return await Task.FromResult(_appDbContext.Coach.FirstOrDefault(x => x.ID == cid));
        }
        public async Task UpdateAsync(Coach c)
        {
            try
            {
                var z = _appDbContext.Coach.FirstOrDefault(x => x.ID == c.ID);
                z.Forename = c.Forename;
                z.Surname = c.Surname;
                z.ID = c.ID;
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
