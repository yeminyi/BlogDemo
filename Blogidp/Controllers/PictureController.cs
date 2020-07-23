using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogIdp.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlogIdp.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PictureController(
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        private string GetClaimValue(string claimType)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim?.Value;
        }

        public IActionResult Index()
        {
            var picture = GetClaimValue(JwtClaimTypes.Picture);
            if (picture != null)
            {
                ViewData["picture"] = picture;
            }
            return View();
        }

        [HttpPost("UploadPicture")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is null");
            }

            if (file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("File size cannot exceed 10M");
            }

            var acceptTypes = new[] { ".jpg", ".jpeg", ".png" };
            if (acceptTypes.All(t => t != Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("File type not valid, only jpg and png are acceptable.");
            }

            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "avatars");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var userName = GetClaimValue(JwtClaimTypes.PreferredUserName);
            var fileName = $"{userName}.jpg";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var imageStream = file.OpenReadStream();

                using (var image = Image.Load(imageStream, out _))
                {
                    image.Mutate(c => c.Resize(100, 100));
                    image.SaveAsJpeg(stream);
                }
            }

            var user = await _userManager.FindByNameAsync(userName);
            var pictureClaims = User.Claims.Where(x => x.Type == JwtClaimTypes.Picture).ToList();
            if (pictureClaims.Any())
            {
                await _userManager.RemoveClaimsAsync(user, pictureClaims);
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Picture, fileName));
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            ViewData["picture"] = fileName;

            return View("Index");
        }
    }
}
