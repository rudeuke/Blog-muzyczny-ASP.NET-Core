using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_muzyczny.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Treść")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }


        //public virtual List<Comment> Comments { get; set; }
    }
}
