using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Session : BaseEntity
    {
        public double Price { get; set; }
        public int FilmLanguageId { get; set; }
        public FilmLanguage? FilmLanguage { get; set; }
        public int HallId { get; set; }
        public Hall? Hall { get; set; }

    }

}
