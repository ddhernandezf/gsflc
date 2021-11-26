using System.Collections.Generic;

namespace Transporte.Model.Security
{
    public class UserAction
    {
        public byte id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public short order { get; set; }
        public List<UserAction> items { get; set; }
    }
}
