namespace Preoff.Entity
{
    public partial class TokenUser
    {
        public string token { get; set; }
        public UserTable user { get; set; }
        public string state { get; set; }
        public string msg { get; set; }
    }
}
