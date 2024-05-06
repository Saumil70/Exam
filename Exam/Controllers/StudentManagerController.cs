
using Exam.Models;
using Exam.Repository.IRepository;
using Exam.StudentViewModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;


namespace UserDetailsPopup.Controllers
{
    public class StudentManagerController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public StudentManagerController(IUnitOfWork db)
        {
            _unitOfWork = db;
         
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentList()
        {
            var data = _unitOfWork.StudentRepository.GetAll(filter: s => s.IsActive == true, includeProperties: "Cities,States,Country,ContactInfo")
                            .Select(s => new StudentViewModel
                            {
                                StudentId = s.StudentId,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                DOB = s.DOB,
                                Salary = s.Salary,
                                StartingDate = s.StartingDate,
                                Position = s.Position,
                                IsActive = s.IsActive,
                                CountryId = s.CountryId,
                                StateId = s.StateId,
                                CityId = s.CityId,
                                Email = s.ContactInfo.Email,
                                PhoneNumber = s.ContactInfo.PhoneNumber,
                                CountryName = s.Country.CountryName,
                                StateName = s.States.StateName,
                                CityName = s.Cities.CityName
                            }).ToList();

            // Return partial view with the JSON data
            return PartialView("_StudentListPartial", data);
        }





        private int CalculateAge(DateTime dob)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - dob.Year;
            if (dob > currentDate.AddYears(-age)) // Check if the birthday has occurred this year
            {
                age--;
            }
            return age;
        }

        public JsonResult CountryList()
        {
            var data = _unitOfWork.CountryRepository.GetAll().ToList();
            return Json(data,JsonRequestBehavior.AllowGet);

        }
        public JsonResult StateList(int countryId)
        {
            var data = _unitOfWork.StateRepository.GetAll().Where(u => u.CountryId == countryId);
            return Json(data,JsonRequestBehavior.AllowGet); // Return states as JSON

        }
        public JsonResult CityList(int stateId)
        {
            var data = _unitOfWork.CityRepository.GetAll().Where(u => u.StateId == stateId);
            return Json(data,JsonRequestBehavior.AllowGet); // Return states as JSON

        }

        public JsonResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var data = _unitOfWork.StudentRepository.GetAll(filter: s => s.IsActive == true, includeProperties: "Cities,States,Country,ContactInfo")
            .Select(s => new
            {
                s.StudentId,
                s.FirstName,
                s.LastName,
                Age = CalculateAge(s.DOB),
                s.Salary,
                StartingDate = s.StartingDate.ToString("yyyy-MM-dd"),
                s.Position,
                s.IsActive,
                s.CountryId,
                s.StateId,
                s.CityId,
                s.ContactInfo.Email,
                s.ContactInfo.PhoneNumber,
                s.Country.CountryName,
                s.States.StateName,
                s.Cities.CityName
            }).ToList();



                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                decimal searchSalary;
                bool isSalary = decimal.TryParse(searchTerm, out searchSalary);

                var data = _unitOfWork.StudentRepository.GetAll(
                    u => (u.FirstName.Contains(searchTerm) ||
                          u.LastName.Contains(searchTerm) ||
                          u.Position.Contains(searchTerm) ||
                          u.Cities.CityName.Contains(searchTerm)) &&
                          u.IsActive &&
                         // Ensure non-null values for required fields
                         !string.IsNullOrEmpty(u.FirstName) &&
                         !string.IsNullOrEmpty(u.LastName) &&
                         !string.IsNullOrEmpty(u.Position) &&
                         !string.IsNullOrEmpty(u.Cities.CityName)   
                ).Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Position,
                    u.Cities.CityName,
                    Age = CalculateAge(u.DOB),
                    StartingDate = u.StartingDate.ToString("yyyy-MM-dd"),
                    u.Salary
                });

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult AddStudent(StudentViewModel studentViewModel)
        {
            if(ModelState.IsValid)
            {
                var contactInfo = new ContactInfo
                {
                    PhoneNumber = studentViewModel.PhoneNumber,
                    Email = studentViewModel.Email,
                };
                var student = new Student
                {
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                    DOB = studentViewModel.DOB,
                    Salary = studentViewModel.Salary,
                    StartingDate = studentViewModel.StartingDate,
                    Position = studentViewModel.Position,
                    IsActive = true,
                    CountryId = studentViewModel.CountryId,
                    CityId = studentViewModel.CityId,
                    StateId = studentViewModel.StateId,
                    
                };

                Session["FirstName"] = studentViewModel.FirstName;
                Session["LastName"] = studentViewModel.LastName;

                _unitOfWork.StudentRepository.Add(student);
                _unitOfWork.ContactInfoRepository.Add(contactInfo);
                _unitOfWork.Save();
                return Json(new { success = true, firstName = Session["FirstName"], lastName = Session["LastName"] });

            }
            else
            {
                return Json(new { success = false });

            }

        }
        public JsonResult Edit(int id)
        {
            var data = _unitOfWork.StudentRepository.GetAll(
                filter: s => s.StudentId == id,
                includeProperties: "Cities,States,Country,ContactInfo")
            .Select(s => new
            {
                s.StudentId,
                s.FirstName,
                s.LastName,
                s.Salary,
                StartingDate = s.StartingDate.ToString("yyyy-MM-dd"),
                DOB = s.DOB.ToString("yyyy-MM-dd"),
                s.Position,
                s.IsActive,
                s.CountryId,
                s.StateId,
                s.CityId,
                s.ContactInfoId,
                s.ContactInfo.Email,
                s.ContactInfo.PhoneNumber,
                s.Country.CountryName,
                s.States.StateName,
                s.Cities.CityName
            })
            .FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditStudent(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = _unitOfWork.StudentRepository.Get(
                    filter: s => s.StudentId == studentViewModel.StudentId, // Assuming StudentId is the primary key
                    includeProperties: "ContactInfo",
                    tracked: true // Track changes for updating
                );
                if (existingStudent != null)
                {
                    existingStudent.FirstName = studentViewModel.FirstName;
                    existingStudent.LastName = studentViewModel.LastName;
                    existingStudent.DOB = studentViewModel.DOB;
                    existingStudent.Salary = studentViewModel.Salary;
                    existingStudent.StartingDate = studentViewModel.StartingDate;
                    existingStudent.Position = studentViewModel.Position;
                    existingStudent.IsActive = true; // You can update IsActive if necessary
                    existingStudent.CountryId = studentViewModel.CountryId;
                    existingStudent.CityId = studentViewModel.CityId;
                    existingStudent.StateId = studentViewModel.StateId;

                    // Update the properties of the contactInfo entity
                    existingStudent.ContactInfo.PhoneNumber = studentViewModel.PhoneNumber;
                    existingStudent.ContactInfo.Email = studentViewModel.Email;


                    
                    _unitOfWork.Save();
                    
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
            
        }

        public JsonResult Delete(int[] ids)
        {
            foreach (int id in ids)
            {
                var student = _unitOfWork.StudentRepository.Get(c => c.StudentId == id);
                if (student != null)
                {
                    student.IsActive = false; // Soft delete by setting IsActive to false
                    _unitOfWork.StudentRepository.Update(student);
                }
            }
            _unitOfWork.Save();
            return Json("Selected users deleted successfully");
        }

    }
}
