using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage.ViewModel
{
    /// <summary>
    /// 打印票据回传参数
    /// </summary>
    public class TicketPrintEntity
    {
        public string feeitem_id { get; set; }

        public decimal fmoney { get; set; }

        public string speriod { get; set; }

        public string feedispname { get; set; }

        public string taxrate { get; set; }

        public string taxtype { get; set; }
    }
}
