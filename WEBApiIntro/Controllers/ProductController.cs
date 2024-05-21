using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBApiIntro.Models;

namespace WEBApiIntro.Controllers
{
    public class ProductController : ApiController
    {
        #region using C# method

        /* [HttpGet]
         public List<Product> GetAll()
         {
             List<Product> products = new List<Product>()
             {
              new Product() { ProductId = 1 ,ProductName = "Shirt", ProductPrice = 999 },
              new Product() { ProductId = 2 ,ProductName = "T-Shirt", ProductPrice = 499 },
              new Product() { ProductId = 3 ,ProductName = "Jens", ProductPrice = 1999 },
             };

             return products;
         }

         [HttpGet]
         public Product GetProductById(int id)
         {
             Product products = new Product() { ProductId = 1, ProductName = "Shirt", ProductPrice = 999 };
             return products;
         }*/

        #endregion using C# method



        #region using HttpResponseMessage

        /*
                public HttpResponseMessage GetAll()
                {
                    List<Product> product = new List<Product>()
                     {
                      new Product() { ProductId = 1 ,ProductName = "Shirt", ProductPrice = 999 },
                      new Product() { ProductId = 2 ,ProductName = "T-Shirt", ProductPrice = 499 },
                      new Product() { ProductId = 3 ,ProductName = "Jens", ProductPrice = 1999 },
                     }; 

                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }

                [HttpGet]
                public HttpResponseMessage GetProductById(int id)
                {
                    Product products = new Product() { ProductId = 1, ProductName = "Shirt", ProductPrice = 999 };
                    return Request.CreateResponse(HttpStatusCode.OK, products);
                }*/

        #endregion using HttpResponseMessage


        #region Using IHttpActionResult

        public IHttpActionResult GetAll()
        {
            List<Product> product = new List<Product>()
             {
              new Product() { ProductId = 1 ,ProductName = "Shirt", ProductPrice = 999 },
              new Product() { ProductId = 2 ,ProductName = "T-Shirt", ProductPrice = 499 },
              new Product() { ProductId = 3 ,ProductName = "Jens", ProductPrice = 1999 },
             };

            return Ok(product);
        }

        [HttpGet]
        public IHttpActionResult GetProductById(int id)
        {
            Product products = new Product() { ProductId = 1, ProductName = "Shirt", ProductPrice = 999 };
            return Ok(products);
        }


        public IHttpActionResult Create([FromBody]Product product)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }

        }

        [HttpPut]
        public IHttpActionResult Update([FromUri] int id, [FromBody] Product product)
        {
            try
            {

                if (product != null && product.ProductId > 0)
                {
                    if (id == product.ProductId)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return InternalServerError();
            }

        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            if (id >= 0)
            {
                return Ok();
            }
            else
            {

                return NotFound();
            }

        }


        #endregion Using IHttpActionResult
    }
}
