using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
	public class GradesController : Controller {
		private GradeService _gradeService;

		public GradesController(GradeService gradeService) {
			_gradeService = gradeService;
		}

		public IActionResult Index() {
			return View();
		}
		public async Task<IActionResult> CreateAsync() {
			var gradesDropdowsData = await _gradeService.GetGradesDropdowsData();
			ViewBag.Students = new SelectList(gradesDropdowsData.Students, "Id", "LastName");
			ViewBag.Subjects = new SelectList(gradesDropdowsData.Subjects, "Id", "Name");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(GradeDTO gradeDTO) {
			await _gradeService.CreateAsync(gradeDTO);
			return RedirectToAction("Index");
		}
	}
}
