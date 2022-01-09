using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Treningi.WebApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace Treningi.WebApp.Controllers
{
    public class ActivityController : Controller
    {
        public IConfiguration Configuration;
        private string currentlyViewingId = "1";

        public ActivityController(IConfiguration configuration)
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

        [Authorize(Roles = "Trener")]
        public async Task<IActionResult> Editplan(string id)
        {
            currentlyViewingId = id.ToString();
            ViewBag.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string _restpath = GetHostUrl().Content + CN();
            //var tokenString = GenerateJSONWebToken();
            List<ActivityVM> skiJumpersList = new List<ActivityVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    skiJumpersList = JsonConvert.DeserializeObject<List<ActivityVM>>(apiResponse);
                    foreach(ActivityVM a in skiJumpersList.ToArray())
                    {
                        if (a.CompetitorID != id)
                            skiJumpersList.Remove(a);
                    }
                    skiJumpersList.Sort((p, q) => p.hour.CompareTo(q.hour));
                    skiJumpersList.Sort((p, q) => p.day.CompareTo(q.day));

                }
            }

            return View(skiJumpersList);
        }
        [HttpPost]
        public async Task<IActionResult> Editplan(ActivityVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            ActivityVM sjResult = new ActivityVM();
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

        [Authorize(Roles = "Trener")]
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
            return RedirectToAction(nameof(Index), "Competitor");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityVM s)
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
            return RedirectToAction(nameof(Index), "Competitor");
        }
        public async Task<IActionResult> Create(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            ActivityVM s = new ActivityVM();
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
