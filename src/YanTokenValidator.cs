using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Preoff
{
    /// <summary>
    /// ×Ô¶¨ÒåToken
    /// </summary>
    public class YanTokenValidator : ISecurityTokenValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public bool CanValidateToken => true;
        /// <summary>
        /// 
        /// </summary>
        public int MaximumTokenSizeInBytes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        public bool CanReadToken(string securityToken)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken=null;
            var identity=new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            if(securityToken=="abcdefg")
            {
                identity.AddClaim(new Claim("name","coreyan"));
                identity.AddClaim(new Claim("SuperAdminOnly","true"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType,"user"));
            } 
            var principal=new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
