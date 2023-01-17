using Microsoft.AspNetCore.Components.Routing;
using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HREmpRoasterController : Controller
    {
        private readonly IHREmpDetails _hrempdetailsRepo;
        private readonly IHREmpRoaster _empRoaster;

        public HREmpRoasterController(IHREmpDetails hrempdetailsRepo, IHREmpRoaster empRoaster ) // here the repository will be passed by the dependency injection.
        {
            _hrempdetailsRepo = hrempdetailsRepo;
            _empRoaster = empRoaster;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRERId");
            sortModel.AddColumn("HREmpDetails");
            sortModel.AddColumn("HRERDate");
            sortModel.AddColumn("IsPresent");
            sortModel.AddColumn("CUser");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HREmpRoaster> resmenu = _empRoaster.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }

        public IActionResult Create()
        {
            HREmpRoaster empdetails = new HREmpRoaster();
            ViewBag.HREmpDetails = GetEmpDetails();
            return View(empdetails);
        }
        [HttpPost]
        public IActionResult Create(HREmpRoaster roaster)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (_empRoaster.IsDNameExists(roaster.HREDId, roaster.HRERDate ) == true)
                    errMessage = errMessage + " " + "Same Name & date " + "Already Exists";
                if (errMessage == "")
                {
                    roaster = _empRoaster.Create(roaster);
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
                ViewBag.HREmpDetails = GetEmpDetails();
                return View(roaster);
            }
            else
            {
                TempData["SuccessMessage"] = "Employee Profile " + roaster.HRERId + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HREmpRoaster item = _empRoaster.GetItem(id);
            ViewBag.HREmpDetails = GetEmpDetails();
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HREmpRoaster item = _empRoaster.GetItem(id);
            ViewBag.HREmpDetails = GetEmpDetails();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HREmpRoaster roaster)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (_empRoaster.IsDNameExists(roaster.HREDId, roaster.HRERDate) == true)
                    errMessage = errMessage + " " + "Same Name & date " + "Already Exists";

                if (errMessage == "")
                {
                    roaster = _empRoaster.Edit(roaster);
                    TempData["SuccessMessage"] = roaster.HREDId + ",  Employee Profile Updated Successfully";
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
                return View(roaster);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HREmpRoaster item = _empRoaster.GetItem(id);
            ViewBag.HREmpDetails = GetEmpDetails();
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HREmpRoaster roaster)
        {
            try
            {
                roaster = _empRoaster.Delete(roaster);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(roaster);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
       
       
        private List<SelectListItem> GetEmpDetails()
        {
            var lstStatus = new List<SelectListItem>();
            PaginatedList<HREmpDetails> items = _hrempdetailsRepo.GetItems("HREDEName", SortOrder.Ascending, "", 1, 1000);
            lstStatus = items.Select(ut => new SelectListItem()
            {
                Value = ut.HREDId.ToString(),
                Text = ut.HREDEName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Work Status----"
            };
            lstStatus.Insert(0, defItem);
            return lstStatus;
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _empRoaster.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(int id, DateTime date)
        {
            bool isExists = _empRoaster.IsDNameExists(id, date);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
