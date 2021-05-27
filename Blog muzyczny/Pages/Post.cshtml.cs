using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Blog_muzyczny.Pages
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext _database;
        public int PostId;

        public Post Post { get; set; }


        [BindProperty]
        public Comment Comment { get; set; }


        public IEnumerable<Comment> Comments { get; set; }


        [TempData]
        public string Message { get; set; }


        public PostModel(ApplicationDbContext database)
        {
            _database = database;
        }

        public void OnGet(int id)
        {
            PostId = id;
            Post = _database.Posts.Find(id);
            Comments = _database.Comments.Where(x => x.PostId == id).ToList();
        }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            Comment.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                Message = "Dane niepoprawne";
                return Page();
            }

            _database.Comments.Add(Comment);
            await _database.SaveChangesAsync();

            Post = _database.Posts.Find(postId);
            Comments = _database.Comments.Where(x => x.PostId == postId).ToList();

            Message = "Twój komentarz został dodany";

            return Page();
        }

        public double GetAverage()
        {
            double avg = 0;

            foreach (var comment in Comments)
            {
                avg += comment.Rating;
            }

            avg /= Comments.Count();

            return Math.Round(avg, 2);
        }

        public async Task<IActionResult> OnPostDeleteComment(int id)
        {
            var comment = await _database.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            _database.Comments.Remove(comment);
            
            await _database.SaveChangesAsync();

            Message = "Usunięto pomyślnie.";

            Post = _database.Posts.Find(PostId);
            Comments = _database.Comments.Where(x => x.PostId == PostId).ToList();

            return RedirectToPage("Admin/Panel");
        }
    }
}