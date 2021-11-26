using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class User
    {
        public User()
        {
            RoleUser = new HashSet<RoleUser>();
            Transaction = new HashSet<Transaction>();
        }

        public byte Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompleteName { get; set; }
        public bool ResetPassword { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<RoleUser> RoleUser { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
