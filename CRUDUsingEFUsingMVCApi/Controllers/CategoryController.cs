using CRUDUsingEFUsingMVCApi;
using CRUDUsingEFUsingMVCApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace CRUDUsingEFMVCClient.Controllers
{
    public class CategoryController : Controller
    {
        HttpClient _client = null;

        public CategoryController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(GlobalConstants.ApiUrl);
        }

        // GET: Category
        public ActionResult Index()
        {
            List<Category> categories = new List<Category>();

            // string json = _client.GetStringAsync("category").Result;

            Utility.AddTokenToRequest(_client);

            HttpResponseMessage response =
                _client.GetAsync("category").Result;

            if (response.IsSuccessStatusCode)
            {
                categories =
                    response.Content.ReadAsAsync<List<Category>>().Result;
            }

            return View(categories);
        }
    }
}