using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeClient.Models;
using System.Text;

namespace ResumeClient.Controllers
{
    //https://localhost:7188/api/users
    public class ResumeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
                return RedirectToAction("GetStarted", "GetStarted");

            List<User> users = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7188/api/users"))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(content);
                }
            }

            return View(users);
        }

        public async Task<IActionResult> Resume(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
                return RedirectToAction("GetStarted", "GetStarted");

            if (id == 0)
                return RedirectToAction("Index");

            User user = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://localhost:7188/api/users/{id}"))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }

            if (user == null)
                return RedirectToAction("Index");

            return View(user);
        }

        public async Task<IActionResult> MyResume()
        {
            if (HttpContext.Session.GetString("CurrentUser") == "Guest")
                return RedirectToAction("Index");

            string userId = HttpContext.Session.GetString("CurrentUserId");

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");
      
            return RedirectToAction("Resume", new { id = Convert.ToInt32(userId) });
        }
        

        public async Task<IActionResult> Edit(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")))
                return RedirectToAction("GetStarted", "GetStarted");

            if (id == 0 || id.ToString() != HttpContext.Session.GetString("CurrentUserId"))
                return RedirectToAction("Index");            

            User user = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"https://localhost:7188/api/users/{id}"))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }

            if (user == null)
                return RedirectToAction("Index");

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile (User user, IFormFile profileImage)
        {

            List<User> users = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7188/api/users"))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(content);
                }
            }

            var oldUser = users.FirstOrDefault(u => u.Id.ToString() == HttpContext.Session.GetString("CurrentUserId"));

            oldUser.Name = user.Name;
            oldUser.SurName = user.SurName;
            oldUser.Age = user.Age;
            oldUser.Telegram = user.Telegram;
            oldUser.Instagram = user.Instagram;
            oldUser.GitHub = user.GitHub;

            if (profileImage != null)
                using (var ms = new MemoryStream())
                {
                    profileImage.CopyTo(ms);
                    oldUser.BaseImage = Convert.ToBase64String(ms.ToArray());
                }

            using (var client = new HttpClient())
            {
                var userJson = new StringContent(JsonConvert.SerializeObject(oldUser), Encoding.UTF8, "application/json");
                using (var responseUser = await client.PutAsync($"https://localhost:7188/api/users/{oldUser.Id}", userJson)) ; 
            }

            return RedirectToAction("Resume", new { id = oldUser.Id });
        }

        public async Task<IActionResult> AddMainSkill(int id)
        {
            var skill = new MainSkill() { UserId = id };
            return View(skill); 
        }
        public async Task<IActionResult> AddAdditionSkill(int id)
        {
            var skill = new AdditionSkill() { UserId = id };
            return View(skill); 
        }
        [HttpPost]
        public async Task<IActionResult> AddMainSkill(MainSkill skill)
        {
            using (var client = new HttpClient())
            {
                var jsonSkill = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"https://localhost:7188/api/mainSkills/", jsonSkill)) ;
            }

            return RedirectToAction("Edit", new { id = skill.UserId });
        }


        [HttpPost]
        public async Task<IActionResult> AddAdditionSkill(AdditionSkill skill)
        {
            using (var client = new HttpClient())
            {
                var jsonSkill = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"https://localhost:7188/api/additionSkills/", jsonSkill)) ;
            }

            return RedirectToAction("Edit", new { id = skill.UserId } );
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSection(Section section)
        {
            using (var client = new HttpClient())
            {
                var jsonSection = new StringContent(JsonConvert.SerializeObject(section), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"https://localhost:7188/api/sections/", jsonSection)) ;
            }

            return RedirectToAction("Edit", new { id = section.UserId });
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(UserProject project, IFormFile projectImage)
        {
            using (var ms = new MemoryStream())
            {
                projectImage.CopyTo(ms);
                project.BaseImage = Convert.ToBase64String(ms.ToArray());
            }

            using (var client = new HttpClient())
            {
                var jsonProject = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"https://localhost:7188/api/projects/", jsonProject)) ;
            }

            return RedirectToAction("Edit", new { id = project.UserId });
        }
        public async Task<IActionResult> DeleteProfile(int id)
        {
            using ( var client = new HttpClient())
            {
                using var response = await client.DeleteAsync($"https://localhost:7188/api/users/{id}");
            }

            return RedirectToAction("LogOut", "GetStarted");
        }

        public async Task<IActionResult> DeleteMainSkill(int id)
        {
            using (var client = new HttpClient())
            {
                using var response = await client.DeleteAsync($"https://localhost:7188/api/mainSkills/{id}");
            }

            return RedirectToAction("Edit", new { id = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")) });
        }

        public async Task<IActionResult> DeleteAdditionSkill(int id)
        {
            using (var client = new HttpClient())
            {
                using var response = await client.DeleteAsync($"https://localhost:7188/api/additionSkills/{id}");
            }

            return RedirectToAction("Edit", new { id = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")) });
        }
        public async Task<IActionResult> DeleteSection(int id)
        {
            using (var client = new HttpClient())
            {
                using var response = await client.DeleteAsync($"https://localhost:7188/api/sections/{id}");
            }

            return RedirectToAction("Edit", new { id = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")) });
        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            using (var client = new HttpClient())
            {
                using var response = await client.DeleteAsync($"https://localhost:7188/api/projects/{id}");
            }

            return RedirectToAction("Edit", new { id = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")) });
        }


    }
    
}
