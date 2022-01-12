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
using Microsoft.AspNetCore.Identity;

namespace Treningi.WebApp.Controllers
{
    public class ActivityController : Controller
    {
        public IConfiguration Configuration;
        private static string currentlyViewingId = "1";
        static int currentId;
        private readonly JWToken _jwtoken;
        public ActivityController(IConfiguration configuration, JWToken jwtoken)
        {
            _jwtoken = jwtoken;
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
            currentlyViewingId = id;
            currentId = Int32.Parse(id);
            ViewBag.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            List<ActivityVM> skiJumpersList = new List<ActivityVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    skiJumpersList = JsonConvert.DeserializeObject<List<ActivityVM>>(apiResponse);
                    foreach(ActivityVM a in skiJumpersList.ToArray())
                    {
                        if (a.CompetitorID != currentId)
                            skiJumpersList.Remove(a);
                    }
                    skiJumpersList.Sort((p, q) => p.hour.CompareTo(q.hour));
                    skiJumpersList = skiJumpersList.OrderBy(x => ( getDayValue(x.day))).ToList();
                }
            }

            return View(skiJumpersList);
        }
        private int getDayValue(string s)
        {
            if (s == "Poniedziałek")
                return 0;
            if (s == "Wtorek")
                return 1;
            if (s == "Środa")
                return 2;
            if (s == "Czwartek")
                return 3;
            if (s == "Piątek")
                return 4;
            if (s == "Sobota")
                return 5;
            return 6;
        }
        [HttpPost]
        public async Task<IActionResult> Editplan(ActivityVM s)
        {
            currentId = s.ID;
            string _restpath = GetHostUrl().Content + CN();
            ActivityVM sjResult = new ActivityVM();
            try
            {
                var tokenString = _jwtoken.GenerateJSONWebToken();
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
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
                var tokenString = _jwtoken.GenerateJSONWebToken();
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e) { return View(e); }
            return RedirectToAction(nameof(Editplan), new { id = currentId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityVM s)
        {
            s.CompetitorID = currentId;
            string _restpath = GetHostUrl().Content + CN();
            string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
            var tokenString = _jwtoken.GenerateJSONWebToken();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{_restpath}", content))
                {
                }
            }
            return RedirectToAction(nameof(Editplan), new { id = currentId });
        }
        public async Task<IActionResult> Create(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            ActivityVM s = new ActivityVM();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync($"{_restpath}"))
                {
                }
            }
            return View(s);
        }

        [Authorize(Roles = "Zawodnik")]
        public async Task<IActionResult> Showactivities(ActivityVM avm)
        {
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            List<ActivityVM> skiJumpersList = new List<ActivityVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    skiJumpersList = JsonConvert.DeserializeObject<List<ActivityVM>>(apiResponse);
                    foreach (ActivityVM a in skiJumpersList.ToArray())
                    {
                        if (a.CompetitorID != avm.CompetitorID)
                            skiJumpersList.Remove(a);
                    }
                    skiJumpersList.Sort((p, q) => p.hour.CompareTo(q.hour));
                    skiJumpersList = skiJumpersList.OrderBy(x => (getDayValue(x.day))).ToList();
                }
            }
            return View(skiJumpersList);
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}
