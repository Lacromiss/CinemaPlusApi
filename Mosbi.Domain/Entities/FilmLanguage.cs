using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class FilmLanguage: BaseEntity
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
