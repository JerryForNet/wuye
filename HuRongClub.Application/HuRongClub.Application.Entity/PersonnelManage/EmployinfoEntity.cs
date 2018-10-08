using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本

    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    public class EmployinfoEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// empid
        /// </summary>
        /// <returns></returns>
        public int empid { get; set; }

        /// <summary>
        /// empcode
        /// </summary>
        /// <returns></returns>
        public string empcode { get; set; }

        /// <summary>
        /// empname
        /// </summary>
        /// <returns></returns>
        public string empname { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        /// <returns></returns>
        public string sex { get; set; }

        /// <summary>
        /// birthday
        /// </summary>
        /// <returns></returns>
        public DateTime? birthday { get; set; }

        /// <summary>
        /// deptid
        /// </summary>
        /// <returns></returns>
        public int? deptid { get; set; }

        /// <summary>
        /// hiredate
        /// </summary>
        /// <returns></returns>
        public DateTime? hiredate { get; set; }

        /// <summary>
        /// firedate
        /// </summary>
        /// <returns></returns>
        public DateTime? firedate { get; set; }

        /// <summary>
        /// address
        /// </summary>
        /// <returns></returns>
        public string address { get; set; }

        /// <summary>
        /// postcode
        /// </summary>
        /// <returns></returns>
        public string postcode { get; set; }

        /// <summary>
        /// homephone
        /// </summary>
        /// <returns></returns>
        public string homephone { get; set; }

        /// <summary>
        /// mobilephone
        /// </summary>
        /// <returns></returns>
        public string mobilephone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        /// <returns></returns>
        public string email { get; set; }

        /// <summary>
        /// identityno
        /// </summary>
        /// <returns></returns>
        public string identityno { get; set; }

        /// <summary>
        /// status
        /// </summary>
        /// <returns></returns>
        public Int16 status { get; set; }

        /// <summary>
        /// photofile
        /// </summary>
        /// <returns></returns>
        public string photofile { get; set; }

        /// <summary>
        /// inputtime
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }

        /// <summary>
        /// ifmarry
        /// </summary>
        /// <returns></returns>
        public int? ifmarry { get; set; }

        /// <summary>
        /// operateuser
        /// </summary>
        /// <returns></returns>
        public string operateuser { get; set; }

        /// <summary>
        /// politicsid
        /// </summary>
        /// <returns></returns>
        public int? politicsid { get; set; }

        /// <summary>
        /// specialty
        /// </summary>
        /// <returns></returns>
        public string specialty { get; set; }

        /// <summary>
        /// degreeid
        /// </summary>
        /// <returns></returns>
        public int? degreeid { get; set; }

        /// <summary>
        /// dutyid
        /// </summary>
        /// <returns></returns>
        public int? dutyid { get; set; }

        /// <summary>
        /// techclassid
        /// </summary>
        /// <returns></returns>
        public int? techclassid { get; set; }

        /// <summary>
        /// jobid
        /// </summary>
        /// <returns></returns>
        public int? jobid { get; set; }

        /// <summary>
        /// contracttypeid
        /// </summary>
        /// <returns></returns>
        public int? contracttypeid { get; set; }

        /// <summary>
        /// careerid
        /// </summary>
        /// <returns></returns>
        public int? careerid { get; set; }

        /// <summary>
        /// careerdegree
        /// </summary>
        /// <returns></returns>
        public int? careerdegree { get; set; }

        /// <summary>
        /// contracttime
        /// </summary>
        /// <returns></returns>
        public int? contracttime { get; set; }

        /// <summary>
        /// indate
        /// </summary>
        /// <returns></returns>
        public DateTime? indate { get; set; }

        /// <summary>
        /// contractnotime
        /// </summary>
        /// <returns></returns>
        public int? contractnotime { get; set; }

        /// <summary>
        /// notes
        /// </summary>
        /// <returns></returns>
        public string notes { get; set; }

        /// <summary>
        /// contractfromid
        /// </summary>
        /// <returns></returns>
        public int? contractfromid { get; set; }

        /// <summary>
        /// fireoutdate
        /// </summary>
        /// <returns></returns>
        public DateTime? fireoutdate { get; set; }

        #endregion 实体成员

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.empid = Guid.NewGuid().ToString();
        //}

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>

        public override void Modify(string keyValue)
        {
            int _empid = 0;
            int.TryParse(keyValue, out _empid);
            this.empid = _empid;
        }

        #endregion 扩展操作
    }
}