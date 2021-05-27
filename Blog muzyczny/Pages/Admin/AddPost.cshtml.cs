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
    public class AddPostModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public AddPostModel(ApplicationDbContext database)
        {
            _database = database;
        }

        [BindProperty]
        public Post Post { get; set; }

        [TempData]
        public string Message { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Post.Date = DateTime.Now;

            _database.Posts.Add(Post);
            await _database.SaveChangesAsync();

            Message = "Pomyślnie dodano wpis.";

            return RedirectToPage("Panel");
        }
    }
}