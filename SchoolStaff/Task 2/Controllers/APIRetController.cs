using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Business.API;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class APIRetController : ControllerBase
    {
        private readonly IAPIProfessionArrayRepozitory _iAPIProfessionArrayRepozitory;
        private readonly IAPISchoolStaffListRepozitory _iAPISchoolStaffListRepozitory;
        private readonly IAPISchoolStaffRepozitory _iAPISchoolStaffRepozitory;
        private readonly IAPISubjecArrayRepozitory _iAPISubjecMassRepozitory;

        public APIRetController(
            IAPIProfessionArrayRepozitory iAPIProfessionArrayRepozitory,
            IAPISchoolStaffListRepozitory iAPISchoolStaffListRepozitory,
            IAPISchoolStaffRepozitory iAPISchoolStaffRepozitory,
            IAPISubjecArrayRepozitory iAPISubjecMassRepozitory
            )
        {
            _iAPIProfessionArrayRepozitory = iAPIProfessionArrayRepozitory;
            _iAPISchoolStaffListRepozitory = iAPISchoolStaffListRepozitory;
            _iAPISchoolStaffRepozitory = iAPISchoolStaffRepozitory;
            _iAPISubjecMassRepozitory = iAPISubjecMassRepozitory;
        }
        
        [HttpGet]
        [Route("SchoolStaffById")]
        public ApiSchoolStaff GetAll(int id)
        {
            return _iAPISchoolStaffRepozitory.ReturnAPISchoolStaffArray(id);
        }
        
        [HttpGet]
        [Route("SchoolStaffAll")]
        public List<ApiSchoolStaff> GetAllRecord()
        {
            return _iAPISchoolStaffListRepozitory.ReturnAPISchoolStaffList();
        }
        
        [HttpPost]
        [FormatFilter]
        [Route("ProfessionByName")]
        public ApiProfession[] GetProfessionMass(string  name)
        {
            return _iAPIProfessionArrayRepozitory.ReturnAPIProfessionArray(name);
        }
        
        [HttpGet]
        [Route("SubjecByName")]
        public ApiSubject[] GetSubjecMass(string name)
        {
            return _iAPISubjecMassRepozitory.ReturnAPISubjecArray(name);
        }
    }
}