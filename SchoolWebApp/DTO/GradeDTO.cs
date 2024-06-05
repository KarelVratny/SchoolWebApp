using SchoolWebApp.Models;

namespace SchoolWebApp.DTO {
    public class GradeDTO {
        public int Id { get; set; }
        public Student StudentId { get; set; }
        public Subject SubjectId { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }
    }
}
