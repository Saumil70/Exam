
using Exam;
using Exam.Models;


namespace Exam.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        void Update(State obj);

    }
}
