using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Hall:BaseEntity
    {
        public string HallName { get; set; }
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
        public List<Session> Sessions { get; set; }   
        public List<Seat> Seats { get; set; }   
    }
}
