using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleAction = new HashSet<RoleAction>();
            RoleUser = new HashSet<RoleUser>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleAction> RoleAction { get; set; }
        public virtual ICollection<RoleUser> RoleUser { get; set; }
    }
}
