using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    public class DistrictModel
    {
        public string feeitem_id { get; set; }
        public string property_id { get; set; }
        public decimal yinshou { get; set; }
        public decimal shishou { get; set; }
        public decimal zhuijiao { get; set; }
        public decimal yue { get; set; }

        public string property_name { get; set; }
    }
}
