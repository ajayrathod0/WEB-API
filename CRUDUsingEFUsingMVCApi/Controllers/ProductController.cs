using CRUDUsingEFUsingMVCApi.Models;
using Newtonsoft.Json;//install package Newtonsoft.Json
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;  //install package System.Net.Http
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel;
using PagedList;
using System.Web.UI;

namespace CRUDUsingEFUsingMVCApi.Controllers
{
    public class ProductController : Controller
    {
        HttpClient _client = null;

        public ProductController()
        {
            _client = new HttpClient();
            Uri apiAddress = new Uri(GlobalConstants.ApiUrl);
            _client.BaseAddress = apiAddress;
        }


        // GET: Product
        public ActionResult Index(string sort, int? page, int? rPerPage, string serach)
        {
            #region Using C#
            IEnumerable<Product> products = new List<Product>();

            //Api call
            /* HttpClient _client = new HttpClient();
             Uri apiAddress = new Uri("http://localhost:54360/api/");
             _client.BaseAddress = apiAddress;*/

            Utility.AddTokenToRequest(_client);

            HttpResponseMessage response = _client.GetAsync("Product").Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonResponseData = response.Content.ReadAsStringAsync().Result;

                products = JsonConvert.DeserializeObject<List<Product>>(jsonResponseData);
            }



            ViewBag.NameSort = (sort == "name") ? "name desc" : "name";
            ViewBag.PriceSort = (sort == "price") ? "price desc" : "price";
            ViewBag.AvailableQuantity = (sort == "AvailableQuantity") ? "AvailableQuantity desc" : "price";

            // sort data
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {

                    case "name":
                        products = products.OrderBy(x => x.Name).ToList();
                        break;
                    case "price":
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case "price desc":
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "AvailableQuantity":
                        products = products.OrderBy(x => x.AvailableQuantity).ToList();
                        break;
                    case "AvailableQuantity desc":
                        products = products.OrderByDescending(x => x.AvailableQuantity).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.Name).ToList();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(serach))
            {
                products = products.Where(
                    p => p.Name.Contains(serach) ||
                    p.Price.ToString().Equals(serach)
                ).ToList();
            }


            /*  if (string.IsNullOrEmpty(search))
                        {
                            products = products.Where(p=>p.Id.)
                        }*/

            return View(products.ToPagedList(page ?? 1, rPerPage ?? 3));


            #endregion Using C#

            //using JQuery in index.cshtml page ,call api in ajax

            //return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            #region using C#
            if (ModelState.IsValid)
            {
                /* HttpClient _client = new HttpClient();
                 Uri apiAdderess = new Uri("http://localhost:54360/api/");
                 _client.BaseAddress = apiAdderess;*/

                Utility.AddTokenToRequest(_client);

                string requestBody = JsonConvert.SerializeObject(product);
                StringContent bodyContent = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync("product", bodyContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Massage = "Product Created Successfull ";
                }
                else
                {
                    ViewBag.Massage = "Product Created Failed";
                }
            }

            #endregion using C#

            //using Jqury se get call honga vahi pr view me hi ajax call se API call ho jayenga/create ho jayenga

            return View();
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            /*HttpClient _client = new HttpClient();
            Uri apiAdderess = new Uri("http://localhost:54360/api/");
            _client.BaseAddress = apiAdderess;*/

            Utility.AddTokenToRequest(_client);

            HttpResponseMessage response = _client.GetAsync($"product/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;

                Product product = JsonConvert.DeserializeObject<Product>(jsonResult);

                TempData["product"] = product;

                return View(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product products)
        {
            if (products != null)
            {
                /* HttpClient _client = new HttpClient();
                 Uri apiAdderess = new Uri("http://localhost:54360/api/");
                 _client.BaseAddress = apiAdderess;*/

                Utility.AddTokenToRequest(_client);

                string jsonBody = JsonConvert.SerializeObject(products);
                StringContent requestBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PutAsync($"product/{products.Id}", requestBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Massage = "Error in updating product";

            return View((Product)TempData["product"]);
        }

        [HttpGet]
        public ActionResult Details(int? Id)
        {
            /* HttpClient _client = new HttpClient();
             Uri apiAdderess = new Uri("http://localhost:54360/api/");
             _client.BaseAddress = apiAdderess;*/

            Utility.AddTokenToRequest(_client);

            Product product = new Product();

            HttpResponseMessage response = _client.GetAsync($"product/{Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(jsonResult);

            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            /* HttpClient _client = new HttpClient();
             Uri apiAdderess = new Uri("http://localhost:54360/api/");
             _client.BaseAddress = apiAdderess;*/

            Utility.AddTokenToRequest(_client);

            HttpResponseMessage response = _client.GetAsync($"product/{Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;

                Product product = JsonConvert.DeserializeObject<Product>(jsonResult);

                TempData["product"] = product;

                return View(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? Id)
        {
            /*  HttpClient _client = new HttpClient();
              Uri apiAdderess = new Uri("http://localhost:54360/api/");
              _client.BaseAddress = apiAdderess;*/
            Utility.AddTokenToRequest(_client);


            HttpResponseMessage response = _client.DeleteAsync($"product/{Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Massage = "Error in Deleting Product";

            return View((Product)TempData["product"]);
        }

    }
}