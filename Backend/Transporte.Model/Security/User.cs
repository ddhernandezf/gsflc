namespace Transporte.Model.Security
{
    public class User
    {
        public byte? id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public bool reset { get; set; }
        public bool active { get; set; }
        public Role role { get; set; }
    }
}
