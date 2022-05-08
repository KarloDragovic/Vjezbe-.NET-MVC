using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Address { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(City))]
        public int? CityID { get; set; }
        public City? City { get; set; }
        [Range(0, 100, ErrorMessage = "Broj sati mora biti između 0 i 100.")]
        public int? WorkingExperience { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Meeting>? Meetings { get; set; }
    }
}
