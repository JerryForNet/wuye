using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.IService.TenementManage
{
    public interface PaymentSerachIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="ban">所在楼栋</param>
        /// <param name="unit">所在单元</param>
        /// <param name="type">1删除费用 2减免费用</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PaymentSerachEntity> GetPageList(Pagination pagination, string ban, string unit, int type);

        #endregion
    }
}
