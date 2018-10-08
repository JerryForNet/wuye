using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27
    /// 描 述：PaymentSerach
    public class PaymentSerachService : RepositoryFactory<PaymentSerachEntity>, PaymentSerachIService
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
        public IEnumerable<PaymentSerachEntity> GetPageList(Pagination pagination, string ban, string unit, int type)
        {
            var strSql = new StringBuilder();
            strSql.Append("select itemid,contract_id,('/'+(select top 1 building_name from wy_building where property_id=wy_feechangelog.property_id)+'/'+(select top 1 room_name from wy_room where room_id=wy_feechangelog.room_id)+'/'+(select top 1 owner_name from wy_owner where owner_id=wy_feechangelog.owner_id)) as room_name  ");
            strSql.Append(",source_money,new_money,(select top 1 building_name from wy_building where building_id=(select top 1 building_id from wy_room where room_id=wy_feechangelog.room_id)) as bname,(select top 1 feeitem_name from wy_feeitem where feeitem_id=wy_feechangelog.feeitem_id ) as  feename,operatetime,operatername,income_date,memo ");
            strSql.Append(" from wy_feechangelog  where 1=1 ");
            if (ban != "")
            {
                strSql.Append(" AND property_id=@property_id");
            }
            if (unit != "")
            {
                strSql.Append(" AND room_id=@room_id");
            }

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@property_id",ban),
                    DbParameters.CreateDbParameter("@room_id",unit)
                };

            RepositoryFactory<PaymentSerachEntity> repository = new RepositoryFactory<PaymentSerachEntity>();
            if (pagination != null)
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter, pagination);//.OrderBy(t => t.operatetime);
            }
            else
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter);//.OrderBy(t => t.operatetime);
            }
        }
        #endregion


    }
}
