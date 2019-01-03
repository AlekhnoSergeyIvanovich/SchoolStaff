using System;
using System.Linq;
using System.Threading.Tasks;
using Business.EditReferenceTable;
using Business.Repositories;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NLog;
using Presentation.AJAX;

namespace Presentation.Controllers
{
    //[AllowAnonymous]
    public class SchoolStaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Subject> _reposSubject;
        private readonly IRepository<SchoolStaff> _repos;
        private readonly ISchoolStaffDataAjax _dataAjax;
        private readonly IRepository<Profession> _repositoryProfession;
        private readonly IDeleteSchoolStaffSubjectFromSubject _deleteSchoolStaffSubjectFromSubject;
        private readonly IDeleteSchoolStaffProfessionFromProfession _deleteSchoolStaffProfessionFromProfession;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repos"></param>
        /// <param name="hubContext"></param>
        public SchoolStaffController(
            ISchoolStaffDataAjax dataAjax,
            IRepository<Subject> reposSubject,
            IRepository<Profession> repositoryProfession,
            IRepository<SchoolStaff> repos,
            IDeleteSchoolStaffSubjectFromSubject deleteSchoolStaffSubjectFromSubject,
            IDeleteSchoolStaffProfessionFromProfession deleteSchoolStaffProfessionFromProfession,
            ApplicationDbContext context
        )
        {
            _repos = repos;
            _dataAjax = dataAjax;
            _reposSubject = reposSubject;
            _repositoryProfession = repositoryProfession;
            _deleteSchoolStaffSubjectFromSubject = deleteSchoolStaffSubjectFromSubject;
            _deleteSchoolStaffProfessionFromProfession = deleteSchoolStaffProfessionFromProfession;
            _context = context;

            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Main page
        /// </summary>
        /// <returns></returns>
        // GET: SchoolStaff
        //[Authorize]
        public ViewResult Index()
        {
            int a = 0;
            var b = a.GetType();
            var listOfSchoolStaffsAll = _repos.GetAll();
            if (listOfSchoolStaffsAll == null)
            {
                throw new ApplicationException($"Unable to load listOfSchoolStaff.");
            }

            ViewBag.SchoolStaffPhones = _context.SchoolStaffPhones;// _repositorySchoolStaffPhone.GetAll();
            return View(listOfSchoolStaffsAll);
        }

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: SchoolStaff/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var listOfSchoolStaff = _repos.Get(id);
                return View(listOfSchoolStaff.Result);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to get details SchoolStaff.");
                throw new ApplicationException($"Unable to load SchoolStaff.");
            }
        }

        /// <summary>
        /// Add record
        /// </summary>
        /// <returns></returns>
        // GET: SchoolStaff/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add record
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        // POST: SchoolStaff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SchoolStaff staff)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    await _repos.Create(staff);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create SchoolStaff.");
                return View();
            }
        }

        /// <summary>
        /// Edit record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: SchoolStaff/Edit/5
        public ActionResult Edit(int id)
        {
            SchoolStaff schoolStaff = null;
            try
            {
                // TODO: Add delete logic here
                schoolStaff = _repos.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to load form edit SchoolStaff.");
                return View(schoolStaff);
            }

            return View(schoolStaff);
        }

        /// <summary>
        /// Edit record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        // POST: SchoolStaff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SchoolStaff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    //staff.Id = id;
                    await _repos.Update(staff);

                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to edit SchoolStaff.");
                return View();
            }
        }

        /// <summary>
        /// Delete record (menu)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: SchoolStaff/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.SchoolStaffSubjects.Count(a => a.SchoolStaffId == id) > 0)
            {
                ViewBag.DelMessageSSS = "SchoolStaff has a link to Subject!!!";
            }

            if (_context.SchoolStaffProfessions.Count(a => a.SchoolStaffId == id) > 0)
            {
                ViewBag.DelMessageSSP = "SchoolStaff has a link to Profession!!!";
            }

            SchoolStaff listOfSchoolStaff = null;
            try
            {
                listOfSchoolStaff = _repos.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to load form delete SchoolStaff.");
                return View();
            }

            return View(listOfSchoolStaff);
        }

        /// <summary>
        /// Action delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        // POST: SchoolStaff/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SchoolStaff staff)
        {
            // TODO: Add delete logic here
            SchoolStaff ofSchoolStaff = null;
            try
            {
                ofSchoolStaff = _repos.Get(id).Result;
                _repos.Delete(ofSchoolStaff);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable delete SchoolStaff.");
                return View(ofSchoolStaff);
            }
        }

        /// <summary>
        /// Ajax
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult SelectSchoolStaff(string name)
        {
            return new JsonResult(_dataAjax.RetAjax(name));
        }

        public async Task<ActionResult> LinkSchoolStaffSubject(int id)
        {
            try
            {
                ViewBag.Subject = _reposSubject.GetAll();
                ViewBag.SchoolStaffSubjects = _context.SchoolStaffSubjects.Where(c => c.SchoolStaffId == id);
                var schoolStaff = await _repos.Get(id);
                return View(schoolStaff);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to load form SchoolStaff-Subject.");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> LinkSchoolStaffSubject(SchoolStaff schoolStaff, int[] selectedSubject)
        {
            try
            {
                await _deleteSchoolStaffSubjectFromSubject.procDeleteSchoolStaffSubjectFromSubject(schoolStaff,
                    selectedSubject);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create linq SchoolStaff-Subject.");
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> LinkSchoolStaffAdministration(int id)
        {
            try
            {
                ViewBag.Profession = _repositoryProfession.GetAll();
                ViewBag.SchoolStaffProfession =
                    _context.SchoolStaffProfessions.Where(c => c.SchoolStaffId == id);
                var schoolStaff = await _repos.Get(id);
                return View(schoolStaff);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to load form SchoolStaff-Administration.");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> LinkSchoolStaffAdministration(SchoolStaff schoolStaff,
            int[] selectedProfessions)
        {
            try
            {
                await _deleteSchoolStaffProfessionFromProfession.procDeleteSchoolStaffProfessionFromProfession(
                    schoolStaff, selectedProfessions);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create Link SchoolStaff-Administration.");
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult LinkSchoolStaffPhone(int id)
        {
            return RedirectToAction(nameof(Index), "SchoolStaffPhones", new RouteValueDictionary(
                new { controller = "SchoolStaffPhones", action = nameof(Index), idSchoolStaffInd = id }));
        }
    }
}