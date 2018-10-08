using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 小区台帐
    /// </summary>
    [NotMapped]
    public class AccountTZEntity : FeeincomeEntity
    {
        /// <summary>
        /// 物业
        /// </summary>
        public string building_name { set; get; }
        /// <summary>
        /// 单元
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 住户
        /// </summary>
        public string owner_name { set; get; }

        public string fee_income_1 { get; set; }

        public string fee_income_2 { get; set; }

        public string fee_income_3 { get; set; }

        public string fee_income_4 { get; set; }

        public string fee_income_5 { get; set; }

        public string fee_income_6 { get; set; }

        public string fee_income_7 { get; set; }

        public string fee_income_8 { get; set; }

        public string fee_income_9 { get; set; }

        public string fee_income_10 { get; set; }

        public string fee_income_11 { get; set; }

        public string fee_income_12 { get; set; }

        public string fee_already_1 { get; set; }

        public string fee_already_2 { get; set; }

        public string fee_already_3 { get; set; }

        public string fee_already_4 { get; set; }

        public string fee_already_5 { get; set; }

        public string fee_already_6 { get; set; }

        public string fee_already_7 { get; set; }

        public string fee_already_8 { get; set; }

        public string fee_already_9 { get; set; }

        public string fee_already_10 { get; set; }

        public string fee_already_11 { get; set; }

        public string fee_already_12 { get; set; }



    }
    /// <summary>
    /// 小区台帐GroupBy
    /// </summary>
    public class AccountGroupByEntity
    {
        /// <summary>
        /// 单元编号
        /// </summary>
        public string room_id { set; get; }
        /// <summary>
        /// 物业
        /// </summary>
        public string building_name { set; get; }
        /// <summary>
        /// 单元
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 住户
        /// </summary>
        public string owner_name { set; get; }
    }
}
