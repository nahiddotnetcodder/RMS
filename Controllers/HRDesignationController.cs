
namespace RMS.Controllers
{
    [Authorize]
    public class HRDesignationController : Controller
    {
        private readonly IHRDesignation _Repo;
        public HRDesignationController(IHRDesignation repo) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("HRDeName"); 
            sortModel.AddColumn("HRDeDes");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<HRDesignation> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            HRDesignation item = new HRDesignation();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(HRDesignation item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRDeName.Length < 2 || item.HRDeName == null)
                    errMessage = "Department Must be atleast 2 Characters";
                if (_Repo.IsDNameExists(item.HRDeName) == true)
                    errMessage = errMessage + " " + " Name " + item.HRDeName + " Exists Already";
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
                TempData["SuccessMessage"] = "" + item.HRDeName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            HRDesignation item = _Repo.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            HRDesignation item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(HRDesignation item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (item.HRDeName.Length < 2 || item.HRDeName == null)
                    errMessage = "Department Must be atleast 2 Characters";
                if (_Repo.IsDNameExists(item.HRDeName, item.HRDeId) == true)
                    errMessage = errMessage + " " + " Name " + item.HRDeName + " Exists Already";
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.HRDeName + ", Saved Successfully";
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
            HRDesignation item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(HRDesignation item)
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
            TempData["SuccessMessage"] = item.HRDeName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
    }
}
