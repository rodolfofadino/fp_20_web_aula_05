using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fiapweb2020.api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace fiapweb2020.api.Configure
{
    [Route("/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody]TokenInfo model)
        {
            if (model.Password == "123" && model.UserName == "apiuser")
            {
                var token = GenerateToken(model);

                return new ObjectResult(token);
            }


            return BadRequest();
        }

        private string GenerateToken(TokenInfo model)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()));


            var keyEncoded = Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256");
            var symetricSecurityKey = new SymmetricSecurityKey(keyEncoded);
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);


            var result = new JwtSecurityTokenHandler().WriteToken(token);


            //byte[] keyEncoded = Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256");
            //SymmetricSecurityKey symetricSecurityKey = new SymmetricSecurityKey(keyEncoded);
            //SigningCredentials signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //JwtHeader header = new JwtHeader(signingCredentials);
            //JwtPayload payload = new JwtPayload(claims);

            //JwtSecurityToken token = new JwtSecurityToken(header, payload);


            //string result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }
    }
}