
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Controllers
{
    [Authorize]
    public class HRDepartmentController : Controller
    {
        private readonly IHRDepartment _Repo;
        public HRDepartmentController(IHRDepartment repo) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRDName");
            sortModel.AddColumn("HRDDes");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRDepartment> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            HRDepartment item = new HRDepartment();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(HRDepartment item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRDName.Length < 4 || item.HRDName == null)
                    errMessage = "Department Must be atleast 4 Characters";
                if (_Repo.IsDNameExists(item.HRDName) == true)
                    errMessage = errMessage + " " + " Name " + item.HRDName + " Exists Already";
                if (errMessage == "")
                {
                    item = _Repo.Create(item);
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
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "" + item.HRDName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRDepartment item = _Repo.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRDepartment item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRDepartment item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRDName.Length < 4 || item.HRDName == null)
                    errMessage = "Department Must be atleast 4 Characters";
                if (_Repo.IsDNameExists(item.HRDName, item.HRDId) == true)
                    errMessage = errMessage + " " + " Name " + item.HRDName + " Exists Already";
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.HRDName + ", Saved Successfully";
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
                return View(item);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        public IActionResult Delete(int id)
        {
            HRDepartment item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(HRDepartment item)
        {
            try
            {
                item = _Repo.Delete(item);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = item.HRDName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name)
        {
            bool isExists = _Repo.IsDNameExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsDNameExists(string name, int id)
        {
            bool isExists = _Repo.IsDNameExists(name, id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
