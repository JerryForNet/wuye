using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using System.Text;
using HuRongClub.Data;
using System.Data.Common;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:36
    /// 描 述：wy_DeviceFix
    /// </summary>
    public class wy_DeviceFixService : RepositoryFactory<wy_DeviceFixEntity>, wy_DeviceFixIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<wy_DeviceFixEntity> GetList(string queryJson)
        {
          
            var strSql = new StringBuilder();
            strSql.Append(" SELECT  * FROM dbo.wy_Devicefix where 1=1");

              var queryParam = queryJson.ToJObject();
              var parameter = new List<DbParameter>();

            //查询条件
              if (!queryParam["devicenumber"].IsEmpty())
            {
                string devicenumber = queryParam["devicenumber"].ToString();
                strSql.Append("  and devicenumber=@devicenumber");
                parameter.Add(DbParameters.CreateDbParameter("@devicenumber", devicenumber));
            }
           return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public wy_DeviceFixEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, wy_DeviceFixEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.DeviceFixID = Util.Utils.GetCookie("property_id") + DeviceService.GetMaxID_String("right(DeviceFixID,8)", "wy_DeviceFix", 8);
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
