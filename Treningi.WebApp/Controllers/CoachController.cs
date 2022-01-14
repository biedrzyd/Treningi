using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Treningi.WebApp.Models;

namespace Treningi.WebApp.Controllers
{
    public class CoachController : Controller
    {
        public IConfiguration Configuration;
        private readonly JWToken _jwtoken;
        public CoachController(IConfiguration configuration, JWToken jwtoken)
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
        public async Task<IActionResult> Index()
        {
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            CoachVM coachVM = new CoachVM();
            coachVM.Forename = _restpath;
            List<CoachVM> coachList = new List<CoachVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    coachList = JsonConvert.DeserializeObject<List<CoachVM>>(apiResponse);
                    coachList.Add(coachVM);
                }
            }

            return View(coachList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            CoachVM s = new CoachVM();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
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
        [Authorize(Roles = "Admin")]
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
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CoachVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
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
    }
}
