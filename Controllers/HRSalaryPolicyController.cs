using RMS.Interfaces;
using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HRSalaryPolicyController : Controller
    {
        private readonly IHRSalaryPolicy _salaryPolicy;

        public HRSalaryPolicyController(IHRSalaryPolicy salaryPolicy) // here the repository will be passed by the dependency injection.
        {
            _salaryPolicy = salaryPolicy;
        } 
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRSPId");
            sortModel.AddColumn("HRSPName");
            sortModel.AddColumn("ADDUC");
            sortModel.AddColumn("PerNPer");
            sortModel.AddColumn("HRSPPercent");            
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRSalaryPolicy> resmenu = _salaryPolicy.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }
        public IActionResult Create()
        {
            HRSalaryPolicy policyDetails = new HRSalaryPolicy();
            return View(policyDetails);
        }
        [HttpPost]
        public IActionResult Create(HRSalaryPolicy hrsalarypolicy)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrsalarypolicy.HRSPName.Length < 4 || hrsalarypolicy.HRSPName == null)
                    errMessage = "Salary Policy Name Must be atleast 4 Characters";
                if (_salaryPolicy.IsDNameExists(hrsalarypolicy.HRSPName) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrsalarypolicy.HRSPName + " Exists Already";
                if (errMessage == "")
                {
                    hrsalarypolicy = _salaryPolicy.Create(hrsalarypolicy);
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);     
                return View(hrsalarypolicy);
            }
            else
            {
                TempData["SuccessMessage"] = "Employee Profile " + hrsalarypolicy.HRSPName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRSalaryPolicy item = _salaryPolicy.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRSalaryPolicy item = _salaryPolicy.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRSalaryPolicy hrsalarypolicy)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrsalarypolicy.HRSPName.Length < 4 || hrsalarypolicy.HRSPName == null)
                    errMessage = "Employee Name Must be atleast 4 Characters";
                if (_salaryPolicy.IsDNameExists(hrsalarypolicy.HRSPName, hrsalarypolicy.HRSPId) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrsalarypolicy.HRSPName + " Exists Already";
                if (errMessage == "")
                {
                    hrsalarypolicy = _salaryPolicy.Edit(hrsalarypolicy);
                    TempData["SuccessMessage"] = hrsalarypolicy.HRSPName + ",  Employee Profile Updated Successfully";
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrsalarypolicy);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HRSalaryPolicy item = _salaryPolicy.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HRSalaryPolicy hrsalarypolicy)
        {
            try
            {
                hrsalarypolicy = _salaryPolicy.Delete(hrsalarypolicy);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrsalarypolicy);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = hrsalarypolicy.HRSPName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _salaryPolicy.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name, int id)
        {
                bool isExists = _salaryPolicy.IsDNameExists(name,  id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
