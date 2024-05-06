
using Exam;
using Exam.Models;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Exam.Repository.IRepository
{
    public interface IContactInfoRepository : IRepository<ContactInfo>
    {
        void Update(ContactInfo obj);

    }
}
