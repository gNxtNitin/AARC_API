using AALib;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgencyAdmins.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;

        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserInfo user)
         {
            AALib.clsAAUserInfo.AAUI aauiUser = new AALib.clsAAUserInfo.AAUI();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();

            if (user != null && user.UserName != null)
            {
                aauiUser = oUI.GetUserInfo(user.UserName);
                if(string.IsNullOrEmpty(aauiUser.UserID))
                    aauiUser = oUI.GetUserInfoByEmail(user.UserName);
                var encryption = new Encryption();
                string encryptedPaassword = encryption.EncryptValue(user.Password);
                if (aauiUser != null && encryptedPaassword == aauiUser.Password)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", aauiUser.UserID.ToString()),
                        new Claim("DisplayName", aauiUser.First_name),
                        new Claim("Email", aauiUser.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
                }
                return BadRequest("Invalid credentials");
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<int> Changepassword(ChangePassword changePassword)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            Encryption encryption = new Encryption();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                var encryptedPassword = encryption.EncryptValue(changePassword.Password);
 
                lstParams.Add(new SqlParameter("@Email", changePassword.Email));
                lstParams.Add(new SqlParameter("@Password", encryptedPassword));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_update_Password", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }

            return iRet;
        }

       

    }
}
