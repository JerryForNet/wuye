using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� ����������
    /// �� �ڣ�2017-04-01 10:09
    /// �� ������ҵ����
    /// </summary>
    public class PropertyEntity : BaseEntity
    {
        #region ʵ���Ա

        /// <summary>
        /// ��ҵ���
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }

        /// <summary>
        /// ��ҵ����
        /// </summary>
        /// <returns></returns>
        public string property_name { get; set; }

        /// <summary>
        /// ��ҵ��ַ
        /// </summary>
        /// <returns></returns>
        public string property_address { get; set; }

        /// <summary>
        /// ��ҵ��Ϣ
        /// </summary>
        /// <returns></returns>
        public string property_info { get; set; }

        /// <summary>
        /// ��ҵƽ��ͼ
        /// </summary>
        /// <returns></returns>
        public string image { get; set; }

        /// <summary>
        /// ͼ���
        /// </summary>
        /// <returns></returns>
        public decimal? image_width { get; set; }

        /// <summary>
        /// ͼ�߶�
        /// </summary>
        /// <returns></returns>
        public decimal? image_height { get; set; }

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
            this.property_id = keyValue;
        }

        #endregion ��չ����
    }
}