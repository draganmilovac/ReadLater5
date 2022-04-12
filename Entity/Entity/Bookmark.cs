using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Bookmark
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string BookmarkUser { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; }
    }
}
