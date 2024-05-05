using Microsoft.AspNetCore.Http;
using Mosbi.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Domain.Entities
{
    public class Film: BaseEntity
    {
        public string MovieName { get; set; }
        public string MovieAge { get; set; }
        public string MovieImg { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string ImagePath { get; set; }
        public List<FilmLanguage>? FilmLanguages { get; set; }

    }
}
