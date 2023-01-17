
using RMS.Models;

namespace RMS.Controllers
{
    [Authorize]
    public class StoreSCategoryController : Controller
    {
        private readonly IStoreSCategory _Repo;
        private readonly IStoreCategory _storeCategory;

        public StoreSCategoryController(IStoreSCategory repo, IStoreCategory storeCategory) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
            _storeCategory = storeCategory;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("SSId");
            sortModel.AddColumn("SCName");
            sortModel.AddColumn("SSCName");
            sortModel.AddColumn("CUser");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<StoreSCategory> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        public IActionResult Create()
        {
            StoreSCategory item = new StoreSCategory();
            ViewBag.StoreCategory = GetStoreCategory();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(StoreSCategory storeSCategory)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (storeSCategory.SSCName.Length < 4 || storeSCategory.SSCName == null)
                    errMessage = "Sub-Category Must be atleast 4 Characters";
                if (_Repo.IsItemExists(storeSCategory.SSCName, storeSCategory.SSCId) == true)
                    errMessage = errMessage + " " + "Sub-Category Name " + " Exists Already";
                if (errMessage == "")
                {
                    storeSCategory = _Repo.Create(storeSCategory);
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
                ViewBag.StoreCategory = GetStoreCategory();
                return View(storeSCategory);
            }
            else
            {
                TempData["SuccessMessage"] = "" + storeSCategory.SSCName + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            StoreSCategory item = _Repo.GetItem(id);
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            StoreSCategory item = _Repo.GetItem(id);
            ViewBag.StoreCategory = GetStoreCategory();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StoreSCategory item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (_Repo.IsItemExists(item.SSCName, item.SSCId) == true)
                    errMessage = errMessage + " " + "Sub-Category Name " + " Exists Already";
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.SSCName + ", Saved Successfully";
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
            StoreSCategory item = _Repo.GetItem(id);
            ViewBag.StoreCategory = GetStoreCategory();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(StoreSCategory item)
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
            TempData["SuccessMessage"] = item.SSCName + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        private List<SelectListItem> GetStoreCategory()
        {
            var scategory = new List<SelectListItem>();
            PaginatedList<StoreCategory> items = _storeCategory.GetItems("SCName", SortOrder.Ascending, "", 1, 1000);
            scategory = items.Select(ut => new SelectListItem()
            {
                Value = ut.SCId.ToString(),
                Text = ut.SCName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Category Name----"
            };
            scategory.Insert(0, defItem);
            return scategory;
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult IsItemExists(string name)
        {
            bool isExists = _Repo.IsItemExists(name);
            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsItemExists(string name, int id)
        {
            bool isExists = _Repo.IsItemExists(name, id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}
