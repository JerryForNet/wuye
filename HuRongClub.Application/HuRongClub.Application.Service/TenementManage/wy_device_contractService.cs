using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:34
    /// 描 述：wy_device_contract
    /// </summary>
    public class wy_device_contractService : RepositoryFactory<wy_device_contractEntity>, wy_device_contractIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<wy_device_contractEntity> GetList(string queryJson)
        {
            RepositoryFactory<wy_device_contractEntity> repository = new RepositoryFactory<wy_device_contractEntity>();
            var queryParam = queryJson.ToJObject();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();

            strSql.Append("select * from  wy_device_contract  where 1=1");

            if (!queryParam["devicenumber"].IsEmpty())
            {
                string devicenumber = queryParam["devicenumber"].ToString();
                strSql.Append("  and devicenumber=@devicenumber");
                parameter.Add(DbParameters.CreateDbParameter("@devicenumber", devicenumber));
            }


            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public wy_device_contractEntity GetEntity(string keyValue)
        {
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();

            strSql.Append("select * from  wy_device_contract  where 1=1");

            strSql.Append("  and pkeyid=@pkeyid");
            parameter.Add(DbParameters.CreateDbParameter("@pkeyid", keyValue));
            return base.BaseRepository().FindList(strSql.ToString(), parameter.ToArray()).FirstOrDefault();

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
        public void SaveForm(string keyValue, wy_device_contractEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.pkeyid = Util.Utils.GetCookie("property_id") +DeviceService.GetMaxID_String("right(pkeyid,8)", "wy_device_contract", 8);
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
