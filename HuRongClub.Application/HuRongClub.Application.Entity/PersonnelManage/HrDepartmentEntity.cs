using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// �� ��

    /// �� ��������
    /// �� �ڣ�2017-04-01 10:41
    /// �� ����������Ϣ
    /// </summary>
    public class HrDepartmentEntity : BaseEntity
    {
        #region ʵ���Ա

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

        #endregion ʵ���Ա

        #region ��չ����

        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            var keyv = Convert.ToInt32(keyValue);
            this.deptid = keyv;
        }

        #endregion ��չ����
    }
}