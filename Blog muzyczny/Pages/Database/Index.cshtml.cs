using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog_muzyczny.Pages.Database
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public IndexModel(ApplicationDbContext database)
        {
            _database = database;
        }

        [TempData]
        public string Message { get; set; }

        public IList<Post> Posts { get; set; }

        public IList<Comment> Comments { get; set; }


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

            _database.Posts.Remove(post);

            await _database.SaveChangesAsync();

            Message = "Usunięto pomyślnie.";

            return RedirectToPage("Index");
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

            return RedirectToPage("Index");
        }

    }
}