using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool DeletedAt { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
