

using Exam;
using Exam.Models;
using System.Collections.Generic;

namespace Exam.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void Update(City obj);
      


    }
}
