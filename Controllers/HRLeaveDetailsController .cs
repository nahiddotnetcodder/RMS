using Microsoft.AspNetCore.Hosting;
using RMS.Interfaces;
using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HRLeaveDetailsController : Controller
    {
        private readonly IHRLeaveDetails _leaveDetails;
        private readonly IHREmpDetails _empDetails;
        private readonly IHRLeavePolicy _leavePolicy;

        public HRLeaveDetailsController(IHRLeaveDetails leaveDetails, IHREmpDetails empDetails, IHRLeavePolicy leavePolicy) // here the repository will be passed by the dependency injection.
        {
            _leaveDetails = leaveDetails;
            _empDetails = empDetails;
            _leavePolicy = leavePolicy;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HREmpDetails");
            sortModel.AddColumn("HRLeavePolicy");
            sortModel.AddColumn("HRLDAppSl");
            sortModel.AddColumn("HRLDAppDate");
            sortModel.AddColumn("HRLDLeaveSDate");
            sortModel.AddColumn("HRLDLeaveEDate");
            sortModel.AddColumn("HRLDReason");
            sortModel.AddColumn("HREDIdSu");
            sortModel.AddColumn("HRLDTDay");
            sortModel.AddColumn("CUser");
            sortModel.AddColumn("CreateDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRLeaveDetail> resmenu = _leaveDetails.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }
        private void PopulateViewbags()
        {
           ViewBag.EmpDetails = GetHREmpDetails();
           ViewBag.LeavePolicy = GetLeavePolicy();
        }

        [HttpGet]
        public IActionResult Create()
        {
            HRLeaveDetail leaveDetails = new HRLeaveDetail();

            PopulateViewbags();
            return View(leaveDetails);
        }

        [HttpPost]
        public IActionResult Create(HRLeaveDetail hrleavedetails)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrleavedetails.HRLDAppSl.Length < 4 || hrleavedetails.HRLDAppSl == null)
                    errMessage = "Leave Details App Must be atleast 4 Characters";
                if (_leaveDetails.IsNameExists(hrleavedetails.HREDId, hrleavedetails.HRLPId) == true)
                    errMessage = errMessage + hrleavedetails.HREDEName + "Already Exists";

                if (errMessage == "")
                {

                    hrleavedetails = _leaveDetails.Create(hrleavedetails);
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
                PopulateViewbags();       
                return View(hrleavedetails);
            }
            else
            {
                TempData["SuccessMessage"] = "Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRLeaveDetail item = _leaveDetails.GetItem(id);
           PopulateViewbags();
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HRLeaveDetail item = _leaveDetails.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRLeaveDetail hrleavedetails)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrleavedetails.HRLDAppSl.Length < 4 || hrleavedetails.HRLDAppSl == null)
                    errMessage = "Leave Details App Must be atleast 4 Characters";
                if (_leaveDetails.IsNameExists(hrleavedetails.HREDId, hrleavedetails.HRLDId) == true)
                    errMessage = errMessage + "Already Exists";
                if (errMessage == "")
                {
                    hrleavedetails = _leaveDetails.Edit(hrleavedetails);
                    TempData["SuccessMessage"] = "Updated Successfully";
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
                return View(hrleavedetails);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HRLeaveDetail item = _leaveDetails.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HRLeaveDetail hrleavedetails)
        {
            try
            {
                hrleavedetails = _leaveDetails.Delete(hrleavedetails);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrleavedetails);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        private List<SelectListItem> GetHREmpDetails()
        {
            var lstEmp = new List<SelectListItem>();
            PaginatedList<HREmpDetails> items = _empDetails.GetItems("HREDEName", SortOrder.Ascending, "", 1, 1000);
            lstEmp = items.Select(ut => new SelectListItem()
            {
                Value = ut.HREDId.ToString(),
                Text = ut.HREDEName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Employee Name----"
            };
            lstEmp.Insert(0, defItem);
            return lstEmp;
        }
        private List<SelectListItem> GetLeavePolicy()
        {
            var lstPolicy = new List<SelectListItem>();
            PaginatedList<HRLeavePolicy> items = _leavePolicy.GetItems("HRLPName", SortOrder.Ascending, "", 1, 1000);
            lstPolicy = items.Select(ut => new SelectListItem()
            {
                Value = ut.HRLPId.ToString(),
                Text = ut.HRLPName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Leave Policy Name----"
            };
            lstPolicy.Insert(0, defItem);
            return lstPolicy;
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsNameExists(int Did)
        {
            bool isExists = _leaveDetails.IsNameExists(Did);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsNameExists(int Did, int id)
        {
            bool isExists = _leaveDetails.IsNameExists(Did, id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }

    }
}
