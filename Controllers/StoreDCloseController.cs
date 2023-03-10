
namespace RMS.Controllers
{
    [Authorize]
    public class StoreDCloseController : Controller
    {
        private readonly IStoreDClose _Repo;
        private readonly RmsDbContext _db;

        public StoreDCloseController(IStoreDClose repo, RmsDbContext db) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
            _db = db;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("RDCDate");
            sortModel.AddColumn("CUser");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<StoreDClose> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            StoreDClose item = new StoreDClose();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(StoreDClose item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
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
                TempData["SuccessMessage"] = "Date Setup Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Edit(int id)
        {
            StoreDClose item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StoreDClose item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = "Day Closed Successfully";
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
        [HttpGet]
        public JsonResult GetDateValue()
        {
            var data = _db.StoreDClose.Select(x => x.SDCDate);
            return Json(data);
        }
    }
}
