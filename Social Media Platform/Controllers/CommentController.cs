using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Platform.Models;
using Social_Media_Platform.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace Social_Media_Platform.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(UserManager<ApplicationUser> userManager,AppDbContext context) {
            UserManager = userManager;
            this._context = context;
        }

        public UserManager<ApplicationUser> UserManager { get; }

        [HttpPost]
        public async Task<IActionResult> AddComment(PostPageViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var x = 5;
                //Comments comment = new()
                //{
                //    CommenterId = ,
                //    CommentContent = model.CommentContent,
                //    CreatedAt = DateTime.UtcNow
                //};
                //if(model.PostId != null && model.ParentCommentId == null)
                //{
                //    comment.PostId = (int)model.PostId;
                //}
                //else if(model.ParentCommentId != null && model.PostId == null)
                //{
                //    comment.ParentCommentId = (int) model.ParentCommentId;
                //}
                //else
                //{

                //}

                //_context.Comments.Add(comment);
                //_context.SaveChanges();

            }
            return RedirectToAction("Show", "Profile", model);
        }
    }
}
