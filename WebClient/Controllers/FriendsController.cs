using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Projeto.BLL.Model;
using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace WebClient.Controllers
{
    public class FriendsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Friend> productList = new List<Friend>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:7074/api/Friends"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    productList = JsonConvert.DeserializeObject<List<Friend>>(apiResponse);
                }
            }

            return View(productList);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(Friend friend)
        {
            Friend addFriend = new Friend();

            var accessToken = HttpContext.Session.GetString("JWToken");

            Console.WriteLine(accessToken);
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                

                StringContent content = new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, "application/json");

                Console.WriteLine(content);
                using (var response = await httpClient.PostAsync("https://localhost:7074/api/Friends", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    addFriend = JsonConvert.DeserializeObject<Friend>(apiResponse);
                }
            }

            return View(addFriend);
        }

        public async Task<ActionResult> Update(int id)
        {
            Friend friend = new Friend();
            var accessToken = HttpContext.Session.GetString("JWToken");


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:7074/api/Friends/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    friend = JsonConvert.DeserializeObject<Friend>(apiResponse);
                }
            }
            return View(friend);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Update(Friend friend)
        {
            Friend receivedFriend = new Friend();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, "application/json");



                using (var response = await httpClient.PutAsync("https://localhost:7074/api/Friends/" + friend.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedFriend = JsonConvert.DeserializeObject<Friend>(apiResponse);
                }
            }
            return View(receivedFriend);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int friendId)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync($"https://localhost:7074/api/Friends/{friendId}/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{apiResponse}");
                }
            }
            return RedirectToAction("Index");
        }
    }
}

