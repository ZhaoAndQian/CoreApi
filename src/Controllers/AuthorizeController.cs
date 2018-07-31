using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Preoff.Entity;
using Preoff.Comm;
using Microsoft.AspNetCore.Cors;

namespace Preoff.Controllers
{
    /// <summary>
    /// 授权控制器
    /// </summary>
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class AuthorizeController : BaseController
    {
        private JwtSettings _jwtSettings;
        private PreoffContext _dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_jwtSettingsAccesser">注入Jwt认证</param>
        /// <param name="_db">注入数据库配置</param>
        /// <param name="_accessor">注入http</param>
        public AuthorizeController(IOptions<JwtSettings> _jwtSettingsAccesser, PreoffContext _db)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
            _dbContext = _db;
        }
        /// <summary>
        /// 获取JWT Token
        /// </summary>
        /// <param name="_auth">用户</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Token([FromBody]AuthorizeTable _auth)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var a = _dbContext.UserTable.FirstOrDefault(u => (u.LoginName == _auth.userName) && (u.LoginPwd == Pwd.Ecoding(_auth.password)));
                    if (a is null)
                    {
                        return Json(new
                        {
                            state = "-1",
                            msg = "账号不存在或密码错误！"
                        });
                    }
                    a.LoginCount =(a.LoginCount is null) ? a.LoginCount = 1 : a.LoginCount + 1;
                    a.LastLoginTime = DateTime.Now;
                    //a.LoginCount = a.LoginCount + 1;
                    _dbContext.UserTable.Update(a);
                    _dbContext.SaveChanges();
                    //var claims=new Claim[]{
                    //    new Claim(ClaimTypes.Name,userModel.CName),
                    //    new Claim(ClaimTypes.Role,"user"),
                    //    //new Claim("SuperAdminOnly","true")
                    //};

                    var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,_auth.userName),
                    new Claim(ClaimTypes.Role,"user"),
                    //new Claim("SuperAdminOnly","true")
                };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _jwtSettings.Issuer,
                        _jwtSettings.Audience,
                        claims,
                        DateTime.Now, DateTime.Now.AddMinutes(_jwtSettings.TimeOut),
                        creds);
                    TokenUser _tokenUser = new TokenUser
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        user = a,
                        state = "0",
                        msg = "操作成功!"
                    };
                    //return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token)});
                    return Ok(_tokenUser);
                }
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = "-1",
                    msg = "非法操作！"
                });
            }
        }
    }
}