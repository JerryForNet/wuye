using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HuRongClub.Application.Web.Controllers
{
    public class PostTestController : ApiController
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="ticket_id"></param>
        /// <param name="FeeincomeEntryJson"></param>
        /// <returns></returns>
        public string Get(string keyValue, string ticket_id, string FeeincomeEntryJson)
        {
            
            return "操作成功。";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post(PostBody model)
        {
            string keyValue = model.keyValue;
            string ticket_id = model.ticket_id;
            string FeeincomeEntryJson = model.FeeincomeEntryJson;

            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var FeeincomeEntryList = FeeincomeEntryJson.ToList<FeeincomeAdjustEntity>();

            List<FeeincomeEntity> list_f = new List<FeeincomeEntity>();
            FeeticketEntity end_ft = new FeeticketEntity();
            List<FeereceiveEntity> list_fe = new List<FeereceiveEntity>();
            List<FeecheckEntity> list_fK = new List<FeecheckEntity>();
            FeereceiveBLL bll = new FeereceiveBLL();
            int maxid = bll.GetMaxID(0).ToInt();

            FeecheckBLL bll_f = new FeecheckBLL();
            int maxid_f = bll_f.GetMaxID(0).ToInt();

            foreach (FeeincomeAdjustEntity item in FeeincomeEntryList)
            {
                #region 费用应收

                FeeincomeEntity mod_f = new FeeincomeEntity();
                mod_f.fee_already = item.fee_already + item.fee_income;
                mod_f.fee_date = item.receive_date;
                mod_f.userid = "李俊";
                mod_f.inputtime = DateTime.Now;
                mod_f.income_id = item.income_id;

                list_f.Add(mod_f);

                #endregion

                #region 费用实收

                FeereceiveEntity ent_fe = new FeereceiveEntity();
                ent_fe.receive_id = property_id + Utils.SupplementZero(maxid.ToString(), 8);
                ent_fe.property_id = property_id;
                ent_fe.receive_date = item.receive_date;
                ent_fe.ticket_id = ticket_id;
                ent_fe.owner_id = item.owner_id;
                ent_fe.rentcontract_id = item.rentcontract_id;
                ent_fe.fee_money = item.fee_income;
                ent_fe.userid = "李俊";
                ent_fe.inputtime = DateTime.Now;
                ent_fe.room_id = item.room_id;
                if (item.isprint == "1")
                {
                    ent_fe.isprint = item.isprint;
                    ent_fe.printname = item.printname;
                }

                list_fe.Add(ent_fe);

                #endregion

                #region 收费核销

                FeecheckEntity ent_fk = new FeecheckEntity();
                ent_fk.check_id = property_id + Utils.SupplementZero(maxid_f.ToString(), 8);
                ent_fk.receive_id = ent_fe.receive_id;
                ent_fk.income_id = item.income_id;
                ent_fk.check_money = item.fee_income;

                list_fK.Add(ent_fk);

                #endregion

                maxid++;
                maxid_f++;
            }

            #region 发票领用

            end_ft.ticket_id = ticket_id;
            end_ft.ticket_status = 1;

            #endregion

            FeeincomeBLL feeincomebll = new FeeincomeBLL();
            feeincomebll.FixedCost(list_f, end_ft, list_fe, list_fK);

            return "操作成功。";
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class PostBody
    {
        public string keyValue { get; set; }

        public string ticket_id { get; set; }

        public string paydate { get; set; }

        public string typelist { get; set; }

        public string codelist { get; set; }

        public string FeeincomeEntryJson { get; set; }
    }
}