using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Treningi.WebApp;
using Treningi.WebApp.Models;

namespace Treningi.WebApp.Controllers
{
    public class CompetitorController : Controller
    {
        public IConfiguration Configuration;
        private readonly IEmailSender _emailSender;
        private readonly JWToken _jwtoken;

        public CompetitorController(IEmailSender emailSender, IConfiguration configuration, JWToken jwtoken)
        {
            _emailSender = emailSender;
            Configuration = configuration;
            _jwtoken = jwtoken;
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
            ViewBag.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ViewBag.Id == null)
                ViewBag.Id = -1;
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            List<CompetitorVM> skiJumpersList = new List<CompetitorVM>();
            using (var httpClient = new HttpClient( new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }}) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    skiJumpersList = JsonConvert.DeserializeObject<List<CompetitorVM>>(apiResponse);
                }
            }

            return View(skiJumpersList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            CompetitorVM s = new CompetitorVM();
            using (var httpClient = new HttpClient( new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }}) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    s = JsonConvert.DeserializeObject<CompetitorVM>(apiResponse);
                }
            }
            return View(s);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CompetitorVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            CompetitorVM sjResult = new CompetitorVM();
            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }}) )
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{_restpath}/{s.ID}", content))
                    {
                    }
                }
            } catch ( Exception e)
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
            catch (Exception e){ return View(e); }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompetitorVM s)
        {
            string _restpath = GetHostUrl().Content + CN();

            string messageBody = "Konto " + s.Forename + " " + s.Surname + " zostalo stworzone";
            var message = new Message(new string[] { "mytrainingsapp@gmail.com" }, "Test email", "aaaaaaaaaaaa");
            message.Content = messageBody;
            _emailSender.SendEmail(message);
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
            CompetitorVM s = new CompetitorVM();
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



        public async Task<IActionResult> Details(int id)
        {
            string _restpath = GetHostUrl().Content + CN();
            CompetitorVM s = new CompetitorVM();
            var tokenString = _jwtoken.GenerateJSONWebToken();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    s = JsonConvert.DeserializeObject<CompetitorVM>(apiResponse);
                }
            }

            return View(s);
        }
        [HttpPost]
        public async Task<IActionResult> Details(CompetitorVM s)
        {
            string _restpath = GetHostUrl().Content + CN();
            CompetitorVM sjResult = new CompetitorVM();
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
        public async Task<IActionResult> Editplan(int id)
        {
            string _restpath = "https://localhost:5001/Activity";
            List<ActivityVM> skiJumpersList = new List<ActivityVM>();
            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                var tokenString = _jwtoken.GenerateJSONWebToken();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
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
    }
}
