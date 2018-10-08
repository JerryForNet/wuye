using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// �� ����HurongClub.Framework V1.0.1
    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    public class GoodstypeEntity : BaseEntity
    {
        #region ʵ���Ա

        /// <summary>
        /// ftypecode
        /// </summary>
        /// <returns></returns>
        public string ftypecode { get; set; }

        /// <summary>
        /// fparentcode
        /// </summary>
        /// <returns></returns>
        public string fparentcode { get; set; }

        /// <summary>
        /// frootid
        /// </summary>
        /// <returns></returns>
        public string frootid { get; set; }

        /// <summary>
        /// flayer
        /// </summary>
        /// <returns></returns>
        public int? flayer { get; set; }

        /// <summary>
        /// ftypename
        /// </summary>
        /// <returns></returns>
        public string ftypename { get; set; }

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
            this.ftypecode = keyValue;
        }

        #endregion ��չ����
    }
}