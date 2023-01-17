
namespace RMS.Controllers
{
    [Authorize]
    public class StoreUnitController : Controller
    {
        private readonly IStoreUnit _Repo;
        public StoreUnitController(IStoreUnit repo) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("SUName");
            sortModel.AddColumn("CUser");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<StoreUnit> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            StoreUnit item = new StoreUnit();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(StoreUnit item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.SUName.Length < 2 || item.SUName == null)
                    errMessage = "Store Unit Must be atleast 2 Characters";
                if (_Repo.IsItemExists(item.SUName) == true)
                    errMessage = errMessage + " " + " Name " + item.SUName + " Exists Already";
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
                TempData["SuccessMessage"] = "" + item.SUName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            StoreUnit item = _Repo.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            StoreUnit item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StoreUnit item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.SUName.Length < 2 || item.SUName == null)
                    errMessage = "Department Must be atleast 2 Characters";
                if (_Repo.IsItemExists(item.SUName) == true)
                    errMessage = errMessage + " " + " Name " + item.SUName + " Exists Already";
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.SUName + ", Saved Successfully";
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
            StoreUnit item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(StoreUnit item)
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
            TempData["SuccessMessage"] = item.SUName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
    }
}
