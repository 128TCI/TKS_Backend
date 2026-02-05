using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.DTO.FileSetUp.Process.Device
{
    public class CoordinatesSetUpDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public decimal? Distance { get; set; }
    }
}
