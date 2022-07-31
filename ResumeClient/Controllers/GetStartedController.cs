using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeClient.Models;
using System.Text;

namespace ResumeClient.Controllers
{
    public class GetStartedController : Controller
    {
        //GetStarted

        public async Task<IActionResult> GetStarted()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
                return RedirectToAction("Index", "Resume");
      
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel data)
        {
            Credentials cred = null;

            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"https://localhost:7188/api/credentials/{data.Login}/{data.Password}"))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        cred = JsonConvert.DeserializeObject<Credentials>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetStarted");
            }
            

            if (cred.Id == 0)
            {
                return RedirectToAction("GetStarted");
            }


            HttpContext.Session.SetString("CurrentUserId", cred.User.Id.ToString());
            HttpContext.Session.SetString("CurrentUser", cred.Login);
            return RedirectToAction("Index", "Resume");


        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel data) 
        {
            if (ModelState.IsValid)
            {
                Credentials cred;

                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"https://localhost:7188/api/credentials/{data.Login}"))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        cred = JsonConvert.DeserializeObject<Credentials>(content);
                    }
                }

                if (cred.Login != null)
                {
                    return RedirectToAction("GetStarted");
                }

                cred = new Credentials()
                {
                    Login = data.Login,
                    Password = data.Password,
                    Role = "user",
                    Email = data.Email,
                    User = new User()
                    {
                        Name = data.Name,
                        SurName = data.SurName,
                        Age = data.Age,
                        MainSkills = new List<MainSkill>(),
                        AdditionSkills = new List<AdditionSkill>(),
                        Projects = new List<UserProject>(),
                        Areas = new List<Area>(),
                        Sections = new List<Section>()
                    }

                };


                using (var client = new HttpClient())
                {
                    var credJson = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");
                    using (var responseUser = await client.PostAsync("https://localhost:7188/api/credentials", credJson)) ;
                }
            }

            return RedirectToAction("GetStarted");
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.SetString("CurrentUser", "");
            HttpContext.Session.SetString("CurrentUserId", "");

            return RedirectToAction("GetStarted");
        }

        public async Task<IActionResult> Guest()
        {
            HttpContext.Session.SetString("CurrentUser", "Guest");
            HttpContext.Session.SetString("CurrentUserId", "");
            return RedirectToAction("Index", "Resume");
        }
    }
}
