using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Language: BaseEntity
    { 
       public string LanguageName { get; set; }
        public string IconUrl { get; set; }
        public List<FilmLanguage> FilmLanguages { get; set; }
    }
}
