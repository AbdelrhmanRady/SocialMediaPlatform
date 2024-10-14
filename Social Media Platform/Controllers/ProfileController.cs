using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media_Platform.Models;
using Social_Media_Platform.Services;
using Social_Media_Platform.ViewModels;

namespace Social_Media_Platform.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMailingService mailingService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailingService = mailingService;
            this.hostingEnvironment = hostingEnvironment;
            this._context = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            ProfileViewModel model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.gender,
                Birthday = user.Birthday,
                ImagePath = user.ImagePath,
                City = user.City,
                Biography = user.Biography
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.City = model.City;
                user.Birthday = (DateTime)model.Birthday;
                user.gender = (Gender) model.Gender;
                user.Biography = model.Biography;

                if(user.ImagePath != model.ImagePath)
                {
                    var newImagePath = ProcessImageAndReturnPath(model.ImagePath);
                    System.IO.File.Delete(Path.Combine(hostingEnvironment.WebRootPath,"Images",user.ImagePath));
                    user.ImagePath = newImagePath;
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Edit", "Profile");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddPicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPicture(PhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var imagePath = ProcessImageAndReturnPath(model.photoBase64);
                user.ImagePath = imagePath;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        private string ProcessImageAndReturnPath(string base64IMG)
        {
            var base64 = base64IMG.Split(",")[1];
            byte[] imageBytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(imageBytes))
            {
                var fileName = Guid.NewGuid() + ".jpeg";

                IFormFile file = new FormFile(ms, 0, ms.Length, "image", fileName);

                var uploadPath = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                var filePath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return fileName;
            }
        }

        public async Task<IActionResult> Show()
        {
            var postslist =  await _context.Posts
                .Include(p => p.Media)
                .Include(p=>p.User)
                .Include(p => p.Comments)
                    .ThenInclude(C => C.SubComments)
                    .ThenInclude(C => C.User)
                .Include(p=>p.Comments)
                    .ThenInclude(C=>C.User)
                .Include(p => p.Reactions)
                    .ThenInclude(R => R.User)
                .ToListAsync();

            PostPageViewModel model = new PostPageViewModel()
            {
                Posts = postslist
            };

            return View(model);
        }






    }
}
