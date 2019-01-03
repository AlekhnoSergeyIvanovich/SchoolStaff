using System;
using System.Linq;
using System.Threading.Tasks;
using Business.MainPhone;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NLog;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    public class SchoolStaffPhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly ICheckedPhone _checkedPhone;

        public SchoolStaffPhonesController(
            ApplicationDbContext context,
            ICheckedPhone checkedPhone
        )
        {
            _context = context;
            _checkedPhone = checkedPhone;
            _logger = LogManager.GetCurrentClassLogger();
        }

        // GET: SchoolStaffPhones
        public ActionResult Index(int idSchoolStaffInd)
        {
            var listRepositorySchoolStaffPhone =
                _context.SchoolStaffPhones.Where(c => c.SchoolStaffId == idSchoolStaffInd);
            if (listRepositorySchoolStaffPhone == null)
            {
                throw new ApplicationException($"Unable to load listOfSchoolPhone.");
            }

            ViewBag.idSchoolStaff = idSchoolStaffInd;
            ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaffInd).PrimaryPhoneId;

            if (
                (listRepositorySchoolStaffPhone.Count() == 1) && 
                (listRepositorySchoolStaffPhone.First().Id != _context.SchoolStaffs.Find(idSchoolStaffInd).PrimaryPhoneId)
                )
            {
                _checkedPhone.WritePhone(idSchoolStaffInd, listRepositorySchoolStaffPhone.First().Id);
            }

            return View(listRepositorySchoolStaffPhone);
        }

        // GET: SchoolStaffPhones/Details/5
        public ActionResult Details(int id, int idSchoolStaff)
        {
            try
            {
                var repositorySchoolStaffPhone = _context.SchoolStaffPhones.Find(id);
                ViewBag.idSchoolStaff = idSchoolStaff;
                ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;
                return View(repositorySchoolStaffPhone);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable show Link SchoolStaff-Phones.");
                throw new ApplicationException($"Unable to load listOfSchoolPhone.");
            }
        }

        // GET: SchoolStaffPhones/Create
        public ActionResult Create(int idSchoolStaff)
        {
            ViewBag.idSchoolStaff = idSchoolStaff;
            return View();
        }

        // POST: SchoolStaffPhones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SchoolStaffPhone staffPhone, int idSchoolStaff)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    staffPhone.SchoolStaffId = idSchoolStaff;
                    ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;
                    await _context.AddAsync(staffPhone);

                    return RedirectToAction(nameof(Index), new {idSchoolStaffInd = idSchoolStaff});
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create Link SchoolStaff-Phones.");
                return View();
            }
        }

        // GET: SchoolStaffPhones/Edit/5
        public ActionResult Edit(int id, int idSchoolStaff)
        {
            SchoolStaffPhone schoolStaffPhone = null;
            try
            {
                // TODO: Add delete logic here
                ViewBag.idSchoolStaff = idSchoolStaff;
                ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;
                schoolStaffPhone = _context.SchoolStaffPhones.First(c => c.Id == id);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show form edit Link SchoolStaff-Phones.");
                return View(schoolStaffPhone);
            }

            return View(schoolStaffPhone);
        }

        // POST: SchoolStaffPhones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SchoolStaffPhone schoolStaffPhone, int idSchoolStaff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;
                    _context.Entry(schoolStaffPhone).State = EntityState.Modified;
                    return RedirectToAction(nameof(Index), new {idSchoolStaffInd = idSchoolStaff});
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to edit Link SchoolStaff-Phones.");
                return View();
            }
        }

        // GET: SchoolStaffPhones/Delete/5
        public ActionResult Delete(int id, int idSchoolStaff)
        {
            SchoolStaffPhone schoolStaffPhone = null;
            try
            {
                schoolStaffPhone = _context.SchoolStaffPhones.Find(id);
                ViewBag.idSchoolStaff = idSchoolStaff;
                ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show form delete Link SchoolStaff-Phones.");
                return View();
            }

            return View(schoolStaffPhone);
        }

        // POST: SchoolStaffPhones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SchoolStaffPhone staff, int idSchoolStaff)
        {
            // TODO: Add delete logic here
            SchoolStaffPhone schoolStaffPhone = null;
            try
            {
                schoolStaffPhone = _context.SchoolStaffPhones.Find(id);
                ViewBag.idPhone = _context.SchoolStaffs.Find(idSchoolStaff).PrimaryPhoneId;

                if (schoolStaffPhone.Id == ViewBag.idPhone)
                {
                    _checkedPhone.WritePhone(idSchoolStaff, null);
                }

                _context.SchoolStaffPhones.Remove(schoolStaffPhone);

                return RedirectToAction(nameof(Index), "SchoolStaffPhones", new RouteValueDictionary(
                    new {controller = "SchoolStaffPhones", action = nameof(Index), idSchoolStaffInd = idSchoolStaff}));
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to delete Link SchoolStaff-Phones.");
                return View(schoolStaffPhone);
            }
        }

        public object CheckedPhone([FromBody] JObject data)
        {
            try
            {
                var objSchoolStaff = data["idSchoolStaff"];
                var idSchoolStaff = 0;

                if (null != objSchoolStaff)
                {
                    idSchoolStaff = int.Parse(objSchoolStaff.ToString());
                }

                var objPhone = data["idPhone"];
                var idPhone = 0;

                if (null != objPhone)
                {
                    idPhone = int.Parse(objPhone.ToString());
                }

                _checkedPhone.WritePhone(idSchoolStaff, idPhone);

                var rezWork = new { success = true, message = "Ok!!!" };
                return rezWork;
            }
            catch (Exception e)
            {
                var error = new { success = false };
                _logger.Error(e, "Errow!!!");
                return error;
            }
        }
    }
}