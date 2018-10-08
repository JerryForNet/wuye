using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本

    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    public class HrDepartmentEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// deptid
        /// </summary>
        /// <returns></returns>
        public int? deptid { get; set; }

        /// <summary>
        /// deptname
        /// </summary>
        /// <returns></returns>
        public string deptname { get; set; }

        /// <summary>
        /// manager
        /// </summary>
        /// <returns></returns>
        public string manager { get; set; }

        /// <summary>
        /// phone
        /// </summary>
        /// <returns></returns>
        public string phone { get; set; }

        /// <summary>
        /// managerphone
        /// </summary>
        /// <returns></returns>
        public string managerphone { get; set; }

        #endregion 实体成员

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            var keyv = Convert.ToInt32(keyValue);
            this.deptid = keyv;
        }

        #endregion 扩展操作
    }
}