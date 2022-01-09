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
    public class ActivityService : IActivityService
    {
        private readonly IActivitiesRepository _coachRepository;
        public ActivityService(IActivitiesRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }
        public async Task<IEnumerable<ActivityDTO>> BrowseAll()
        {
            var z = await _coachRepository.BrowseAllAsync();
            return z.Select(x => new ActivityDTO()
            {
                ID = x.ID,
                day = x.day,
                hour = x.hour,
                exercise = x.exercise,
                CompetitorID = x.CompetitorID
            });
        }
        public async Task<ActivityDTO> Get(int givenId)
        {
            var x = await _coachRepository.GetAsync(givenId);
            return (new ActivityDTO()
            {
                ID = x.ID,
                day = x.day,
                hour = x.hour,
                exercise = x.exercise,
                CompetitorID = x.CompetitorID
            });
        }

        public async Task<ActivityDTO> Add(CreateActivity s)
        {
            await _coachRepository.AddAsync(new Activity()
            {
                ID = _coachRepository.getAvailableId(),
                CompetitorID = s.CompetitorID,
                day = s.day,
                hour = s.hour,
                exercise = s.exercise
            });
            return (new ActivityDTO()
            {

            });
        }
        public async Task<ActivityDTO> Delete(int id)
        {
            await _coachRepository.DelAsync(id);
            return (new ActivityDTO()
            {

            });
        }

    }
}
