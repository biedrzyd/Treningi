using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treningi.Core.Repositories;
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
    }
}
