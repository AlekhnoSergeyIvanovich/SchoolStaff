using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Class;
using Business.EditReferenceTable;
using Business.Repositories;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Presentation.SignalR;

namespace Presentation.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SchoolStaff> _dbSet;
        private readonly IRepository<Subject> _repSubject;
        private readonly IDeleteSchoolStaffSubjectFromSchoolStaff _deleteSchoolStaffSubjectFromSchoolStaff;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repos"></param>
        /// <param name="hubContext"></param>
        public SubjectController(
            ApplicationDbContext context,
            IRepository<Subject> repSubject,
            IDeleteSchoolStaffSubjectFromSchoolStaff deleteSchoolStaffSubjectFromSchoolStaff
            )
        {
            _context = context;
            _dbSet = _context.SchoolStaffs;
            _repSubject = repSubject;
            _deleteSchoolStaffSubjectFromSchoolStaff = deleteSchoolStaffSubjectFromSchoolStaff;
            _logger = LogManager.GetCurrentClassLogger();
        }

        // GET: Subject
        public ActionResult Index()
        {
            var SubjectStaffsAll = _repSubject.GetAll();
            if (SubjectStaffsAll == null)
            {
                throw new ApplicationException($"Unable to load subject.");
            }

            return View(SubjectStaffsAll);
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var subject = _repSubject.Get(id);
                return View(subject.Result);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show detail Link Subject.");
                throw new ApplicationException($"Unable to load subject.");
            }
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _repSubject.Create(subject);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create detail Subject.");
                return View();
            }
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            Subject subject = null;
            try
            {
                // TODO: Add delete logic here
                subject = _repSubject.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show form editSubject.");
                return View(subject);
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Subject subject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    subject.Id = id;
                    await _repSubject.Update(subject);

                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to edit Subject.");
                return View();
            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.SchoolStaffSubjects.Count(a => a.SubjectId == id) > 0)
            {
                ViewBag.DelMessage = "Subject has a link to SchoolStaff!!!";
            }

            Subject subject = null;
            try
            {
                subject = _repSubject.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show form delete Subject.");
                return View();
            }
            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // TODO: Add delete logic here
            Subject subject = null;
            try
            {
                subject = _repSubject.Get(id).Result;

                _repSubject.Delete(subject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to delete Subject.");
                return View(subject);
            }
        }

        public async Task<ActionResult> LinkSchoolStaffSubject(int id)
        {
            try
            {
                ViewBag.SchoolStaffs = _dbSet;
                ViewBag.SchoolStaffSubjects = _context.SchoolStaffSubjects.Where(c => c.SubjectId == id);
                var subject = await _repSubject.Get(id);
                return View(subject);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to show form Link SchoolStaff-Subject.");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> LinkSchoolStaffSubject(Subject subject, int[] selectedSchoolStaffs)
        {
            try
            {
                await _deleteSchoolStaffSubjectFromSchoolStaff.procDeleteSchoolStaffSubjectFromSchoolStaff(subject, selectedSchoolStaffs);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create Link SchoolStaff-Subject.");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CheckName(string name)
        {
            if (_repSubject.GetAll().Count(c => c.Name == name) >0)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}