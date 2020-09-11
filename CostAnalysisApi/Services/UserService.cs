using CostAnalysis.DataSource;
using CostAnalysis.Models;
using CostAnalysisAPI.Helpers;
using CostAnalysisAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CostAnalysisAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationContext _applicationContext;

        public UserService(IOptions<AppSettings> appSettings,
            ApplicationContext applicationContext)
        {
            _appSettings = appSettings.Value;
            _applicationContext = applicationContext;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _applicationContext.Users.FirstOrDefault(it =>
                it.Username == model.Username && it.Password == model.Password);

            if (user == null)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public UserService GetById(int id)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
