using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Linq;
using System.Web.Mvc;
using HuRongClub.Util.Extension;
using HuRongClub.Application.Code;
using System.Collections.Generic;
using Newtonsoft.Json;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:07
    /// 描 述：Owner_fee
    /// </summary>
    [HandlerOperateLog]
    public class Owner_feeController : MvcControllerBase
    {
        private Owner_feeBLL owner_feebll = new Owner_feeBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Owner_feeIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Owner_feeForm()
        {
            return View();
        }
        /// <summary>
        /// 表单页面--产业档案调用
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="room_id">房间编号</param>
        /// <param name="owner_name">业主名称</param>
        /// <param name="building_dim">房屋面积</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoomForm(string owner_id,string room_id,string owner_name,string building_dim)
        {
            ViewBag.room_id = room_id;
            ViewBag.owner_id = owner_id;
            ViewBag.owner_name = owner_name;
            ViewBag.building_dim = building_dim;
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
            var data = owner_feebll.GetPageList(pagination, queryJson);
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
            var data = owner_feebll.GetList(queryJson);
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
            var data = owner_feebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取费用列表--产业档案使用
        /// </summary>
        /// <param name="keyValue">room_id</param>
        /// <param name="owner_id">业主编号</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListByRoomJson(string keyValue, string owner_id)
        {
            var data = owner_feebll.GetList(owner_id, keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListsJson(Pagination pagination, string queryJson)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            IEnumerable<OwnerFeeIndexEntity> data = owner_feebll.GetList(pagination, queryJson, property_id);
            string json = JsonConvert.SerializeObject(data);
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
            owner_feebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="building_dim">服务面积</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue,string building_dim, Owner_feeEntity entity)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }

            if (keyValue.IndexOf(",") == -1)
            {
                #region 单个操作

                if (entity.fee_rule == "0")
                {
                    //按房屋面积
                    decimal dim = 0;
                    if (!string.IsNullOrEmpty(building_dim))
                    {
                        dim = building_dim.ToDecimal();
                    }
                    entity.fee_rule = "按房屋面积：" + entity.fee_money + "*" + building_dim;
                    entity.fee_money = entity.fee_money.ToDecimal() * dim;
                }
                else
                {
                    entity.fee_rule = "按固定金额：" + entity.fee_money;
                }
                string owner_feeid = owner_feebll.SaveForm(keyValue, property_id, entity);
                return Success("操作成功。", owner_feeid); 

                #endregion
            }
            else
            {
                #region 多个操作
                string[] keyValues = keyValue.Split(',');
                string[] building_dims = building_dim.Split(',');
                string strfee_rule = entity.fee_rule;
                decimal money = entity.fee_money.ToDecimal();
                for (int i = 0; i < keyValues.Length; i++)
                {
                    if (strfee_rule == "0")
                    {
                        //按房屋面积
                        decimal dim = 0;
                        if (!string.IsNullOrEmpty(building_dim))
                        {
                            dim = building_dims[i].ToDecimal();
                        }
                        entity.fee_rule = "按房屋面积：" + money + "*" + dim;
                        entity.fee_money = money * dim;
                    }
                    else
                    {
                        entity.fee_rule = "按固定金额：" + entity.fee_money;
                    }
                    string owner_feeid = owner_feebll.SaveForm(keyValues[i], property_id, entity);
                }

                return Success("操作成功。");
                #endregion
            }

            
        }

        /// <summary>
        /// 视图列表Json转换视图树形Json   OwnerFeeModelEntity
        /// </summary>
        /// <param name="moduleColumnJson">视图列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult ListToListTreeJson(string moduleColumnJson)
        {
            var data = from items in moduleColumnJson.ToList<OwnerFeeModelEntity>() orderby items.start_date descending select items;
            return Content(data.ToJson());
        }
        #endregion
    }
}
