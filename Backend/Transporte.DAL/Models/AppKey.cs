using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class AppKey
    {
        public byte Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
    }
}
