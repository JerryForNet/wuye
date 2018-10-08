using AutoMapper;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-26 16:36
    /// 描 述：费用管理
    /// </summary>
    [HandlerOperateLog]
    public class FeeManageController : MvcControllerBase
    {
        #region 视图功能

        /// <summary>
        /// 费用管理界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeeManageIndex()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="type">leix</param>
        /// <param name="id">查询参数</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetListJson(int type, string id)
        {
            RoomBLL bll = new RoomBLL();
            if (type == 2)
            {
                if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
                {
                    id = Utils.GetCookie("property_id");
                }
            }

            bll.GetListByFee(type, id);

            return Success("导出成功。");
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 业主费用登记
        /// </summary>
        /// <param name="queryJson">数据</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveOwnerFrom(string queryJson)
        {
            FeeincomeEntity ent = new FeeincomeEntity();
            int type = 0;

            #region 赋值

            var queryParam = queryJson.ToJObject();
            if (!queryParam["fee_year"].IsEmpty())
            {
                ent.fee_year = Convert.ToInt16(queryParam["fee_year"]);
            }
            if (!queryParam["fee_month"].IsEmpty())
            {
                ent.fee_month = Convert.ToInt16(queryParam["fee_month"]);
            }
            if (!queryParam["type"].IsEmpty())
            {
                type = queryParam["type"].ToInt();
            }
            if (type == 1)
            {
                if (!queryParam["building_id"].IsEmpty())
                {
                    ent.building_id = queryParam["building_id"].ToString();
                }
                if (!queryParam["owner_id"].IsEmpty())
                {
                    string[] owner_id = queryParam["owner_id"].ToString().Split('|');
                    ent.owner_id = owner_id[1];
                    ent.room_id = owner_id[0];
                }
            }
            else
            {
                if (!queryParam["rentcontract_id"].IsEmpty())
                {
                    ent.rentcontract_id = queryParam["rentcontract_id"].ToString();
                }
            }
            if (!queryParam["feeitem_id"].IsEmpty())
            {
                ent.feeitem_id = queryParam["feeitem_id"].ToString();
            }
            if (!queryParam["fee_income"].IsEmpty())
            {
                ent.fee_income = queryParam["fee_income"].ToDecimal();
            }
            if (!queryParam["notes"].IsEmpty())
            {
                ent.notes = queryParam["notes"].ToString();
            }
            if (!queryParam["pay_enddate"].IsEmpty())
            {
                ent.pay_enddate = queryParam["pay_enddate"].ToDate();
            }
            ent.property_id = Utils.GetCookie("property_id");
            ent.start_date = (ent.fee_year + "-" + ent.fee_month + "-01").ToDate();
            ent.end_date = (ent.fee_year + "-" + ent.fee_month + "-25").ToDate();
            ent.userid = Code.OperatorProvider.Provider.Current().UserName;
            ent.inputtime = DateTime.Now;
            ent.fee_already = 0;

            #endregion

            DateTime endtime = ent.pay_enddate.ToDate();
            if (ent.fee_year.ToInt() == endtime.Year)
            {
                if (endtime.Month < ent.fee_month)
                {
                    return Error("应收截止日期不能小于记帐月份！");
                }
                else
                {
                    if (endtime.Month - ent.fee_month.ToInt() > 1)
                    {
                        return Error("应收截止日期不能大于2个月！");
                    }
                }
            }
            else
            {
                if (((endtime.Year - ent.fee_year.ToInt() != 1) && ent.fee_month != 1) && (ent.fee_month != 12))
                {
                    return Error("应收截止日期不能大于2个月！");
                }
            }

            FeeincomeBLL feeincomebll = new FeeincomeBLL();
            
            // 业主费用登记和租户费用登记查询的条件不一样 update by:Jery.Li Time:2017/11/29
            string operateId = ent.room_id;
            if (type == 2)
            {
                operateId = ent.rentcontract_id;
            }
            //判断是否存在
            var data = feeincomebll.GetList(ent.feeitem_id, operateId, ent.fee_year.ToInt(), ent.fee_month.ToInt(), type);
            if (data != null && data.Count() > 0)
            {
                Busines.FinanceManage.FeeitemBLL bll = new Busines.FinanceManage.FeeitemBLL();
                Entity.FinanceManage.FeeitemEntity fen_ent = bll.GetEntity(ent.feeitem_id);
                if (fen_ent.allowreply == false)
                {
                    return Error("该费用[ " + queryParam["feeitemname"] + " ]已经生成过，请设置新的日期！。");
                }
            }

            feeincomebll.SaveForm("", ent);

            return Success("操作成功。");
        }

        /// <summary>
        /// 费用导入
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SevaFeeFrom(string queryJson)
        {
            //try
            //{
            FeeincomeEntity ent = new FeeincomeEntity();
            int type = 0;
            string building_id = "";
            int selectdecimal = 2;
            string FeeManageEntryJson = "";

            #region 赋值

            var queryParam = queryJson.ToJObject();
            if (!queryParam["fee_year"].IsEmpty())
            {
                ent.fee_year = Convert.ToInt16(queryParam["fee_year"]);
            }
            if (!queryParam["fee_month"].IsEmpty())
            {
                ent.fee_month = Convert.ToInt16(queryParam["fee_month"]);
            }
            if (!queryParam["type"].IsEmpty())
            {
                type = queryParam["type"].ToInt();
            }
            if (!queryParam["feeitem_id"].IsEmpty())
            {
                ent.feeitem_id = queryParam["feeitem_id"].ToString();
            }
            if (!queryParam["pay_enddate"].IsEmpty())
            {
                ent.pay_enddate = queryParam["pay_enddate"].ToDate();
            }
            ent.notes = "批量导入";
            ent.property_id = Utils.GetCookie("property_id");
            ent.start_date = (ent.fee_year + "-" + ent.fee_month + "-01").ToDate();
            ent.end_date = (ent.fee_year + "-" + ent.fee_month + "-25").ToDate();
            ent.userid = Code.OperatorProvider.Provider.Current().UserName;
            ent.inputtime = DateTime.Now;
            ent.fee_already = 0;

            if (!queryParam["building_id"].IsEmpty())
            {
                building_id = queryParam["building_id"].ToString();
            }
            if (!queryParam["selectdecimal"].IsEmpty())
            {
                selectdecimal = queryParam["selectdecimal"].ToInt();
            }
            if (!queryParam["file"].IsEmpty())
            {
                FeeManageEntryJson = queryParam["file"].ToString();
            }

            #endregion

            #region 判断

            DateTime endtime = ent.pay_enddate.ToDate();
            if (ent.fee_year.ToInt() == endtime.Year)
            {
                if (endtime.Month < ent.fee_month)
                {
                    return Error("应收截止日期不能小于记帐月份！");
                }
                else
                {
                    if (endtime.Month - ent.fee_month.ToInt() > 1)
                    {
                        return Error("应收截止日期不能大于2个月！");
                    }
                }
            }
            else
            {
                if (((endtime.Year - ent.fee_year.ToInt() != 1) && ent.fee_month != 1) && (ent.fee_month != 12))
                {
                    return Error("应收截止日期不能大于2个月！");
                }
            }

            #endregion

            #region 判断文件格式

            if (string.IsNullOrEmpty(FeeManageEntryJson))
            {
                return Error("请选择上传费用导出EXCEL文件！");
            }

            DataTable dt = Util.Offices.ExcelHelper.ExcelImport(Utils.GetMapPath(FeeManageEntryJson));

            List<FeeincomeEntity> list = new List<FeeincomeEntity>();
            string ids = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Columns.Count < 6)
                {
                    return Error("请选择按照导出文档模版填写金额！");
                }

                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    //单元代码编号和金额不能为空
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()) && !string.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                    {
                        ids += dt.Rows[i][1] + ",";
                    }
                    else
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }
                if (dt == null || dt.Rows.Count == 0)
                {
                    return Error("请选择按照导出文档模版填写金额！");
                }
            }
            else
            {
                return Error("请选择上传有效的费用文件！");
            }

            #endregion

            #region 子表赋值

            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.Length - 1);
            }
            FeeincomeBLL feeincomebll = new FeeincomeBLL();

            var data = feeincomebll.GetList(ent.feeitem_id, ids, ent.fee_year.ToInt(), ent.fee_month.ToInt(), type);
            int Total = data.Count();

            Busines.FinanceManage.FeeitemBLL blls = new Busines.FinanceManage.FeeitemBLL();
            Entity.FinanceManage.FeeitemEntity fen_ent = blls.GetEntity(ent.feeitem_id);

            // mapper setting
            Mapper.Initialize(cfg => { cfg.CreateMap<FeeincomeEntity, FeeincomeEntity>(); });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FeeincomeEntity Fent = new FeeincomeEntity();
                bool bl = false;

                if (Total == 0)
                {
                    bl = true;
                }
                else
                {
                    if (data.Where(t => t.room_id == dt.Rows[i][1].ToString()).Count() == 0)
                    {
                        bl = true;
                    }
                    else
                    {
                        if (fen_ent.allowreply == true)
                        {
                            bl = true;
                        }
                    }
                }

                if (bl == true)
                {
                    //Fent = ent;
                    Fent = Mapper.Map<FeeincomeEntity>(ent);

                    if (type == 1)
                    {
                        //owner_id 第一列不能为空
                        if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                        {
                            Fent.building_id = building_id;
                            Fent.owner_id = dt.Rows[i][0].ToString();
                            Fent.room_id = dt.Rows[i][1].ToString();
                        }
                    }
                    else
                    {
                        Fent.rentcontract_id = dt.Rows[i][1].ToString();
                    }

                    Fent.fee_income = Math.Round(dt.Rows[i][5].ToDecimal(), selectdecimal);
                    list.Add(Fent);
                }
            }

            #endregion

            if (list.Count == 0)
            {
                return Error("该费用[ " + queryParam["feeitemname"] + " ]已经生成过，请设置新的日期！。");
            }
            else
            {
                feeincomebll.FeeManage(list);
            }

            return Success("操作成功。");
            //}
            //catch (Exception)
            //{
            //    return Error("操作失败，请检查导入模版是否正确。");
            //}
        }

        #endregion
    }
}