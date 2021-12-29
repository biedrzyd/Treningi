using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treningi.Infrastructure.DTO;
using Treningi.Infrastructure.Services;

namespace Treningi.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _coachService;

        public ActivityController(IActivityService coachService)
        {
            _coachService = coachService;
        }

            [HttpGet]
            public async Task<IActionResult> BrowseAllCoaches()
            {
                IEnumerable<ActivityDTO> z = await _coachService.BrowseAll();
                return Json(z);
            }

            //https://localhost:5001/skijumper/{id}

            [HttpGet("{id}")]
            public async Task<IActionResult> GetCoach(int id)
            {
                ActivityDTO z = await _coachService.Get(id);
                return Json(z);
            }

    }
}
