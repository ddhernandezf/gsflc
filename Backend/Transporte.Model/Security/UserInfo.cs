namespace Transporte.Model.Security
{
    public class UserInfo
    {
        public string email { get; set; }
        public string name { get; set; }
        public Role role { get; set; }
        public bool reset { get; set; }
    }
}
