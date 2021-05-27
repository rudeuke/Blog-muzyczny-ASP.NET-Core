using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Blog_muzyczny.Pages
{
    public class PostListModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public IList<Post> Posts { get; set; }

        [BindProperty]
        public string SearchString { get; set; }


        public PostListModel(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<IActionResult> OnGet()
        {
            Posts = await _database.Posts.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSort(string sortType)
        {
            switch (sortType)
            {
                case "date":
                    Posts = await _database.Posts.OrderBy(s => s.Date).ToListAsync();
                    break;

                case "title":
                    Posts = await _database.Posts.OrderBy(s => s.Title).ToListAsync();
                    break;

                case "author":
                    Posts = await _database.Posts.OrderBy(s => s.Author).ToListAsync();
                    break;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSearch()
        {
            Posts = await _database.Posts.ToListAsync();

            if (!String.IsNullOrEmpty(SearchString))
            {
                List<Post> temp = new List<Post>();
                foreach (var post in Posts)
                {
                    if (post.Title.Contains(SearchString) || post.Author.Contains(SearchString))
                    {
                        temp.Add(post);
                    }
                }
                Posts = temp;
            }
            return Page();
        }
    }
}