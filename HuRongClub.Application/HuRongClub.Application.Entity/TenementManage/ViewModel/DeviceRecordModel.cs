using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class DeviceRecordModel : DeviceRecordEntity
    {
        /// <summary>
        /// fyearmonth
        /// </summary>
        public string fyearmonth { get; set; }
        /// <summary>
        /// ostatus
        /// </summary>
        public string ostatus { get; set; }  
        /// <summary>
        /// p_name
        /// </summary>
        public string p_name { get; set; }


        /// <summary>
        /// p_place
        /// </summary>
        public string p_place { get; set; }
        /// <summary>
        /// p_standard
        /// </summary>
        public string p_standard { get; set; }
        /// <summary>
        /// p_number
        /// </summary>
        public string p_number { get; set; }



        /// <summary>
        /// d_number
        /// </summary>
        public string d_number { get; set; }
        /// <summary>
        /// d_name
        /// </summary>
        public string d_name { get; set; }
        /// <summary>
        /// d_place
        /// </summary>
        public string d_place { get; set; }
        /// <summary>
        /// d_standard
        /// </summary>
        public string d_standard { get; set; }
        /// <summary>
        /// lastdate
        /// </summary>
        public string lastdate { get; set; }
        /// <summary>
        /// lastman
        /// </summary>
        public string lastman { get; set; }
        /// <summary>
        /// lastinfo
        /// </summary>
        public string lastinfo { get; set; }





        /// <summary>
        /// maintencename
        /// </summary>
        public string maintencename { get; set; }
        /// <summary>
        /// maintence
        /// </summary>
        public string maintence { get; set; }
        /// <summary>
        /// property_name
        /// </summary>
        public string property_name { get; set; }
        /// <summary>
        /// itemname
        /// </summary>
        public string itemname { get; set; }


    }
}
