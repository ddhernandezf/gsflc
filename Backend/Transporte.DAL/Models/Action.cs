using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Action
    {
        public Action()
        {
            InverseParentNavigation = new HashSet<Action>();
            RoleAction = new HashSet<RoleAction>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public bool IsGroup { get; set; }
        public byte? Parent { get; set; }
        public short Order { get; set; }

        public virtual Action ParentNavigation { get; set; }
        public virtual ICollection<Action> InverseParentNavigation { get; set; }
        public virtual ICollection<RoleAction> RoleAction { get; set; }
    }
}
