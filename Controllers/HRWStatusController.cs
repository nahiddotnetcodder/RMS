
namespace RMS.Controllers
{
    [Authorize]
    public class HRWStatusController : Controller
    {
        private readonly IHRWStatus _Repo;
        public HRWStatusController(IHRWStatus repo) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRWSName");
            sortModel.AddColumn("HRWSDes");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRWStatus> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            HRWStatus item = new HRWStatus();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(HRWStatus item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRWSName.Length < 2 || item.HRWSName == null)
                    errMessage = "Department Must be atleast 2 Characters";
                if (_Repo.IsStatusExists(item.HRWSName) == true)
                    errMessage = errMessage + " " + " Name " + item.HRWSName + " Exists Already";
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
                TempData["SuccessMessage"] = "" + item.HRWSName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRWStatus item = _Repo.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRWStatus item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRWStatus item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRWSName.Length < 2 || item.HRWSName == null)
                    errMessage = "Department Must be atleast 2 Characters";
                if (_Repo.IsStatusExists(item.HRWSName, item.HRWSId) == true)
                    errMessage = errMessage + " " + " Name " + item.HRWSName + " Exists Already";             
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.HRWSName + ", Saved Successfully";
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
            HRWStatus item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(HRWStatus item)
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
            TempData["SuccessMessage"] = item.HRWSName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
    }
}
