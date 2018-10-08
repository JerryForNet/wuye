using System;
using HuRongClub.Application.Code;
using HuRongClub.Util.Extension;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    public class LblistEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// lid
        /// </summary>
        /// <returns></returns>
        public int lid { get; set; }
        /// <summary>
        /// 品种编号
        /// </summary>
        /// <returns></returns>
        public int dictitemid { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary>
        /// <returns></returns>
        public string lbname { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        /// <returns></returns>
        public string lbguige { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        /// <returns></returns>
        public string lbcolor { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        /// <returns></returns>
        public Int16 deptid { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        public string deptname { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        /// <returns></returns>
        public int empid { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        /// <returns></returns>
        public string empname { get; set; }
        /// <summary>
        /// 领意时间
        /// </summary>
        /// <returns></returns>
        public DateTime lbbegindate { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        /// <returns></returns>
        public DateTime? lbenddate { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        public Int16? inputuserid { get; set; }
        /// <summary>
        /// 发放周期
        /// </summary>
        /// <returns></returns>
        public Int16 lbmonth { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public Int16? isnew { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public string lbcount { get; set; }
        #endregion

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
            // 主健只能为 uniqueidentifier 或 int 
            this.lid = keyValue.ToInt();
        }
        #endregion
    }
}