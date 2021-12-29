using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Treningi.WebApp.Controllers
{
    public class CoachController : Controller
    {
        public IConfiguration Configuration;

        public CoachController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ContentResult GetHostUrl()
        {
            var result = Configuration["RestApiUrl:HostUrl"];
            return Content(result);
        }
        private string CN()
        {
            return ControllerContext.RouteData.Values["controller"].ToString();
        }
        public async Task<IActionResult> Index()
        {
            string _restpath = GetHostUrl().Content + CN();
            List<CoachVM> coachList = new List<CoachVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    coachList = JsonConvert.DeserializeObject<List<CoachVM>>(apiResponse);
                }
            }

            return View(coachList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            CoachVM s = new CoachVM();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    s = JsonConvert.DeserializeObject<CoachVM>(apiResponse);
                }
            }

            return View(s);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CoachVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            CoachVM sjResult = new CoachVM();
            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{_restpath}/{s.ID}", content))
                    {
                    }
                }
            }
            catch (Exception e)
            {
                return View(e);
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e) { return View(e); }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CoachVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{_restpath}", content))
                {
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Create(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            CoachVM s = new CoachVM();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}"))
                {
                }
            }
            return View(s);
        }
    }
}
