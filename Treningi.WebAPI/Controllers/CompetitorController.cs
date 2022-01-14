using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Treningi.Infrastructure.Commands;
using Treningi.Infrastructure.DTO;
using Treningi.Infrastructure.Services;

namespace Treningi.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class CompetitorController : Controller
    {
        private readonly ICompetitorService _competitorService;

        public CompetitorController(ICompetitorService competitorService)
        {
            _competitorService = competitorService;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            IEnumerable<CompetitorDTO> z = await _competitorService.BrowseAll();
            return Json(z);
        }

        //https://localhost:5001/skijumper/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkiJumper(int id)
        {
            CompetitorDTO z = await _competitorService.Get(id);
            return Json(z);
        }

        //https://localhost:5001/skijumper/filters?name=alan&country=ger

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter(string name, string country)
        {
            IEnumerable<CompetitorDTO> z = await _competitorService.GetByFilter(name, country);
            return Json(z);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkiJumper([FromBody] CreateCompetitor skiJumper)
        {
            await _competitorService.Add(skiJumper);
            return Json("user added");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkiJumper([FromBody] UpdateCompetitor skiJumper)
        {
            await _competitorService.Update(skiJumper);
            return Json("user updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkiJumper(int id)
        {
            await _competitorService.Delete(id);
            return Json("user with id " + id + " removed");
        }

    }
}
