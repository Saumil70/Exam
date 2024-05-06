using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;

namespace UserDetails.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private StudentEntites _db;
        public StateRepository(StudentEntites db) : base(db)
        {
            _db = db;
        }
        public void Update(State obj)
        {
            Update(obj);
        }


    }
}
