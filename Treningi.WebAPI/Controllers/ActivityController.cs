using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
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
        public async Task<IActionResult> BrowseAll()
        {
            IEnumerable<ActivityDTO> z = await _coachService.BrowseAll();
            return Json(z);
        }

        //https://localhost:5001/skijumper/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkiJumper(int id)
        {
            ActivityDTO z = await _coachService.Get(id);
            return Json(z);
        }

        //https://localhost:5001/skijumper/filters?name=alan&country=ger

        [HttpPost]
        public async Task<IActionResult> CreateSkiJumper([FromBody] CreateActivity skiJumper)
        {
            await _coachService.Add(skiJumper);
            return Json("user added");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkiJumper(int id)
        {
            await _coachService.Delete(id);
            return Json("user with id " + id + " removed");
        }



    }
}
