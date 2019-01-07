using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Repositories;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.AJAX;
using Business.EditReferenceTable;
using NLog;
using NLog.Web;

namespace Presentation.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SchoolStaff> _dbSet;
        private readonly IRepository<Profession> _repositoryProfession;
        private readonly IProfessionDataAjax _dataAjax;
        private readonly IDeleteSchoolStaffProfessionFromSchoolStaff _deleteSchoolStaffProfessionFromSchoolStaff;
        private readonly ILogger _logger;


        public AdministrationController(
            IRepository<Profession> repositoryProfession,
            IProfessionDataAjax dataAjax,
            ApplicationDbContext context,
            IDeleteSchoolStaffProfessionFromSchoolStaff deleteSchoolStaffProfessionFromSchoolStaff
        )
        {
            _repositoryProfession = repositoryProfession;
            _dataAjax = dataAjax;
            _context = context;
            _dbSet = _context.SchoolStaffs;
            _deleteSchoolStaffProfessionFromSchoolStaff = deleteSchoolStaffProfessionFromSchoolStaff;

            _logger = LogManager.GetCurrentClassLogger();
        }

        // GET: Administration
        public ActionResult Index()
        {
            var listOfProfession = _repositoryProfession.GetAll();
            if (listOfProfession == null)
            {
                throw new ApplicationException($"Unable to load listOfProfession Administration.");
            }

            return View(listOfProfession);
        }

        // GET: Administration/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var profession = _repositoryProfession.Get(id);
                return View(profession.Result);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to load profession.");
                throw new ApplicationException($"Unable to load Administration.");
            }
        }

        // GET: Administration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profession prof)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _repositoryProfession.Create(prof);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to creat Administration.");
                return View();
            }
        }

        // GET: Administration/Edit/5
        public ActionResult Edit(int id)
        {
            Profession profession = null;
            try
            {
                // TODO: Add delete logic here
                profession = _repositoryProfession.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create form edit Administration.");
                return View(profession);
            }
            return View(profession);
        }

        // POST: Administration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Profession prof)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    prof.Id = id;
                    await _repositoryProfession.Update(prof);

                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to edit Administration.");
                return View();
            }
        }

        // GET: Administration/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.SchoolStaffProfessions.Count(a => a.ProfessionId == id) > 0)
            {
                ViewBag.DelMessage = "Profession has a link to SchoolStaff!!!";
            }

            Profession prof = null;
            try
            {
                prof = _repositoryProfession.Get(id).Result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create form delete Administration.");
                return View();
            }
            return View(prof);
        }

        // POST: Administration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Profession prof)
        {
            // TODO: Add delete logic here
            Profession ofProf = null;
            try
            {
                ofProf = _repositoryProfession.Get(id).Result;
                _repositoryProfession.Delete(ofProf);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to delete Administration.");
                return View(ofProf);
            }
        }

        public JsonResult SelectProfession(string name)
        {
            return new JsonResult(_dataAjax.RetAjax(name));
        }

        public async Task<ActionResult> LinkSchoolStaffAdministration(int id)
        {
            try
            {
                ViewBag.SchoolStaff = _dbSet;
                ViewBag.SchoolStaffProfession = _context.SchoolStaffProfessions.Where(c => c.ProfessionId == id);
                var profession = await _repositoryProfession.Get(id);
                return View(profession);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create form linq SchoolStaff-Administration.");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> LinkSchoolStaffAdministration(Profession professions, int[] selectedSchoolStaffs)
        {
            try
            {
                await _deleteSchoolStaffProfessionFromSchoolStaff.procDeleteSchoolStaffProfessionFromSchoolStaff(professions, selectedSchoolStaffs);
                
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to create linq SchoolStaff-Administration.");
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyName(string nameProfession, int id)
        {
            if (_repositoryProfession.GetAll().Count(c => ((c.NameProfession == nameProfession) && (c.Id != id))) > 0)
            {
                return Json(false);
            }

            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifySchoolStaffProfession(int? ProfessionId, int? SchoolStaffId)
        {
            if (_context.SchoolStaffProfessions.Count(c => c.SchoolStaffId == SchoolStaffId) > _repositoryProfession.Get(ProfessionId.Value).Result.CountPeopleProfession)
            {
                return Json(false);
            }
            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyCountPeopleProfession(uint? countPeopleProfession, int id)
        {
            if (_context.SchoolStaffProfessions.Count(c => c.ProfessionId == id) > countPeopleProfession.Value)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}