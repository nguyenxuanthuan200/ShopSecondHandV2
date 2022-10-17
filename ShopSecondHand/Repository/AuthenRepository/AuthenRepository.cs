using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopSecondHand.Data.RequestModels.AuthenRequest;
using ShopSecondHand.Data.ResponseModels.AuthenResponse;
using ShopSecondHand.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.AuthenRepository
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly IMapper _mapper;
        private readonly ShopSecondHandContext dbContext;
        public IConfiguration _configuration;
        public AuthenRepository(IMapper mapper,
           ShopSecondHandContext dbContext,
            IConfiguration configuration)
        {
            this._mapper = mapper;
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Token> GenerateToken(Account account)
        {
            var roleName = await dbContext.Roles.SingleOrDefaultAsync(p => p.Id == account.RoleId);
            if (roleName.Name.Equals("ADMIN"))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", account.Id.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["JwtJwtConfig:Issuer"], _configuration["JwtJwtConfig:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(120), signingCredentials: signIn);

                var result = _mapper.Map<Token>(account);

                result.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                result.Role = roleName.Name;
                return result;
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id",account.Id.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"], _configuration["JwtConfig:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(120), signingCredentials: signIn);


                var result = _mapper.Map<Token>(account);

                result.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                result.Role = roleName.Name;
                return result;
            }


            return null;
        }

        public async Task<Account> LoginByUserNameAndPassword(LoginRequest payload)
        {
            var account = await dbContext.Accounts.Where(p => p.UserName .Equals(payload.UserName))
                .SingleOrDefaultAsync();
            if (account == null)
                return null;
            if (account.Status == false)
                return null;
            var roleName = await dbContext.Roles.Where(p => p.Id ==account.RoleId)
                 .SingleOrDefaultAsync();
           
            bool isVerified=false;
            if (roleName.Name.Equals("USER"))
            {
                isVerified = BCrypt.Net.BCrypt.EnhancedVerify(payload.Password, account.Password);

                return isVerified
                    ? account
                    : null;

            }
            if (roleName.Name.Equals("ADMIN"))
            {
                if(payload.Password == account.Password)

                return account;

            }

            return null;
        }
      
    }
}
