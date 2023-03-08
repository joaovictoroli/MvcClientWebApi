using Microsoft.AspNetCore.Mvc;
using Projeto.BLL.Model;
using System.Text;
using Newtonsoft.Json;
using NuGet.Protocol;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDetails userDetails)
        {
            using (var httpClient = new HttpClient())
            {

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");                

                using (HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7074/api/Auth/Register", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                        //return Redirect("~/Auth/Register");
                    } else
                    {
                        return Redirect("~/Auth/Login");
                    }
                }

                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials loginCredentials)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginCredentials), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7074/api/Auth/Login", stringContent))
                {
                    

                    string token = await response.Content.ReadAsStringAsync();

                    JsonResponse responseJson = System.Text.Json.JsonSerializer.Deserialize<JsonResponse>(token);

                    //responseJson.token = "Bearer " + responseJson.token;
                    Console.WriteLine(responseJson.token);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                        //return Redirect("~/Auth/Login");
                    } else
                    {
                        HttpContext.Session.SetString("JWToken", responseJson.token);
                        return Redirect("~/Friends/Index");
                    }

                    //HttpContext.Session.SetString("JWToken", responseJson.token);

                }

            }

            return View();


        }
    }
}
