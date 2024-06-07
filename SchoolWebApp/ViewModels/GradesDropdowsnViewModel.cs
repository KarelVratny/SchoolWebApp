using SchoolWebApp.Models;

namespace SchoolWebApp.ViewModels {
    public class GradesDropdowsnViewModel {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }

        public GradesDropdowsnViewModel() {
            Subjects = new List<Subject>();
            Students = new List<Student>();
        }
    }
}
