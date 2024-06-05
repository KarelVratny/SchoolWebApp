using SchoolWebApp.Models;

namespace SchoolWebApp.Services {
    public class GradeService {
        private ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context) {
            _context = context;
        }
    }
}
