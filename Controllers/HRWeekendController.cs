using RMS.Interfaces;
using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HRWeekendController : Controller
    { 
        private readonly IHRWeekend _weekend;

        public HRWeekendController(IHRWeekend weekend) // here the repository will be passed by the dependency injection.
        {
            _weekend = weekend;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRWId");
            sortModel.AddColumn("Weekend");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRWeekend> resmenu = _weekend.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }
        public IActionResult Create()
        {
            HRWeekend weekend = new HRWeekend();
            return View(weekend);
        }
        [HttpPost]
        public IActionResult Create(HRWeekend hrweekend)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    hrweekend = _weekend.Create(hrweekend);
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
                return View(hrweekend);
            }
            else
            {
                TempData["SuccessMessage"] = "Employee Profile " + hrweekend.HRWId + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRWeekend item = _weekend.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRWeekend item = _weekend.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRWeekend hrweekend)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                //if (_weekend.IsDNameExists(hrweekend.HRWId.ToString(), hrweekend.HRWId) == true)
                //    errMessage = errMessage + " " + " Employee Id. " + hrweekend.HRWId + " Exists Already";
                if (errMessage == "")
                {
                    hrweekend = _weekend.Edit(hrweekend);
                    TempData["SuccessMessage"] = hrweekend.HRWId + ",  Employee Profile Updated Successfully";
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
                return View(hrweekend);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HRWeekend item = _weekend.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HRWeekend hrweekend)
        {
            try
            {
                hrweekend = _weekend.Delete(hrweekend);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrweekend);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _weekend.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name, int id)
        {
                bool isExists = _weekend.IsDNameExists(name,  id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
