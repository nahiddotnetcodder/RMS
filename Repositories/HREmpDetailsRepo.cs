
using RMS.Models;
using System.Runtime.ConstrainedExecution;

namespace RMS.Repositories
{
    public class HREmpDetailsRepo : IHREmpDetails
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        private readonly IWebHostEnvironment _webHost;

        public HREmpDetailsRepo(RmsDbContext context, IWebHostEnvironment webHost) // will be passed by dependency injection.
        {
            _context = context;
            _webHost = webHost;
        }
        public HREmpDetails Create(HREmpDetails empdetails)
        {
            _context.HREmpDetails.Add(empdetails);
            _context.SaveChanges();
            return empdetails;
        }
    public HREmpDetails Delete(HREmpDetails empdetails)
        {
            _context.HREmpDetails.Attach(empdetails);
            _context.Entry(empdetails).State = EntityState.Deleted;
            _context.SaveChanges();
            return empdetails;
        }
        public HREmpDetails Edit(HREmpDetails empdetails)
        {
            _context.HREmpDetails.Attach(empdetails);
            _context.Entry(empdetails).State = EntityState.Modified;
            _context.SaveChanges();
            return empdetails;
        }
        private List<HREmpDetails> DoSort(List<HREmpDetails> items, string SortProperty, SortOrder sortOrder)
        {

            if (SortProperty.ToLower() == "HREDEId")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HREDEId).ToList();
                else
                    items = items.OrderByDescending(n => n.HREDEId).ToList();
            }
            else if (SortProperty.ToLower() == "RMItemCode")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HREDEName).ToList();
                else
                    items = items.OrderByDescending(n => n.HREDEName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRDId).ToList();
                else
                    items = items.OrderByDescending(d => d.HRDId).ToList();
            }
            return items;
        }
        public PaginatedList<HREmpDetails> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<HREmpDetails> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HREmpDetails.Where(n => n.HREDEId.Contains(SearchText) || n.HREDEName.Contains(SearchText))
                    .Include(u=>u.HRDepart)
                    .ToList();
            }
            else
                items = _context.HREmpDetails.Include(u => u.HRDepart).ToList();
                items = _context.HREmpDetails.Include(u => u.HRDesig).ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<HREmpDetails> retItems = new PaginatedList<HREmpDetails>(items, pageIndex, pageSize);
                return retItems;
        }
        public HREmpDetails GetItem(int edid)
        {
            HREmpDetails item = _context.HREmpDetails.Where(u => u.HREDId == edid)
                .Include(u => u.HRDepart)
                .FirstOrDefault();
            item.HREDBPhotoName = GetBriefPhotoName(item.HREDPUrl);
            return item;
        }
        private string GetBriefPhotoName(string fileName)
        {
            if (fileName == null)
                return "";
            string[] words = fileName.Split('_');
            return words[words.Length - 1];
        }
        public bool IsEmpCodeExists(string empcode)
        {
            int ct = _context.HREmpDetails.Where(n => n.HREDEId.ToLower() == empcode.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsEmpCodeExists(string empcode, string empname)
        {
            if (empname == "")
                return IsEmpCodeExists(empcode);
            var strName = _context.HREmpDetails.Where(n => n.HREDEId == empcode).Max(nm => nm.HREDEName);
            if (strName == null || strName == empname)
                return false;
            else          
                return IsEmpCodeExists(empname);                                         
        }

        
    }
}
