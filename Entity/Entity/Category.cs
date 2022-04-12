using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public int? BookmarkId { get; set; }
    }
}
