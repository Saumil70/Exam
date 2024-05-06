using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;

namespace UserDetails.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private StudentEntites _db;
        public ICountryRepository CountryRepository { get; private set; }

        public IStateRepository StateRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public IStudentRepository StudentRepository { get; private set; }

        public IContactInfoRepository ContactInfoRepository { get; private set; }
        public UnitOfWork(StudentEntites db)
        {
            _db = db;
            CountryRepository = new CountryRepository(_db);
            StateRepository = new StateRepository(_db);
            CityRepository = new CityRepository(_db);
            StudentRepository = new StudentRepository(_db);
            ContactInfoRepository = new ContactInfoRepository(_db);

        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
