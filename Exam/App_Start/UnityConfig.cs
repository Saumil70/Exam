using Exam.Models;
using Exam.Repository;
using Exam.Repository.IRepository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UserDetails.Repository;

namespace Exam
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<ICountryRepository, CountryRepository>();

            container.RegisterType<IStateRepository, StateRepository>();

            container.RegisterType<IContactInfoRepository, ContactInfoRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IUnitOfWork, UnitOfWork>();













            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}