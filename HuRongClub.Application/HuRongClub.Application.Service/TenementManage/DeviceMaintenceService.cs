using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HuRongClub.Util;
using System.Text;
namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-04-28 21:58
    /// 描 述：wy_Device_maintence
    /// </summary>
    public class DeviceMaintenceService : RepositoryFactory<DeviceMaintenceEntity>, DeviceMaintenceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DeviceMaintenceEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //查询条件
            string p_number = queryParam["p_number"].ToString();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT  * FROM dbo.wy_Device_maintence ");
            strSql.AppendFormat("WHERE p_number='{0}' ", p_number);
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DeviceMaintenceEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, DeviceMaintenceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.classid = Util.Utils.GetCookie("property_id") + DeviceService.GetMaxID_String("right(replace(classid,'-',''),8)", "wy_Device_maintence", 8);
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
