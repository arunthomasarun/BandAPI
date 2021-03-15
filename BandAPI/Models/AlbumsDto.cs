using BandAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class AlbumsDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey(nameof(BandId))]
        public Band Band { get; set; }
        public Guid BandId { get; set; }
    }
}
