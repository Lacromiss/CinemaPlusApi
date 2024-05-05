using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Cinema: BaseEntity
    {
        public string CinemaName { get; set; }
        public List<Hall> Halls { get; set; }
    }
}
