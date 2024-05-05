using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Seat: BaseEntity
    {
        public bool isReserved { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
    }
}
