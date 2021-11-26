using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class RoleAction
    {
        public byte Action { get; set; }
        public byte Role { get; set; }

        public virtual Action ActionNavigation { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}
