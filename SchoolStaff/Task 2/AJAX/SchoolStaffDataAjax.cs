using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Business.Repositories;
using DAL;
using DAL.Entities;

namespace Presentation.AJAX
{
    public class SchoolStaffDataAjax: ISchoolStaffDataAjax
    {
        readonly IRepository<SchoolStaff> _repos;
        private readonly ApplicationDbContext _context;
        //readonly IRepository<SchoolStaffPhone> _reposPhone;

        public SchoolStaffDataAjax(IRepository<SchoolStaff> repos,
            ApplicationDbContext context
            //IRepository<SchoolStaffPhone> reposPhone
            )
        {
            _repos = repos;
            _context = context;
            //_reposPhone = reposPhone;
        }

        public IEnumerable<SchoolStaffSentJson> RetAjax(string name)
        {
            var deserializedUser = "";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(name));
            var ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as string;
            ms.Close();
            name = deserializedUser;
            IEnumerable<SchoolStaffSentJson> selectedSchoolStaff;

            if (name == "")
            {
                selectedSchoolStaff = (IEnumerable<SchoolStaffSentJson>)(from t in _repos.GetAll()
                                                               orderby t.Id
                                                               select new SchoolStaffSentJson
                                                               {
                                                                   Id = t.Id,
                                                                   Surname = t.Surname,
                                                                   Name = t.Name,
                                                                   Patronymic = t.Patronymic,
                                                                   DateOfBirth = t.DateOfBirth.ToString("dd/MM/yyyy"),
                                                                   Email = t.Email,
                                                                   IsExist = true,
                                                                   SchoolStaffPhones = _context.SchoolStaffPhones //_reposPhone.GetAll()
                                                                       .Where(c => c.SchoolStaffId == t.Id && c.Phone.ToUpper().Contains(name.ToUpper())).ToList()
                                                               });
            }
            else
            {
                selectedSchoolStaff = (IEnumerable<SchoolStaffSentJson>)(from t in _repos.GetAll()
                                                               orderby t.Id
                                                               select RetAjax(t, name, _context.SchoolStaffPhones //_reposPhone.GetAll()
                                                                   .Where(c => c.SchoolStaffId == t.Id && c.Phone.ToUpper().Contains(name.ToUpper())).ToList()));
            }
            return selectedSchoolStaff;
        }

        SchoolStaffSentJson RetAjax(SchoolStaff schoolStaff, string pName, List<SchoolStaffPhone> lssp)
        {
            //List<SchoolStaffPhone> lssp = _reposPhone.GetAll()
            //    .Where(c => c.SchoolStaffId == schoolStaff.Id && c.Phone == pName).ToList();

            var bisExist = (
                (schoolStaff.Surname.ToUpper().Contains(pName.ToUpper())) || 
                (schoolStaff.Email.ToUpper().Contains(pName.ToUpper())) ||
                (schoolStaff.Name.ToUpper().Contains(pName.ToUpper())) || 
                (schoolStaff.Patronymic.ToUpper().Contains(pName.ToUpper())) || 
                (lssp.Count()>0)
                );

            return new SchoolStaffSentJson
            {
                Id = schoolStaff.Id,
                Surname = bisExist ? schoolStaff.Surname : "",
                Name = bisExist ? schoolStaff.Name : "",
                Patronymic = bisExist ? schoolStaff.Patronymic : "",
                DateOfBirth = bisExist ? schoolStaff.DateOfBirth.ToString("dd/MM/yyyy") : "",
                Email = bisExist ? schoolStaff.Email : "",
                IsExist = bisExist,
                SchoolStaffPhones = lssp
            };
        }
    }
}