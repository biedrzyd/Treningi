using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;
using Treningi.Infrastructure.Services;

namespace Treningi.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class CoachController : Controller
    {
        private readonly ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseAllCoaches()
        {
            IEnumerable<CoachDTO> z = await _coachService.BrowseAll();
            return Json(z);
        }

        //https://localhost:5001/skijumper/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoach(int id)
        {
            CoachDTO z = await _coachService.Get(id);
            return Json(z);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoach([FromBody] CreateCoach c)
        {
            await _coachService.Add(c);
            return Json("coach added");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoach([FromBody] UpdateCoach c)
        {
            await _coachService.Update(c);
            return Json("coach updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            CoachDTO z = await _coachService.Delete(id);
            return Json("coach with id " + id + " removed");
        }
    }
}
