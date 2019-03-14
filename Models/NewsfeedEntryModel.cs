using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsfeedCoreMVC.Models
{
    public class NewsfeedEntryModel
    {
        [Required]
        [MinLength(1)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [StringLength(500, MinimumLength = 1)]
        [DisplayName("Text")]
        public string Text { get; set; }

        public IFormFile File { get; set; }
    }
}
