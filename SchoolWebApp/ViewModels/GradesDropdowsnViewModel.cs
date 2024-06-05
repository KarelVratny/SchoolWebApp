using SchoolWebApp.Models;

namespace SchoolWebApp.ViewModels {
    public class GradesDropdowsnViewModel {
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }

        public GradesDropdowsnViewModel() {
            Subjects = new List<Subject>();
            Students = new List<Student>();
        }
    }
}
