using Exam;
using Exam.Models;

namespace Exam.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        void Update(Student obj);



    }
}
