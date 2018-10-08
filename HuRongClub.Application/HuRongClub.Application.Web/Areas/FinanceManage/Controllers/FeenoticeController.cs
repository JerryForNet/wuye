using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Application.Code;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using HuRongClub.Util.Extension;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.FinanceManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    [HandlerOperateLog]
    public class FeenoticeController : MvcControllerBase
    {
        private FeenoticeBLL feenoticebll = new FeenoticeBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeenoticeIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeenoticeForm()
        {
            return View();
        }
        /// <summary>
        /// 认领
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeenoticeClaim()
        {
            return View();
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeenoticeImport()
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
            var data = feenoticebll.GetList(pagination, queryJson);
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
            var data = feenoticebll.GetList(queryJson);
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
            var data = feenoticebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 账单编号不能重复
        /// </summary>
        /// <param name="accountcode">账单编号值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Existaccountcode(string accountcode, string keyValue)
        {
            bool IsOk = feenoticebll.Existaccountcode(accountcode, keyValue);
            return Content(IsOk.ToString());
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            feenoticebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, FeenoticeEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.CreatorId = OperatorProvider.Provider.Current().UserId;
                entity.CreatorName = OperatorProvider.Provider.Current().UserName != ""? OperatorProvider.Provider.Current().UserName: OperatorProvider.Provider.Current().Account;
                entity.CreateDate = DateTime.Now;
            }
            feenoticebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存表单---认领
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveClaimForm(string keyValue, FeenoticeEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return Error("操作错误！");
            }

            if (feenoticebll.ExistIsClaim(keyValue))
            {
                return Error("该进账已经认领，请选择其他进账认领！");
            }

            entity.checkuserid = OperatorProvider.Provider.Current().UserId;

            feenoticebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="attfile">文件路径</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ImportForm(string attfile)
        {
            if (!string.IsNullOrEmpty(attfile))
            {
                DataTable dt = Util.Offices.ExcelHelper.ExcelImport(Utils.GetMapPath(attfile));
                if (dt != null && dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Columns.Contains("账单编号") && dt.Columns.Contains("账单单位") && dt.Columns.Contains("账单日期") && dt.Columns.Contains("账单金额"))
                        {
                            List<FeenoticeEntity> list = new List<FeenoticeEntity>();
                            string CreatorName = OperatorProvider.Provider.Current().UserName != "" ? OperatorProvider.Provider.Current().UserName : OperatorProvider.Provider.Current().Account;
                            DateTime date = DateTime.Now;
                            bool Ismemo = false, Isaccounts = false, Ispurpose = false;

                            #region 判断
                            if (dt.Columns.Contains("账单备注"))
                            {
                                Ismemo = true;
                            }
                            if (dt.Columns.Contains("对方帐号"))
                            {
                                Isaccounts = true;
                            }
                            if (dt.Columns.Contains("用途"))
                            {
                                Ispurpose = true;
                            }
                            #endregion

                            #region 赋值

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (!dt.Rows[i]["账单编号"].IsEmpty() && !dt.Rows[i]["账单单位"].IsEmpty() && !dt.Rows[i]["账单日期"].IsEmpty() && !dt.Rows[i]["账单金额"].IsEmpty())
                                {
                                    FeenoticeEntity ent = new FeenoticeEntity();
                                    ent.CreatorId = OperatorProvider.Provider.Current().UserId;
                                    ent.CreatorName = CreatorName;
                                    ent.CreateDate = date;

                                    ent.accountcode = dt.Rows[i]["账单编号"].ToString();
                                    ent.accountcompany = dt.Rows[i]["账单单位"].ToString();
                                    ent.accountdate = dt.Rows[i]["账单日期"].ToDateOrNullToNow();
                                    ent.account = dt.Rows[i]["账单金额"].ToDecimal();
                                    if (Ismemo)
                                    {
                                        ent.memo = dt.Rows[i]["账单备注"].ToString();
                                    }
                                    if (Isaccounts)
                                    {
                                        ent.accounts = dt.Rows[i]["对方帐号"].ToString();
                                    }
                                    if (Ispurpose)
                                    {
                                        ent.purpose = dt.Rows[i]["用途"].ToString();
                                    }

                                    list.Add(ent);

                                }
                            }

                            #endregion

                            if (list != null && list.Count > 0)
                            {
                                feenoticebll.ImportForm(list);
                                return Success("操作成功。");
                            }
                            else
                            {
                                return Error("请按照模式填写进账数据上传！");
                            }
                        }
                        else
                        {
                            return Error("请按照模式填写进账数据上传！");
                        }
                    }
                    catch (Exception)
                    {
                        return Error("请按照模式填写进账数据上传！");
                    }
                }
                else
                {
                    return Error("请上传有效导入进账文件！");
                }
            }
            else
            {
                return Error("请选择上传进账导出EXCEL文件！");
            }
        }

        #endregion
    }
}
