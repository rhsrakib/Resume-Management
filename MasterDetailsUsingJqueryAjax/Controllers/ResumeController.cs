using MasterDetailsUsingJqueryAjax.Models;
using MasterDetailsUsingJqueryAjax.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailsUsingJqueryAjax.Controllers
{
    public class ResumeController : Controller
    {
        private readonly AppDbContext  _context;
        private readonly IWebHostEnvironment _environment;

        public ResumeController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var applicants = _context.Applicants.Include(e => e.Experiances).ToList();
            return View(applicants);
        }
        public JsonResult GetDesignationlist()
        {
            List<SelectListItem> digs = (from dig in this._context.Designations
                                         select new SelectListItem
                                         {
                                             Value = dig.DesignationName.ToString(),
                                             Text = dig.DesignationName
                                         }).ToList();
            return Json(digs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ApplicantViewModel applicant=new ApplicantViewModel();
            applicant.Designations = _context.Designations.ToList();
            applicant.Experiances.Add(new Experiance() { ExperianceId = 1 });
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Create(ApplicantViewModel viewObj)
        {
            string uniqueFileName = GetUploadFileName(viewObj);
            viewObj.ImageUrl = uniqueFileName;
            Applicant obj=new Applicant();
            obj.ApplicantName= viewObj.ApplicantName;
            obj.DesignationName= viewObj.DesignationName;
            obj.MobileNo = viewObj.MobileNo;
            obj.IsActive = viewObj.IsActive;
            obj.ImageUrl= viewObj.ImageUrl;
            obj.Dob= viewObj.Dob;
            _context.Applicants.Add(obj);
            _context.SaveChanges();
            var user = _context.Applicants.SingleOrDefault(a => a.MobileNo == viewObj.MobileNo);
            if(user!=null)
            {
                if(viewObj.Experiances.Count>0)
                {
                    foreach (var item in viewObj.Experiances)
                    {
                        Experiance exp = new Experiance();
                        exp.ApplicantId = user.Id;
                        exp.CompanyName = item.CompanyName;
                        exp.YearsWorked= item.YearsWorked;

                        _context.Experiances.Add(exp);
                    }
                    _context.SaveChanges();
                }

            }
           
            return RedirectToAction("Index");
        }

        private string GetUploadFileName(ApplicantViewModel applicant)
        {
            string uniqueFileName = null;
            if (applicant!=null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + " " + applicant.ProfilePhoto.FileName;
                string filePath=Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream=new FileStream(filePath,FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public ActionResult Delete(int? id)
        {
            var app = _context.Applicants.Find(id);
            var existingExperience = _context.Experiances.Where(a => a.ApplicantId == id).ToList();
            foreach (var experience in existingExperience)
            {
                _context.Experiances.Remove(experience);
            }
            _context.Entry(app).State =EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            Applicant app = _context.Applicants.First(a=>a.Id==id);
            var exps=_context.Experiances.Where(a=>a.ApplicantId==id).ToList(); 
            if(app!=null)
            {
                ApplicantViewModel appVm = new ApplicantViewModel()
                {
                    Id = app.Id,
                    ApplicantName = app.ApplicantName,
                    MobileNo = app.MobileNo,
                    Dob = app.Dob,
                    ImageUrl = app.ImageUrl,
                    DesignationName = app.DesignationName,
                    IsActive = app.IsActive,
                    Designations=_context.Designations.ToList()
                };
                if(exps.Count() > 0)
                {
                    foreach (var experience in exps)
                    {
                        Experiance obj= new Experiance();
                        obj.ApplicantId=app.Id;
                        obj.YearsWorked=experience.YearsWorked;
                        obj.CompanyName=experience.CompanyName;
                        appVm.Experiances.Add(obj);
                    }
                }
                return View(appVm);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ApplicantViewModel applicant)
        {
            string uniqueFileName = GetUploadFileName(applicant);
            applicant.ImageUrl = uniqueFileName;
            Applicant obj= new Applicant();
            obj.ApplicantName= applicant.ApplicantName;
            obj.DesignationName= applicant.DesignationName;
            obj.IsActive= applicant.IsActive;
            obj.MobileNo= applicant.MobileNo;
            obj.Dob= applicant.Dob;
            obj.ImageUrl= applicant.ImageUrl;
            obj.Id= applicant.Id;
            if(applicant.Experiances.Count>0)
            {
                foreach (var item in applicant.Experiances)
                {
                    Experiance expobj = new Experiance();
                    expobj.ApplicantId=obj.Id;
                    expobj.YearsWorked=item.YearsWorked;
                    expobj.CompanyName = item.CompanyName;
                    _context.Entry(expobj).State= EntityState.Modified;
                }
            }
            _context.Entry(obj).State= EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
