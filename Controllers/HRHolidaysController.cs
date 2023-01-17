using RMS.Models;
using RMS.Repositories;
using System.Runtime.ConstrainedExecution;

namespace RMS.Controllers
{
    [Authorize]
    public class HRHolidaysController : Controller
    {
        private readonly IHRHolidays _holidays;

        public HRHolidaysController(IHRHolidays holidays) // here the repository will be passed by the dependency injection.
        {
            _holidays = holidays;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRHId");
            sortModel.AddColumn("HRHName");
            sortModel.AddColumn("HRHStartDate");
            sortModel.AddColumn("HRHEndDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRHolidays> resmenu = _holidays.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(resmenu.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(resmenu);
        }

        public IActionResult Create()
        {
            HRHolidays empdetails = new HRHolidays();
            return View(empdetails);
        }
        [HttpPost]
        public IActionResult Create(HRHolidays hrholidays)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (hrholidays.HRHName.Length < 4 || hrholidays.HRHName == null)
                    errMessage = "Employee Name Must be atleast 4 Characters";
                if (_holidays.IsDNameExists(hrholidays.HRHName) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrholidays.HRHName + " Exists Already";
                if (errMessage == "")
                {
                    hrholidays = _holidays.Create(hrholidays);
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
                return View(hrholidays);
            }
            else
            {
                TempData["SuccessMessage"] = "Employee Profile " + hrholidays.HRHName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRHolidays item = _holidays.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRHolidays item = _holidays.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRHolidays hrholidays)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {

                if (hrholidays.HRHName.Length < 4 || hrholidays.HRHName == null)
                    errMessage = "Employee Name Must be atleast 4 Characters";
                if (_holidays.IsDNameExists(hrholidays.HRHName, hrholidays.HRHId) == true)
                    errMessage = errMessage + " " + " Employee Id. " + hrholidays.HRHName + " Exists Already";
                if (errMessage == "")
                {
                    hrholidays = _holidays.Edit(hrholidays);
                    TempData["SuccessMessage"] = hrholidays.HRHName + ",  Employee Profile Updated Successfully";
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
                return View(hrholidays );
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HRHolidays item = _holidays.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(HRHolidays hrholidays)
        {
            try
            {
                hrholidays = _holidays.Delete(hrholidays);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(hrholidays);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = hrholidays.HRHName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _holidays.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name, int id)
        {
            bool isExists = _holidays.IsDNameExists(name, id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
