using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treningi.Core;
using Treningi.Core.Repositories;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;

namespace Treningi.Infrastructure.Services
{
    public class CoachService : ICoachService
    {
        private readonly ICoachesRepository _coachRepository;
        public CoachService(ICoachesRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }
        public async Task<IEnumerable<CoachDTO>> BrowseAll()
        {
            var z = await _coachRepository.BrowseAllAsync();
            return z.Select(x => new CoachDTO()
            {
                ID = x.ID,
                Forename = x.Forename,
                Surname = x.Surname,
                Date_of_birth = x.Date_of_birth
            });
        }
        public async Task<CoachDTO> Get(int givenId)
        {
            var x = await _coachRepository.GetAsync(givenId);
            return (new CoachDTO()
            {
                ID = x.ID,
                Forename = x.Forename,
                Surname = x.Surname,
                Date_of_birth = x.Date_of_birth
            });
        }
        public async Task<CoachDTO> Add(CreateCoach s)
        {
            await _coachRepository.AddAsync(new Coach()
            {
                ID = _coachRepository.getAvailableId(),
                Forename = s.Forename,
                Surname = s.Surname,
                Date_of_birth = s.DateBirth
            });
            return (new CoachDTO()
            {

            });
        }
        public async Task<CoachDTO> Delete(int id)
        {
            await _coachRepository.DelAsync(id);
            return (new CoachDTO()
            {

            });
        }


        public async Task<CoachDTO> Update(UpdateCoach s)
        {
            Coach coach = new Coach()
            {
                ID = s.ID,
                Forename = s.Forename,
                Surname = s.Surname,
                Date_of_birth = s.DateBirth,
            };
            await _coachRepository.UpdateAsync(coach);

            return new CoachDTO()
            {
                Forename = s.Forename,
                Surname = s.Surname,
                Date_of_birth = s.DateBirth
            };
        }
    }
}