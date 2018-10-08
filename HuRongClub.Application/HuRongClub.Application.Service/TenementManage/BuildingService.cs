using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-07 17:17
    /// 描 述：Building
    /// </summary>
    public class BuildingService : RepositoryFactory<BuildingEntity>, BuildingIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BuildingEntity> GetPageList(Pagination pagination, string queryJson, string property_id)
        {
            var expression = LinqExtensions.True<BuildingEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件            
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.building_name.Equals(keyword));
            } 
            expression = expression.And(t => t.property_id == property_id);

            return this.BaseRepository().FindList(expression, pagination);

        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BuildingEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<BuildingEntity>();
            expression = expression.And(t => t.property_id == queryJson);

            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.property_id);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BuildingEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(building_id,4))+1 FROM wy_building");
            string str = "1";
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj != null)
            {
                str = obj.ToString();
            }
            if (str.Length < pos)
            {
                int leng = str.Length;
                for (int i = 0; i < (pos - leng); i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取楼层名称
        /// </summary>
        /// <param name="building_id">楼栋编号</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public string Getfloor_list(string building_id,string property_id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select floor_list FROM wy_building where building_id='"+ building_id + "' and property_id='"+ property_id + "'");
            string str = "";
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj != null)
            {
                str = obj.ToString();
            }
            return str;
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
        public void SaveForm(string keyValue, BuildingEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //entity.Create();
                entity.building_id = entity.property_id + GetMaxID(4);
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
