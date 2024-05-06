
using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;
using System.Linq.Expressions;



namespace UserDetails.Repository
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        private StudentEntites _db;
        public ContactInfoRepository(StudentEntites db) : base(db)
        {
            _db = db;
        }


        public void Update(ContactInfo obj)
        {
            Update(obj);
        }

       


    }
}
