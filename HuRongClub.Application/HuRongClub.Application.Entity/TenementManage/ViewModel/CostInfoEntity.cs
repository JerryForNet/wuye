using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class CostInfoEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string property_name { get; set; }

        /// <summary>
        /// 业主
        /// </summary>
        public string ownername { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime receive_date { get; set; }

        /// <summary>
        /// 票据号码
        /// </summary>
        public string ticket_code { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string pay_mode { get;set; }

        /// <summary>
        /// 其他说明
        /// </summary>
        public string propertyname { get; set; }


        /// <summary>
        /// id
        /// </summary>
        public string property_id { get; set; }

        /// <summary>
        /// 收入id
        /// </summary>
        public string incomeid { get; set; }


        public string receive_id { get; set; }

        public string customername { get; set; }

        public string fee_money { get; set; }



        public bool is_income { get; set; }


        public bool is_otherIncome { get; set; }
    }
}
