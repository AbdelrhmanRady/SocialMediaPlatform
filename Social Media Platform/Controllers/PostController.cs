using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media_Platform.Models;
using Social_Media_Platform.ViewModels;
using System.Security.Claims;

namespace Social_Media_Platform.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public PostController(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment
            ,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        } 

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            var user = await userManager.GetUserAsync(User);

            Posts post = new Posts()
            {
                User = user,
                PostContent = postViewModel.Content,
                CreatedAt = DateTime.UtcNow
            };
            if(postViewModel.Files != null)
            {
                List<PostMedia> mediaList = new();

                foreach(var file in postViewModel.Files)
                {
                    var uploadPath = Path.Combine(hostingEnvironment.WebRootPath, "PostsMedia");
                    var fileName = Guid.NewGuid() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadPath, fileName);
                    using(var stream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    mediaList.Add(new PostMedia() { MediaLocation = fileName });
                }
                post.Media = mediaList;
            }
            _context.Posts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Show","Profile");
        }

        public IActionResult AddComment()
        {
            return View();
        }
    }
}
