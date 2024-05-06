using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;
using System.Data.Entity;

namespace Exam.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private StudentEntites _db;
        public StudentRepository(StudentEntites db) : base(db)
        {
            _db = db;
        }
    
        
        public void Update(Student obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }

        


    }
}
