using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encontros.Models
{
    public class FotoEncontro
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Length { get; set; }

        public string Type { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        private DateTime? createdDate;

        [Required]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        public int EncontroId { get; set; }

    }
}
