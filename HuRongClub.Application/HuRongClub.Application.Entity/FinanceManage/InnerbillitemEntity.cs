using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.FinanceManage.ViewModel
{
    public class InnerbillitemEntity
    {
        #region 实体成员

        /// <summary>
        /// billitem_id
        /// </summary>
        /// <returns></returns>
        public string billitem_id { get; set; }

        /// <summary>
        /// billid
        /// </summary>
        /// <returns></returns>
        public string billid { get; set; }

        /// <summary>
        /// feeitem_id
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }

        /// <summary>
        /// summary
        /// </summary>
        /// <returns></returns>
        public string summary { get; set; }

        /// <summary>
        /// feemoney
        /// </summary>
        /// <returns></returns>
        public decimal feemoney { get; set; }

        #endregion 实体成员
    }
}