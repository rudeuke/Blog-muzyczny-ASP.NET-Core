using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_muzyczny.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Podaj wartość (1-5)")]
        [Display(Name = "Ocena")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Treść")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public int PostId { get; set; }

        //public virtual Post Post { get; set; }
    }
}
