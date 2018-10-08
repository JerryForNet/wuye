using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System;
using System.Data.SqlClient;
using System.Data;


namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-05-02 18:41
    /// 描 述：Paydetail
    /// </summary>
    public interface PaydetailIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
       IEnumerable<PaydetailEntity> GetPageList(Pagination pagination, string queryJson);


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<PaydetailEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        PaydetailEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);

        void Delete(int empid, int payrollid);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, PaydetailEntity entity);
        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        DataTable GetPageListToTable(string queryJson, string keyValue);
    }
}
