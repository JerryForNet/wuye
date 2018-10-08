using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;
using System;
using HuRongClub.Application.Code;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本 6.1    
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 10:57
    /// 描 述：Owner
    /// </summary>
    [HandlerOperateLog]
    public class OwnerController : MvcControllerBase
    {
        private OwnerBLL ownerbll = new OwnerBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OwnerIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OwnerForm()
        {
            return View();
        }
        /// <summary>
        /// 详情页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OwnerDetail()
        {
            return View();
        }
        /// <summary>
        /// 业主进户界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OwnerIn()
        {
            return View();
        }
        /// <summary>
        /// 业主出户界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OwnerOut()
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
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var data = ownerbll.GetInfoList(property_id, queryJson, pagination);
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
            var data = ownerbll.GetList(queryJson);
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
            var data = ownerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取单元房屋信息--产业档案使用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetInfoJson(string keyValue,int type=1)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var data = ownerbll.GetInfo(keyValue, property_id, type);
            OwnerModelEntity rm = new OwnerModelEntity();
            foreach (OwnerModelEntity item in data)
            {
                rm = item;
                break;
            }
            return ToJsonResult(rm);
        }
        /// <summary>
        /// 获取业主下拉
        /// </summary>
        /// <param name="building_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectJson(string building_id)
        {
            var data = ownerbll.GetListBySel(building_id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 导出单元房屋信息Excel
        /// </summary>
        /// <param name="building_id">归属楼栋</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUnitInfo(string building_id)
        {
            RoomBLL bll = new RoomBLL();

            int ret = bll.GetUnitInfo(Utils.GetCookie("property_id"), building_id);
            if (ret == 0)
            {
                return Error("未找到该楼栋下空房间！");
            }
            else
            {
                return Success("导出成功。");
            }
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
            ownerbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, OwnerEntity entity)
        {
            entity.sign_date = DateTime.Now;
            entity.sign_userid = OperatorProvider.Provider.Current().UserName;
            entity.property_id = Utils.GetCookie("property_id");

            string ret = ownerbll.SaveForm(keyValue, entity);
            return Success("操作成功。", ret);
        }
        /// <summary>
        /// 出户
        /// </summary>
        /// <param name="owner_id"></param>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveOutForm(string owner_id, string room_id)
        {
            if (!string.IsNullOrEmpty(owner_id) && !string.IsNullOrEmpty(room_id))
            {
                FeeincomeBLL bll = new FeeincomeBLL();
                int count = bll.IsQianFei(room_id, owner_id, 1);
                if (count > 0)
                {
                    return Error("该单元业主还有未收费用不能出户！");
                }
                else
                {
                    RoomBLL bll_r = new RoomBLL();
                    bll_r.SaveOutFrom(room_id, owner_id);
                    return Success("操作成功。");
                }
            }
            else
            {
                return Error("请选择出户单元！");
            }
            


        }
        /// <summary>
        /// 批量进户
        /// </summary>
        /// <param name="building_id">所属楼栋</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchFrom(string building_id, string file)
        {
            string ret = ownerbll.BatchFrom(Utils.GetCookie("property_id"), building_id, file);
            if (ret == "0")
            {
                return Success("操作成功。");
            }
            else
            {
                return Error(ret);
            }
        }
        #endregion
    } 
}
