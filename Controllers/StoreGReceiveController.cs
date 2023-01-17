using DevExpress.Printing.Utils.DocumentStoring;
using DevExpress.XtraEditors.Filtering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace RMS.Controllers
{
    [Authorize]
    public class StoreGReceiveController : Controller
    {
        private readonly IStoreGReceive _Repo;
        private readonly IStoreIGen _storeigen;
        private readonly IStoreGoodsStock _goodsStock;
        private readonly IStoreCategory _storeCat;
        private readonly IStoreSCategory _subCat;
        private readonly IStoreUnit _unit;
        private readonly RmsDbContext _context;

        public StoreGReceiveController(IStoreGReceive repo, IStoreIGen storeigen, IStoreGoodsStock goodsStock, IStoreCategory storeCat, IStoreSCategory subCat, IStoreUnit unit,RmsDbContext context) // here the repository will be passed by the dependency injection.
        {
            _Repo = repo;
            _storeigen = storeigen;
            _goodsStock = goodsStock;
            _storeCat = storeCat;
            _subCat = subCat;
            _unit = unit;
            _context = context;
        }
        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("GRId");
            sortModel.AddColumn("GRDate");
            sortModel.AddColumn("StoreCategory");
            sortModel.AddColumn("StoreSCategory");
            sortModel.AddColumn("StoreUnit");
            sortModel.AddColumn("StoreIGens");
            sortModel.AddColumn("GRQty");
            sortModel.AddColumn("GRUPrice");
            sortModel.AddColumn("GRTPrice");
            sortModel.AddColumn("GRRemarks");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<StoreGReceive> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }
        private void PopulateViewbags()
        {
            ViewBag.StoreCategory = GetCategory();
            ViewBag.StoreSCategory = GetSubCategory();
            ViewBag.StoreUnits = GetUnit();
            ViewBag.IGen = GetStoreIGen();
            ViewBag.IGenCode = GetStoreIGenCode();
        }

        public IActionResult Create()
        {
            StoreGReceive item = new StoreGReceive();
            PopulateViewbags();
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(StoreGReceive item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    var existingRecord = _context.StoreGoodsStock.FirstOrDefault(x => x.SIGId == item.SIGId);
                    if (existingRecord != null)
                    {
                        existingRecord.SGSQty += item.GRQty;
                        existingRecord.SGSUPrice += item.GRTPrice;
                        _context.SaveChanges();
                    }
                    else
                    {
                        var newRecord = new StoreGoodsStock { SIGId = item.SIGId, SGSQty = item.GRQty, SGSUPrice=item.GRTPrice };
                        _context.StoreGoodsStock.Add(newRecord);
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
                TempData["SuccessMessage"] = "" + item.GRId.ToString() + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Details(int id) //Read
        {
            StoreGReceive item = _Repo.GetItem(id);
            PopulateViewbags();
            return View(item);
        }
        public IActionResult Edit(int id)
        {
            StoreGReceive item = _Repo.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StoreGReceive item)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] ="Saved Successfully";
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
            StoreGReceive item = _Repo.GetItem(id);
            PopulateViewbags();
            TempData.Keep();
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(StoreGReceive item)
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
            TempData["SuccessMessage"] = item.GRId.ToString() + " Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
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
                Text = ut.SIGItemCode + " - " + ut.SIGItemName
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
            PaginatedList<StoreCategory> items = _storeCat.GetItems("SCName", SortOrder.Ascending, "", 1, 1000);
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
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsItemExists(string code, int id)
        {
            bool isExists = _Repo.IsItemExists(code, id);

            if (isExists)
                return Json(data: false);
            else
                return Json(data: true);
        }
    }
}

