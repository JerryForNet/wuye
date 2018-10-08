using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    public class ArraersViewModel
    {
        public string building_name { get; set; }

        public string room_name { get; set; }

        public int CountNum { get; set; }

        public string CustomerName { get; set; }

        public decimal ArrearageAmount { get; set; }


        public string Type_name { get; set; }


        public string owner_name { get; set; }
        public string room_id { get; set; }
        public string rentcontract_id { get; set; }
    }
}
