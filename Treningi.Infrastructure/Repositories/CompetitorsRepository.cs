using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treningi.Core;
using Treningi.Core.Repositories;

namespace Treningi.Infrastructure.Repositories
{
    public class CompetitorsRepository : ICompetitorsRepository
    {
        private AppDbContext _appDbContext;
        private int lastId = 0;
        public CompetitorsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Competitor c)
        {
            try
            {
                _appDbContext.Competitor.Add(c);
                _appDbContext.SaveChanges();
                lastId++;
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }
        public async Task<IEnumerable<Competitor>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Competitor.Include(i => i.Image));
        }
        public async Task<IEnumerable<Competitor>> GetByFilterSync(string name, string country)
        {
            return await Task.FromResult(_appDbContext.Competitor.Where(x => x.Forename == name && x.Country == country));
        }
        public async Task DelAsync(int sid)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Competitor.FirstOrDefault(x => x.ID == sid));
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }
        public async Task<Competitor> GetAsync(int i)
        {
            return await Task.FromResult(_appDbContext.Competitor.FirstOrDefault(x => x.ID == i));
        }
        public async Task UpdateAsync(Competitor s)
        {
            try
            {
                var z = _appDbContext.Competitor.FirstOrDefault(x => x.ID == s.ID);
                z.Forename = s.Forename;
                z.Surname = s.Surname;
                z.Weight = s.Weight;
                z.Height = s.Height;
                z.Country = s.Country;
                z.CoachId = s.CoachId;
                z.ID = s.ID;
                z.Image = s.Image;
                z.UserImageId = s.UserImageId;
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
