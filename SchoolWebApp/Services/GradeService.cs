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

		internal async Task CreateAsync(GradeDTO gradeDto) {
			Grade gradeToInsert = await DtoToModel(gradeDto);
			await _context.AddAsync(gradeToInsert);
			await _context.SaveChangesAsync();
		}

		private async Task<Grade> DtoToModel(GradeDTO gradeDTO) {
			return new Grade() {
				Id = gradeDTO.Id,
				Date = DateTime.Today,
				Mark = gradeDTO.Mark,
				Topic = gradeDTO.Topic,
				Student = await _context.Students.FirstOrDefaultAsync(student => student.Id == gradeDTO.StudentId),
				Subject = await _context.Subjects.FirstOrDefaultAsync(subject => subject.Id == gradeDTO.SubjectId),
			};
		}

		public async Task<IEnumerable<GradesViewModel>> GetAllViewModelsAsync() {
			List<Grade> grades = await _context.Grades.Include(grade => grade.Student).Include(grade => grade.Subject).ToListAsync();
			List<GradesViewModel> gradeViewModels = new List<GradesViewModel>();
			foreach (Grade grade in grades) {
				gradeViewModels.Add(new GradesViewModel {
					Date = grade.Date,
					Id = grade.Id,
					Mark = grade.Mark,
					StudentName = $"{grade.Student.LastName} {grade.Student.FirstName}",
					SubjectName = grade.Subject.Name,
					Topic = grade.Topic
				});
			}
			return gradeViewModels;
		}

		internal async Task<Grade> GetByIdAsync(int id) {
			return await _context.Grades.Include(grade => grade.Student).Include(grade => grade.Subject).FirstOrDefaultAsync(grade => grade.Id == id);
		}

		internal GradeDTO ModelToDto(Grade grade) {
			return new GradeDTO {
				Id = grade.Id,
				Date = grade.Date,
				Mark = grade.Mark,
				StudentId = grade.Student.Id,
				SubjectId = grade.Subject.Id,
				Topic = grade.Topic
			};
		}

		internal async Task UpdateAsync(int id, GradeDTO gradeDto) {
			Grade gradeToUpdate = await DtoToModel(gradeDto);
			_context.Grades.Update(gradeToUpdate);
			await _context.SaveChangesAsync();
		}

		internal async Task DeleteAsync(int id) {
			var gradeToDelete = await _context.Grades.FirstOrDefaultAsync(grade => grade.Id == id);
			_context.Grades.Remove(gradeToDelete);
			_context.SaveChanges();
		}
	}
}
