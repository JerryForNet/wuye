using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 缴费查询实体
    /// </summary>
    public class OtherincomeFromEntity
    {
        /// <summary>
        /// 业主实体
        /// </summary>
        public class OtherFeereceiveFromEntity
        {
            /// <summary>
            /// 编号
            /// </summary>
            public string receive_id { set; get; }
            /// <summary>
            /// 物业编号
            /// </summary>
            public string property_id { set; get; }
            /// <summary>
            /// 业主
            /// </summary>
            public string ownername { set; get; }
            /// <summary>
            /// 合同名称
            /// </summary>
            public string customername { set; get; }
            /// <summary>
            /// 物业
            /// </summary>
            public string propertyname { set; get; }
            /// <summary>
            /// 收费日期
            /// </summary>
            public string receive_date { set; get; }
            /// <summary>
            /// 收费金额
            /// </summary>
            public decimal? fee_money { set; get; }
            /// <summary>
            /// 票据号码
            /// </summary>
            public string ticket_code { set; get; }
            /// <summary>
            /// 支付方式
            /// </summary>
            public string pay_mode { set; get; }
            /// <summary>
            /// 类型0 业主 1租户
            /// </summary>
            public string type { set; get; }
        }
        /// <summary>
        /// 应 收 收 入 明 细
        /// </summary>
        public class OtherFeereceiveListEntity
        {
            /// <summary>
            /// 核销序号
            /// </summary>
            public string check_id { set; get; }
            /// <summary>
            /// 实收编号
            /// </summary>
            public string receive_id { set; get; }
            /// <summary>
            /// 应收编号
            /// </summary>
            public string income_id { set; get; }
            /// <summary>
            /// 核销金额
            /// </summary>
            public decimal? check_money { set; get; }
            /// <summary>
            /// 摘要
            /// </summary>
            public string subject { set; get; }
            /// <summary>
            /// 时间
            /// </summary>
            public string pay_enddate { set; get; }
            /// <summary>
            /// 费用项目名称
            /// </summary>
            public string feeitem_name { set; get; }
        }
    }
}
