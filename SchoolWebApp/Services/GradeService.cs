using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using SchoolWebApp.ViewModels;

namespace SchoolWebApp.Services {
	public class GradeService {
		private ApplicationDbContext _context;

		public GradeService(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<GradesDropdowsnViewModel> GetGradesDropdowsData() {
			return new GradesDropdowsnViewModel() {
				Students = await _context.Students.OrderBy(student => student.LastName).ToListAsync(),
				Subjects = await _context.Subjects.OrderBy(subject => subject.Name).ToListAsync(),
			};
		}

		internal async Task CreateAsync(GradeDTO gradeDTO) {
			Grade gradeToInsert = new Grade() {
				Date = DateTime.Today,
				Mark = gradeDTO.Mark,
				Topic = gradeDTO.Topic,
				Student = _context.Students.FirstOrDefault(student => student.Id == gradeDTO.StudentId.Id),
				Subject = _context.Subjects.FirstOrDefault(subject => subject.Id == gradeDTO.SubjectId.Id),
			};
			await _context.SaveChangesAsync();
		}
	}
}
