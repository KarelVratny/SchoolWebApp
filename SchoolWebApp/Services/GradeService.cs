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
				Student =await _context.Students.FirstOrDefaultAsync(student => student.Id == gradeDTO.StudentId),
				Subject =await _context.Subjects.FirstOrDefaultAsync(subject => subject.Id == gradeDTO.SubjectId),
			};
			await _context.AddAsync(gradeToInsert);
			await _context.SaveChangesAsync();
		}
	}
}
