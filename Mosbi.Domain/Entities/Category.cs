using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

    }
}
