using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：物业租赁合同
    /// </summary>
    [HandlerOperateLog]
    public class RentcontractController : MvcControllerBase
    {
        private RentcontractBLL rentcontractbll = new RentcontractBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RentcontractIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RentcontractForm()
        {
            return View();
        }

        /// <summary>
        /// 表单页面--编辑
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 租赁合同费用生成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult CreateContractIncome()
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
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            var data = rentcontractbll.GetPageList(pagination, queryJson, property_id);
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
            var data = rentcontractbll.GetList(queryJson);
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
            var data = rentcontractbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        public ActionResult GetListBySel(int type = 1,int status = 0)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var data = rentcontractbll.GetListBySel(property_id, status);
            if (type == 1)
            {
                var treeList = new List<dynamic>();
                foreach (var item in data)
                {
                    treeList.Add(new
                    {
                        id = item.contractid,
                        text = item.customername,
                        value = item.contractid,
                        parentId = "0",
                        isexpand = true,
                        complete = true,
                        hasChildren = false,
                        shortname = item.shortname,
                        contractcode = item.contractcode,
                        unitroom = item.unitroom
                    });
                }

                return Content(JsonConvert.SerializeObject(treeList));
            }
            else
            {
                return Content(data.ToJson());
            }
        }

        /// <summary>
        /// 获取前五天即将到期合同
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetExpireList()
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var data = rentcontractbll.GetExpireList(property_id);
            return Content(data.ToJson());
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
            FeeincomeBLL bll = new FeeincomeBLL();
            string property_id = Utils.GetCookie("property_id");
            int count = bll.IsQianFei(property_id, keyValue, 2);
            if (count > 0)
            {
                return Error("该单元合同还有未收费用不能出户！");
            }
            else
            {
                rentcontractbll.RemoveForm(keyValue);
                return Success("删除成功。");
            }
        }

        /// <summary>
        /// 删除单元
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveDYForm(string keyValue, string room_id, string rentcell)
        {
            rentcontractbll.RemoveDYForm(keyValue, room_id, rentcell);
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
        public ActionResult SaveForm(string keyValue, RentcontractEntity entity)
        {
            entity.property_id = Utils.GetCookie("property_id");
            entity.inputuser = OperatorProvider.Provider.Current().Account;
            rentcontractbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateStatus(string keyValue, int Status)
        {
            rentcontractbll.UpdateStatus(keyValue, Status);
            return Success("操作成功。");
        }

        /// <summary>
        /// 租凭单元
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <param name="IsTrue">是否添加 1未 2 是</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateRentcell(string keyValue, string room_id, int IsTrue, string rentcell)
        {
            if (IsTrue == 0)
            {
                IsTrue = 1;
            }
            else
            {
                IsTrue = 2;
            }
            if (!rentcontractbll.IsRentcell(keyValue, room_id))
            {
                rentcontractbll.UpdateRentcell(keyValue, room_id, IsTrue, rentcell);
                return Success("操作成功。");
            }
            else
            {
                return Error("添加单元已出租，不能重复添加");
            }
        }

        /// <summary>
        /// 租聘合同费用生成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveContractIncomeForm(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return Error("传参异常");
            }
            bool res = rentcontractbll.SaveContractIncomeForm(param);
            if (res)
            {
                return Success("操作成功。");
            }
            else
            {
                return Error("操作失败。");
            }
        }

        #endregion
    }
}