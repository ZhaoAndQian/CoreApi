
namespace Preoff.Entity
{
    public partial class StreamVideoServerTable
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string ServerAddr { get; set; }
        public string ServerPort { get; set; }
        public string PushName { get; set; }
        public string PushPwd { get; set; }
        public string PlayName { get; set; }
        public string PlayPwd { get; set; }
    }
}
