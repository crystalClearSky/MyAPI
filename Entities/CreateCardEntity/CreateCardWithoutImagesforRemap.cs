using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.CreateCardEntity
{
    public class CreateCardWithoutImagesforRemap
    {
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Title { get; set; }
        // [MinLength(10)]
        // public string ImageUrl { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public bool IsSuperUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}