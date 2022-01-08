using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treningi.Core;
using Treningi.Core.Repositories;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;

namespace Treningi.Infrastructure.Services
{
    public class CompetitorService : ICompetitorService
    {
        private readonly ICompetitorsRepository _competitorsRepository;
        public CompetitorService(ICompetitorsRepository competitorsRepository)
        {
            _competitorsRepository = competitorsRepository;
        }
        public async Task<IEnumerable<CompetitorDTO>> BrowseAll()
        {
            var z = await _competitorsRepository.BrowseAllAsync();
            return z.Select(x => new CompetitorDTO()
            {
                ID = x.ID,
                Forename = x.Forename,
                Surname = x.Surname,
                Country = x.Country,
                Height = x.Height,
                Weight = x.Weight,
                Date_of_birth = x.Date_of_birth,
                CoachId = x.CoachId
            });
        }
        public async Task<IEnumerable<CompetitorDTO>> GetByFilter(string name, string country)
        {
            var x = await _competitorsRepository.GetByFilterSync(name, country);
            return x.Select(x => new CompetitorDTO()
            {
                ID = x.ID,
                Forename = x.Forename,
                Surname = x.Surname,
                Country = x.Country,
                Height = x.Height,
                Weight = x.Weight,
                Date_of_birth = x.Date_of_birth,
                CoachId = x.CoachId
            });
        }
        public async Task<CompetitorDTO> Get(int givenId)
        {
            var x = await _competitorsRepository.GetAsync(givenId);
            return (new CompetitorDTO()
            {
                ID = x.ID,
                Forename = x.Forename,
                Surname = x.Surname,
                Country = x.Country,
                Height = x.Height,
                Weight = x.Weight,
                Date_of_birth = x.Date_of_birth,
                CoachId = x.CoachId
            });
        }
        public async Task<CompetitorDTO> Add(CreateCompetitor s)
        {
            await _competitorsRepository.AddAsync(new Competitor()
            {
                ID = _competitorsRepository.getAvailableId(),
                Forename = s.Forename,
                Surname = s.Surname,
                Country = s.Country,
                Height = s.Height,
                Weight = s.Weight,
                Date_of_birth = s.DateBirth,
                CoachId = s.CoachId
            });
            return (new CompetitorDTO()
            {

            });
        }
        public async Task<CompetitorDTO> Delete(int id)
        {
            await _competitorsRepository.DelAsync(id);
            return (new CompetitorDTO()
            {

            });
        }


        public async Task<CompetitorDTO> Update(UpdateCompetitor s)
        {
            Competitor skiJumper = new Competitor()
            {
                ID = s.ID,
                Forename = s.Forename,
                Surname = s.Surname,
                Country = s.Country,
                Height = s.Height,
                Weight = s.Weight,
                Date_of_birth = s.DateBirth,
                CoachId = s.CoachId
            };
            await _competitorsRepository.UpdateAsync(skiJumper);

            return new CompetitorDTO()
            {
                Forename = s.Forename,
                Surname = s.Surname,
                Country = s.Country,
                Height = s.Height,
                Weight = s.Weight,
                Date_of_birth = s.DateBirth,
                CoachId = s.CoachId
            };
        }
    }
}