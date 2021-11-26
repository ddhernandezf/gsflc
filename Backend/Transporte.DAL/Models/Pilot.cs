using System;
using System.Collections.Generic;

namespace Transporte.DAL.Models
{
    public partial class Pilot
    {
        public byte Id { get; set; }
        public bool? IsMale { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime BornDate { get; set; }
        public string CompleteName { get; set; }
    }
}
