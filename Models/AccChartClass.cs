using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class AccChartClass
    {
        [Key]
        public int ACCId { get; set; }
        [StringLength(100)]
        public string ACCName { get; set; }
        public ACCCType ACCCType { get; set; }
        [StringLength(50)]
        public string CUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

namespace RMS.Models
{
    public enum ACCCType
    {
        [Description("Assets")] Assets, [Description("Liabilities")] Liabilities, [Description("Equity")] Equity, [Description("Income")] Income, [Description("Cost of Goods Sold")] CostofGoodsSold, [Description("Expense")] Expense
    }
}