
using Microsoft.EntityFrameworkCore;
using RMS.Repositories;

namespace RMS.Controllers
{
    [Authorize]
    public class StoreGIssueController : Controller
    {
        private readonly IStoreGIssue _Repo;
        private readonly IHRDepartment _hrdepartmentRepo;
        private readonly IStoreIGen _storeigen;
        private readonly IStoreCategory _category;
        private readonly IStoreSCategory _subCat;
        private readonly IStoreUnit _unit;
        private readonly RmsDbContext _context;

        public StoreGIssueController(IStoreGIssue repo, IHRDepartment hrdepartmentRepo, IStoreIGen storeigen, IStoreCategory category, IStoreSCategory subCat, IStoreUnit unit, RmsDbContext context) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
            _hrdepartmentRepo = hrdepartmentRepo;
            _storeigen = storeigen;
            _category = category;
            _subCat = subCat;
            _unit = unit;
            _context = context;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("GIdate");
            sortModel.AddColumn("StoreIG");
            sortModel.AddColumn("StoreCategory");
            sortModel.AddColumn("StoreSCategory");
            sortModel.AddColumn("StoreUnit");
            sortModel.AddColumn("GIUPrice");
            sortModel.AddColumn("GIQty");
            sortModel.AddColumn("HRDepart");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<StoreGIssue> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }

        private void PopulateViewbags()
        {
            ViewBag.HRDepartment = GetHRDepartments();
            ViewBag.StoreIGen = GetStoreIGen();
            ViewBag.IGenCode = GetStoreIGenCode();
            ViewBag.StoreCategory = GetCategory();
            ViewBag.StoreSCategory = GetSubCategory();
            ViewBag.StoreUnit = GetUnit();
        }

        public IActionResult Create()
        {
            StoreGIssue item = new StoreGIssue();
            PopulateViewbags();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(StoreGIssue item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {    
                if (errMessage == "")
                {
                    var itemStock = _context.StoreGoodsStock.FirstOrDefault(x => x.SIGId == item.SIGId).SGSQty;
                    if (item.GIQty > itemStock)
                    {
                        TempData["SuccessMessage"] = "Out of Stock";
                        return View(item);
                    }
                    else
                    {
                        itemStock -= item.GIQty;
                        _context.StoreGoodsStock.FirstOrDefault(x => x.SIGId == item.SIGId).SGSQty = itemStock;
                        _context.SaveChanges();
                    }

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
                PopulateViewbags();
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "" + item.GIId.ToString() + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            StoreGIssue item = _Repo.GetItem(id);
            PopulateViewbags();
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            StoreGIssue item = _Repo.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StoreGIssue item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.GIId.ToString() + ", Saved Successfully";
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
            StoreGIssue item = _Repo.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(StoreGIssue item)
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
            TempData["SuccessMessage"] = item.GIId.ToString() + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }


        private List<SelectListItem> GetHRDepartments()
        {
            var lstDepartment = new List<SelectListItem>();
            PaginatedList<HRDepartment> items = _hrdepartmentRepo.GetItems("HRDName", SortOrder.Ascending, "", 1, 1000);
            lstDepartment = items.Select(ut => new SelectListItem()
            {
                Value = ut.HRDId.ToString(),
                Text = ut.HRDName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Department----"
            };
            lstDepartment.Insert(0, defItem);
            return lstDepartment;
        }
        private List<SelectListItem> GetStoreIGen()
        {
            var lstDepartment = new List<SelectListItem>();
            PaginatedList<StoreIGen> items = _storeigen.GetItems("SIGItemName", SortOrder.Ascending, "", 1, 1000);
            lstDepartment = items.Select(ut => new SelectListItem()
            {
                Value = ut.SIGId.ToString(),
                Text = ut.SIGItemCode + " - " + ut.SIGItemName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Item Name----"
            };
            lstDepartment.Insert(0, defItem);
            return lstDepartment;
        }
        private List<SelectListItem> GetStoreIGenCode()
        {
            var lstDepartment = new List<SelectListItem>();
            PaginatedList<StoreIGen> items = _storeigen.GetItems("SIGItemCode", SortOrder.Ascending, "", 1, 1000);
            lstDepartment = items.Select(ut => new SelectListItem()
            {
                Value = ut.SIGId.ToString(),
                Text = ut.SIGItemCode
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Item Code----"
            };
            lstDepartment.Insert(0, defItem);
            return lstDepartment;
        }
        private List<SelectListItem> GetCategory()
        {
            var storeCat = new List<SelectListItem>();
            PaginatedList<StoreCategory> items = _category.GetItems("SCName", SortOrder.Ascending, "", 1, 1000);
            storeCat = items.Select(ut => new SelectListItem()
            {
                Value = ut.SCId.ToString(),
                Text = ut.SCName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Category Name----"
            };
            storeCat.Insert(0, defItem);
            return storeCat;
        }
        private List<SelectListItem> GetSubCategory()
        {
            var subCat = new List<SelectListItem>();
            PaginatedList<StoreSCategory> items = _subCat.GetItems("SSCName", SortOrder.Ascending, "", 1, 1000);
            subCat = items.Select(ut => new SelectListItem()
            {
                Value = ut.SSCId.ToString(),
                Text = ut.SSCName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Sub-Category Name----"
            };
            subCat.Insert(0, defItem);
            return subCat;
        }
        private List<SelectListItem> GetUnit()
        {
            var unit = new List<SelectListItem>();
            PaginatedList<StoreUnit> items = _unit.GetItems("SUName", SortOrder.Ascending, "", 1, 1000);
            unit = items.Select(ut => new SelectListItem()
            {
                Value = ut.SUId.ToString(),
                Text = ut.SUName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Unit Name----"
            };
            unit.Insert(0, defItem);
            return unit;
        }
    }
}
