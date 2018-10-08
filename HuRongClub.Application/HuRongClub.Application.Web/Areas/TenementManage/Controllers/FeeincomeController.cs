using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：业主档案
    /// </summary>
    [HandlerOperateLog]
    public class FeeincomeController : MvcControllerBase
    {
        private FeeincomeBLL feeincomebll = new FeeincomeBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeeincomeIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeeincomeForm()
        {
            return View();
        }

        /// <summary>
        /// 缴费确定界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FixedForm()
        {
            return View();
        }

        /// <summary>
        /// 打印界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintFrom()
        {
            ViewBag.UserName = Code.OperatorProvider.Provider.Current().UserName;

            DateTime time = DateTime.Now;
            ViewBag.year = time.Year.ToString();

            ViewBag.month = (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString());
            ViewBag.day = (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString());

            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            OptionBLL optionbll = new OptionBLL();
            var data = optionbll.GetEntity(property_id);
            if (data != null)
            {
                ViewBag.option_exchange = string.Format("{0:N2}", data.option_exchange);
            }
            return View();
        }

        /// <summary>
        /// 实收查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ActualIndex()
        {
            return View();
        }

        /// <summary>
        /// 应收
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReceivableIndex()
        {
            return View();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeeincomeDel()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = feeincomebll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = feeincomebll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = feeincomebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="owner_id">业主编号</param>
        /// <param name="type">1已缴费 2欠缴费</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListsJson(Pagination pagination, string owner_id, int type)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            var data = feeincomebll.GetPageList(pagination, owner_id, property_id, type);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取列表--不分页
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="type">1已缴费 2欠缴费 3单元已缴费 4单元欠缴费 5减免费用</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetListsJson(Pagination pagination, string owner_id, int type)
        {
            if (string.IsNullOrEmpty(owner_id) && type == 4)
            {
                return null;
            }
            else
            {
                string property_id = "";
                if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
                {
                    property_id = Utils.GetCookie("property_id");
                }
                if (type == 5 || type == 6)
                {
                    if (!string.IsNullOrEmpty(owner_id))
                    {
                        if (owner_id.IndexOf(',') == -1)
                        {
                            owner_id = "'" + owner_id + "'";
                        }
                        else
                        {
                            string[] owner_ids = owner_id.Split(',');
                            string str = "";
                            foreach (string item in owner_ids)
                            {
                                str += "'" + item + "',";
                            }
                            owner_id = str.Substring(0, str.Length - 1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                if (string.IsNullOrEmpty(owner_id))
                {
                    owner_id = " ";
                }
                if (type == 5 || type == 6)
                {
                    var data = feeincomebll.GetPageList(null, owner_id, property_id, type);//.OrderByDescending(t => t.feeitem_name).ThenBy(t => t.fee_years).ThenBy(t => t.fee_month);
                    return ToJsonResult(data);
                }
                else
                {
                    if (pagination.sidx == null)
                    {
                        pagination.sidx = "fee_month,feeitem_name";
                    }

                    var data = feeincomebll.GetPageList(pagination, owner_id, property_id, type);
                    return ToJsonResult(data);
                }
            }
        }

        /// <summary>
        /// 获取年份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetYear()
        {
            var treeList = new List<TreeEntity>();
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year; i++)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = i.ToString();
                tree.text = i.ToString();
                tree.value = i.ToString();
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            for (int j = DateTime.Now.Year; j < (DateTime.Now.Year + 5); j++)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = j.ToString();
                tree.text = j.ToString();
                tree.value = j.ToString();
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 获取月份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMouth()
        {
            var treeList = new List<TreeEntity>();
            for (int i = 1; i < 13; i++)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = i.ToString();
                tree.text = i.ToString();
                tree.value = i.ToString();
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 获取租户缴费情况
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListRentJson(Pagination pagination, string contractid)
        {
            if (string.IsNullOrEmpty(contractid))
            {
                return null;
                //contractid = "00";
            }
            var data = feeincomebll.GetPageLists(pagination, contractid);

            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实收查询列表数据
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="Mode">1实收 2 应收</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetActualListJson(Pagination pagination, string queryJson, int Mode)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            var data = feeincomebll.GetActualList(pagination, property_id, queryJson, Mode);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            FeechangelogBLL bll = new FeechangelogBLL();
            List<FeechangelogEntity> list = new List<FeechangelogEntity>();
            if (keyValue.IndexOf(',') == -1)
            {
                #region 单个

                FeeincomeEntity model = feeincomebll.GetEntity(keyValue);
                FeechangelogEntity mod_log = new FeechangelogEntity();
                mod_log.itemid = property_id + bll.GetMaxID(8);
                mod_log.property_id = property_id;
                mod_log.room_id = model.room_id;
                mod_log.contract_id = model.rentcontract_id;
                mod_log.owner_id = model.owner_id;
                mod_log.source_money = model.fee_income.ToDouble();
                mod_log.new_money = 0;
                mod_log.feeitem_id = model.feeitem_id;
                mod_log.operatetime = DateTime.Now;
                mod_log.operatername = Code.OperatorProvider.Provider.Current().Account;
                mod_log.income_date = (model.fee_year + "-" + model.fee_month + "-01").ToDate();
                mod_log.memo = "删除";

                list.Add(mod_log);

                #endregion
            }
            else
            {
                #region 多个

                string[] keyValues = keyValue.Split(',');
                string str = "";
                foreach (string item in keyValues)
                {
                    str += "'" + item + "',";
                }
                if (str != "")
                {
                    str = str.Substring(0, str.Length - 1);
                }
                int maxid = bll.GetMaxID(0).ToInt();
                IEnumerable<FeeincomeEntity> ie = feeincomebll.GetLists(str);
                if (ie.Count() > 0)
                {
                    foreach (FeeincomeEntity item in ie)
                    {
                        //新增日志
                        FeechangelogEntity mod_log = new FeechangelogEntity();
                        mod_log.itemid = property_id + Utils.SupplementZero(maxid.ToString(), 8);
                        mod_log.property_id = property_id;
                        mod_log.room_id = item.room_id;
                        mod_log.contract_id = item.rentcontract_id;
                        mod_log.owner_id = item.owner_id;
                        mod_log.source_money = item.fee_income.ToDouble();
                        mod_log.new_money = 0;
                        mod_log.feeitem_id = item.feeitem_id;
                        mod_log.operatetime = DateTime.Now;
                        mod_log.operatername = Code.OperatorProvider.Provider.Current().UserName;
                        mod_log.income_date = (item.fee_year + "-" + item.fee_month + "-01").ToDate();
                        mod_log.memo = "删除";

                        list.Add(mod_log);

                        maxid++;
                    }
                }

                #endregion
            }

            feeincomebll.RemoveForm(keyValue, list);
            return Success("删除成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="FeeincomeEntryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemovesForm(string keyValue, string FeeincomeEntryJson)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            FeechangelogBLL bll = new FeechangelogBLL();
            List<FeechangelogEntity> list = new List<FeechangelogEntity>();

            var FeeincomeEntryList = FeeincomeEntryJson.ToList<FeeincomeAdjustEntity>();
            int maxid = bll.GetMaxID(0).ToInt();

            foreach (FeeincomeAdjustEntity item in FeeincomeEntryList)
            {
                FeechangelogEntity mod_log = new FeechangelogEntity();

                mod_log.itemid = property_id + Utils.SupplementZero(maxid.ToString(), 8);
                mod_log.property_id = property_id;
                mod_log.room_id = item.room_id;
                mod_log.contract_id = item.rentcontract_id;
                mod_log.owner_id = item.owner_id;
                mod_log.source_money = item.fee_income.ToDouble();
                mod_log.new_money = -1;
                mod_log.feeitem_id = item.feeitem_id;
                mod_log.operatetime = DateTime.Now;
                mod_log.operatername = Code.OperatorProvider.Provider.Current().UserName;
                mod_log.income_date = (item.fee_year + "-" + item.fee_month + "-01").ToDate();
                mod_log.memo = item.memo;

                list.Add(mod_log);

                maxid++;
            }

            feeincomebll.RemoveForm(keyValue, list);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, FeeincomeEntity entity)
        {
            feeincomebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 减免
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="FeeincomeEntryJson">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavesForm(string keyValue, string FeeincomeEntryJson)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var FeeincomeEntryList = FeeincomeEntryJson.ToList<FeeincomeAdjustEntity>();

            List<FeeincomeEntity> list_f = new List<FeeincomeEntity>();
            List<FeechangelogEntity> list_fl = new List<FeechangelogEntity>();
            List<FeeincomeCutEntity> list_fc = new List<FeeincomeCutEntity>();
            FeechangelogBLL bll = new FeechangelogBLL();
            int maxid = bll.GetMaxID(0).ToInt();

            foreach (FeeincomeAdjustEntity item in FeeincomeEntryList)
            {
                if (item.price < 0) item.price = 0;

                #region 费用应收表

                FeeincomeEntity ent_f = new FeeincomeEntity();
                ent_f.income_id = item.income_id;
                ent_f.fee_income = item.price;

                list_f.Add(ent_f);

                #endregion

                #region 费用调整日志

                //新增日志
                FeechangelogEntity mod_log = new FeechangelogEntity();
                mod_log.itemid = property_id + Utils.SupplementZero(maxid.ToString(), 8);
                mod_log.property_id = property_id;
                mod_log.room_id = item.room_id;
                mod_log.contract_id = item.rentcontract_id;
                mod_log.owner_id = item.owner_id;
                mod_log.source_money = item.fee_income.ToDouble();
                mod_log.new_money = item.price.ToDouble();
                mod_log.feeitem_id = item.feeitem_id;
                mod_log.operatetime = DateTime.Now;
                mod_log.operatername = Code.OperatorProvider.Provider.Current().UserName;
                mod_log.income_date = (item.fee_year + "-" + item.fee_month + "-01").ToDate();
                mod_log.memo = item.memo;

                list_fl.Add(mod_log);

                #endregion

                #region 费用减免记录

                FeeincomeCutEntity mod_fc = new FeeincomeCutEntity();
                mod_fc.property_id = property_id;
                mod_fc.room_id = item.room_id;
                mod_fc.owner_id = item.owner_id;
                mod_fc.contract_id = item.rentcontract_id;
                mod_fc.feeitem_id = item.feeitem_id;
                mod_fc.fee_year = item.fee_year;
                mod_fc.fee_month = item.fee_month;
                mod_fc.fee_cutmoney = (item.fee_income - item.price).ToDouble();
                mod_fc.inputtime = DateTime.Now;
                mod_fc.inputempid = Code.OperatorProvider.Provider.Current().UserName;

                list_fc.Add(mod_fc);

                #endregion

                maxid++;
            }

            feeincomebll.SavesForm(list_f, list_fl, list_fc);
            return Success("操作成功。");
        }

        /// <summary>
        /// 生成费用
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <param name="year">年</param>
        /// <param name="mouth">月</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult Generate(string roomid, string year, string mouth)
        {
            if (!String.IsNullOrEmpty(roomid))
            {
                DateTime date = (year + "-" + mouth + "-01").ToDate();
                feeincomebll.Generate(roomid, date, 1);

                return Success("操作成功。");
            }
            else
            {
                return Error("生成失败，请选择单元！");
            }
        }

        /// <summary>
        /// 生成费用--租户
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <param name="year">年</param>
        /// <param name="mouth">月</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult GenerateRent(string roomid, string year, string mouth)
        {
            if (!String.IsNullOrEmpty(roomid))
            {
                DateTime date = (year + "-" + mouth + "-01").ToDate();
                feeincomebll.Generate(roomid, date, 2);

                return Success("操作成功。");
            }
            else
            {
                return Error("生成失败，请选择单元！");
            }
        }

        /// <summary>
        /// 缴费确定
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="ticket_id">发票编号</param>
        /// <param name="FeeincomeEntryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult FixedCost(string keyValue, string ticket_id, string FeeincomeEntryJson)
        {
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

            #region 费用实收

            int maxid = new FeereceiveBLL().GetMaxID(0).ToInt();

            FeeincomeAdjustEntity entity = FeeincomeEntryList[0];

            FeereceiveEntity ent_fe = new FeereceiveEntity();
            ent_fe.receive_id = property_id + Utils.SupplementZero(maxid.ToString(), 8);
            ent_fe.property_id = property_id;
            ent_fe.receive_date = entity.receive_date;
            ent_fe.ticket_id = ticket_id;
            ent_fe.owner_id = entity.owner_id;
            ent_fe.rentcontract_id = entity.rentcontract_id;
            ent_fe.fee_money = entity.fee_income;
            ent_fe.userid = Code.OperatorProvider.Provider.Current().UserName;
            ent_fe.inputtime = DateTime.Now;
            ent_fe.room_id = entity.room_id;
            if (entity.isprint == "1")
            {
                ent_fe.isprint = entity.isprint;
                ent_fe.printname = entity.printname;
            }

            list_fe.Add(ent_fe);

            #endregion

            int maxid_f = new FeecheckBLL().GetMaxID(0).ToInt();
            for (int i = 0; i < FeeincomeEntryList.Count; i++)
            {
                FeeincomeAdjustEntity item = FeeincomeEntryList[i];

                #region 费用应收

                FeeincomeEntity mod_f = new FeeincomeEntity();
                mod_f.fee_already = item.fee_already;
                mod_f.fee_date = item.receive_date;
                mod_f.userid = Code.OperatorProvider.Provider.Current().UserName;
                mod_f.inputtime = DateTime.Now;
                mod_f.income_id = item.income_id;

                list_f.Add(mod_f);

                #endregion

                #region 收费核销

                FeecheckEntity ent_fk = new FeecheckEntity();
                ent_fk.check_id = property_id + Utils.SupplementZero(maxid_f.ToString(), 8);
                ent_fk.receive_id = ent_fe.receive_id;
                ent_fk.income_id = item.income_id;
                ent_fk.check_money = item.fee_income;

                list_fK.Add(ent_fk);

                maxid_f++;

                #endregion

                // 归总金额
                if (i > 0)
                {
                    ent_fe.fee_money += item.fee_income;
                }
            }

            #region 发票领用

            end_ft.ticket_id = ticket_id;
            end_ft.ticket_status = 1;

            #endregion

            feeincomebll.FixedCost(list_f, end_ft, list_fe, list_fK);

            return Success("操作成功。");
        }

        #endregion

        #region 数据导出

        /// <summary>
        /// 导出列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportList(string queryJson, int mode)
        {

            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }

            feeincomebll.GetExportList(property_id, queryJson, mode);
            return Success("导出成功。");
        }

        #endregion 数据导出
    }
}