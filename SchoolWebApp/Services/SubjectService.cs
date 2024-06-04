using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolWebApp.DTO;
using SchoolWebApp.Migrations;
using SchoolWebApp.Models;

namespace SchoolWebApp.Services {
	public class SubjectService {
		private ApplicationDbContext _dbContext;

		public SubjectService(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IEnumerable<SubjectDTO> GetSubjects() {
			var allSubjects = _dbContext.Subjects;
			var subjectsDtos = new List<SubjectDTO>();
			foreach (var subject in allSubjects) {
				subjectsDtos.Add(ModelToDto(subject));
			}
			return subjectsDtos;
		}

		public async Task AddSubjectAsync(SubjectDTO subjectDto) {
			await _dbContext.Subjects.AddAsync(DtoToModel(subjectDto));
			await _dbContext.SaveChangesAsync();
		}
		internal async Task<SubjectDTO> GetByIdAsync(int id) {
			var subject = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
			if (subject == null) {
				return null;
			}
			return ModelToDto(subject);
		}

		private static SubjectDTO ModelToDto(Subject subject) {
			return new SubjectDTO {
				Name = subject.Name,
				Id = subject.Id
			};
		}

		private static Subject DtoToModel(SubjectDTO subjectDto) {
			return new Subject {
				Name = subjectDto.Name,
				Id = subjectDto.Id
			};
		}

		//internal async Task UpdateAsync(int id, SubjectDTO subjectDto) {
		internal async Task UpdateAsync(SubjectDTO subjectDto) {
			_dbContext.Subjects.Update(DtoToModel(subjectDto));
			await _dbContext.SaveChangesAsync();
		}

		internal async Task DeleteAsync(int id) {
			var subjectToDelete = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
			//if (subjectToDelete == null) {
			//	return null;
			//}
			_dbContext.Subjects.Remove(subjectToDelete);
			await _dbContext.SaveChangesAsync();
		}
	}
}
