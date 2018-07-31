namespace Preoff.Entity
{
    /// <summary>
    /// Jwt认证类
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer{get;set;}
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience{get;set;}
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey{get;set;}
        /// <summary>
        /// 超时时间（分钟）
        /// </summary>
        public double TimeOut { get; set; } 
    }
}