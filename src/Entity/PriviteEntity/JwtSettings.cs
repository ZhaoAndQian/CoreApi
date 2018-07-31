namespace Preoff.Entity
{
    /// <summary>
    /// Jwt��֤��
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// ������
        /// </summary>
        public string Issuer{get;set;}
        /// <summary>
        /// ������
        /// </summary>
        public string Audience{get;set;}
        /// <summary>
        /// ��Կ
        /// </summary>
        public string SecretKey{get;set;}
        /// <summary>
        /// ��ʱʱ�䣨���ӣ�
        /// </summary>
        public double TimeOut { get; set; } 
    }
}