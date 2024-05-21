using CRUDUsingEFUsingMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CRUDUsingEFUsingMVCApi.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalConstants.ApiUrl);

            HttpResponseMessage response = client.PutAsJsonAsync("account", model).Result;

            if (response.IsSuccessStatusCode)
            {
                Session["apitoken"] = response.Content.ReadAsStringAsync().Result;

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalConstants.ApiUrl);

                HttpResponseMessage response = client.PostAsJsonAsync("account", user).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(user);
        }
    }
}