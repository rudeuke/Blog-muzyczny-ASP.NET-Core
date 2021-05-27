using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_muzyczny.Pages.Admin
{
    public class EditPostModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public EditPostModel(ApplicationDbContext database)
        {
            _database = database;
        }

        [BindProperty]
        public Post Post { get; set; }

        [TempData]
        public string Message { get; set; }


        public async Task OnGet(int id)
        {
            Post = await _database.Posts.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentPost = await _database.Posts.FindAsync(Post.Id);

            currentPost.Author = Post.Author;
            currentPost.Title = Post.Title;
            currentPost.Text = Post.Text;
            currentPost.Date = DateTime.Now;

            await _database.SaveChangesAsync();

            Message = "Pomyślnie zaktualizowano wpis.";

            return RedirectToPage("Panel");
        }
    }
}