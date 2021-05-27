using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog_muzyczny.Pages.Admin
{
    public class PanelModel : PageModel
    {
        private readonly ApplicationDbContext _database;


        [TempData]
        public string Message { get; set; }

        public IList<Post> Posts { get; set; }

        public IList<Comment> Comments { get; set; }


        public PanelModel(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<IActionResult> OnGet()
        {
            Posts = await _database.Posts.ToListAsync();
            Comments = await _database.Comments.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePost(int id)
        {
            var post = await _database.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var comments = _database.Comments.Where(x => x.PostId == id).ToList();
            _database.Posts.Remove(post);

            foreach (var comment in comments)
            {
                _database.Comments.Remove(comment);
            }

            await _database.SaveChangesAsync();

            Message = "Usunięto pomyślnie.";

            return RedirectToPage("Panel");
        }
    }
}