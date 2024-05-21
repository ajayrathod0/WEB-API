using CRUDUsingEF.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace CRUDUsingEF.Controllers
{
    public class AccountController : ApiController
    {
        ProductDBContext _db = new ProductDBContext();

        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();

                    return Ok(user);
                }
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult Login(LoginModel login)
        {
            if (login != null)
            {
                User dbUser = _db.Users.FirstOrDefault(u => u.Email == login.Email &&
                u.Password == login.Password);
                if (dbUser != null)
                {
                    dbUser.IsAuthenticated = true;
                    string token = createToken(dbUser);
                    return Ok(token);
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        private string createToken(User model) 
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(10);
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                   new Claim(ClaimTypes.Name, model.FirstName + " " + model.LastName),
                   new Claim(ClaimTypes.Email, model.Email)
            });

            const string sec =
                "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)
            tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:54360",
            audience: "http://localhost:54360",
            subject: claimsIdentity,
            notBefore: issuedAt, expires: expires,
            signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
