

using Exam.Models;
using Exam.Repository.IRepository;



namespace Exam.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private StudentEntites _db;
        public CityRepository(StudentEntites db) : base(db)
        {
            _db = db;
        }
        public void Update(City obj)
        {
            Update(obj);
        }


    }
}
