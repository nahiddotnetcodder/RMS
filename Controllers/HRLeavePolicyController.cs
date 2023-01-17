using RMS.Interfaces;
using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HRLeavePolicyController : Controller
    {
        private readonly IHRLeavePolicy _leavePolicy;

        public HRLeavePolicyController(IHRLeavePolicy leavePolicy) // here the repository will be passed by the dependency injection.
        {
            _leavePolicy = leavePolicy;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRLPId");
            sortModel.AddColumn("HRLPName"); 
            sortModel.AddColumn("HRLPTDay");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRLeavePolicy> resmenu = _leavePolicy.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }
        public IActionResult Create()
        {
            HRLeavePolicy empdetails = new HRLeavePolicy();
            return View(empdetails);
        }
        [HttpPost]
        public IActionResult Create(HRLeavePolicy hrleavepolicy)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrleavepolicy.HRLPName.Length < 4 || hrleavepolicy.HRLPName == null)
                    errMessage = "Leave Policy Name Must be atleast 4 Characters";
                if (_leavePolicy.IsDNameExists(hrleavepolicy.HRLPName) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrleavepolicy.HRLPName + " Exists Already";
                if (errMessage == "")
                {
                    hrleavepolicy = _leavePolicy.Create(hrleavepolicy);
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
                return View(hrleavepolicy);
            }
            else
            {
                TempData["SuccessMessage"] = "Employee Profile " + hrleavepolicy.HRLPName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRLeavePolicy item = _leavePolicy.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRLeavePolicy item = _leavePolicy.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRLeavePolicy hrleavepolicy)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrleavepolicy.HRLPName.Length < 4 || hrleavepolicy.HRLPName == null)
                    errMessage = "Employee Name Must be atleast 4 Characters";
                if (_leavePolicy.IsDNameExists(hrleavepolicy.HRLPName, hrleavepolicy.HRLPId) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrleavepolicy.HRLPName + " Exists Already";
                if (errMessage == "")
                {
                    hrleavepolicy = _leavePolicy.Edit(hrleavepolicy);
                    TempData["SuccessMessage"] = hrleavepolicy.HRLPName + ",  Employee Profile Updated Successfully";
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
                return View(hrleavepolicy);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HRLeavePolicy item = _leavePolicy.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HRLeavePolicy hrleavepolicy)
        {
            try
            {
                hrleavepolicy = _leavePolicy.Delete(hrleavepolicy);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrleavepolicy);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = hrleavepolicy.HRLPName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _leavePolicy.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name, int id)
        {
                bool isExists = _leavePolicy.IsDNameExists(name,  id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
