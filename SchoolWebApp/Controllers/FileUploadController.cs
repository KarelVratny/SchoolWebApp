using Microsoft.AspNetCore.Mvc;

namespace SchoolWebApp.Controllers {
	public class FileUploadController : Controller {
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file) {
			if (file.Length > 0) { 
			
			}
			return View();
		}
	}
}
