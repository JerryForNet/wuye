using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 费用导入
    /// </summary>
    public class FeeManageEntity
    {
        /// <summary>
        /// 房间编号
        /// </summary>
        public string rid { set; get; }
        /// <summary>
        /// 业主编号
        /// </summary>
        public string oid { set; get; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? dim { set; get; }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string oname { set; get; }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal? fee_income { set; get; }
    }
}
