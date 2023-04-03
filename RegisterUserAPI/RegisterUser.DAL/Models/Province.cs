using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterUser.DAL.Models
{
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<UserDetail> Users { get; set; }
    }
}
