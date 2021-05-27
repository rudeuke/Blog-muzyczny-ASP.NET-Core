using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog_muzyczny.Data;
using Blog_muzyczny.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog_muzyczny.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _database;
        private IList<Post> posts;

        public Post Post { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext database)
        {
            _logger = logger;
            _database = database;
        }

        public async Task<IActionResult> OnGet()
        {
            posts = await _database.Posts.ToListAsync();
            Post = posts.Last();
            return Page();
        }
    }
}
