using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
	[Authorize]
	public class GradesController : Controller {
		private GradeService _gradeService;

		public GradesController(GradeService gradeService) {
			_gradeService = gradeService;
		}

		//public IActionResult Index() {
		//	return View();
		//}
		public async Task<IActionResult> CreateAsync() {
			await FillSelectsAsync();
			return View();
		}

		private async Task FillSelectsAsync() {
			var gradesDropdowsData = await _gradeService.GetGradesDropdowsData();
			ViewBag.Students = new SelectList(gradesDropdowsData.Students, "Id", "LastName");
			ViewBag.Subjects = new SelectList(gradesDropdowsData.Subjects, "Id", "Name");
		}

		[HttpPost]
		public async Task<IActionResult> Create(GradeDTO gradeDTO) {
			await _gradeService.CreateAsync(gradeDTO);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Index() {
			var allGrades = await _gradeService.GetAllViewModelsAsync();
			return View(allGrades);
		}

		public async Task<IActionResult> Update(int id) {
			var gradeToEdit = await _gradeService.GetByIdAsync(id);
			if (gradeToEdit == null) {
				return View("NotFound");
			}
			var gradeDto = _gradeService.ModelToDto(gradeToEdit);
			await FillSelectsAsync();
			return View(gradeDto);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int id, GradeDTO grade) {
			await _gradeService.UpdateAsync(id, grade);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAsync(int id) {
			await _gradeService.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}
}
