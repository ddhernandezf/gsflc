using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class RoleUser
    {
        public byte User { get; set; }
        public byte Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
