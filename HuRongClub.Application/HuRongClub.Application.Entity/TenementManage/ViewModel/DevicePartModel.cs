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
    public class DevicePartModel : DevicePartEntity
    {
        /// <summary>
        /// d_number
        /// </summary>
        /// <returns></returns>
        public string d_number { get; set; }
        /// <summary>
        /// d_name
        /// </summary>
        /// <returns></returns>
        public string d_name { get; set; }
        /// <summary>
        /// d_place
        /// </summary>
        /// <returns></returns>
        public string d_place { get; set; }
        /// <summary>
        /// itemname
        /// </summary>
        /// <returns></returns>
        public string itemname { get; set; }

        public string maintencename1 { get; set; }
        public string maintencename2 { get; set; }
        public string maintencename3 { get; set; }
        public string maintencename4 { get; set; }
        public string maintencename5 { get; set; }
        public string maintencename6 { get; set; }
        public string maintencename7 { get; set; }
        public string maintencename8 { get; set; }
        public string maintencename9 { get; set; }
        public string maintencename10 { get; set; }
        public string maintencename11 { get; set; }
        public string maintencename12 { get; set; }
        public string p_name { get; set; }
    }
}
