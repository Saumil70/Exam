
using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;
using System.Linq.Expressions;



namespace UserDetails.Repository
{
    public class CountryRepository : Repository<Countries>, ICountryRepository
    {
        private StudentEntites _db;
        public CountryRepository(StudentEntites db) : base(db)
        {
            _db = db;
        }


        public void Update(Countries obj)
        {
            Update(obj);
        }

       


    }
}
