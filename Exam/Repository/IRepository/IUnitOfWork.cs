using Exam.Repository.IRepository;

namespace Exam.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICountryRepository CountryRepository { get; }

        IStateRepository StateRepository { get; }
        ICityRepository CityRepository { get; }

        IStudentRepository StudentRepository { get; }

        IContactInfoRepository ContactInfoRepository { get; }
        void Save();
    }
}
