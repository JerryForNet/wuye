using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using HuRongClub.Application.Code;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-07 17:17
    /// 描 述：Building
    /// </summary>
    [HandlerOperateLog]
    public class BuildingController : MvcControllerBase
    {
        private BuildingBLL buildingbll = new BuildingBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult BuildingIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BuildingForm()
        {
            return View();
        }

        #region 房间
        /// <summary>
        /// 房间列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RoomIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoomForm()
        {
            return View();
        }
        /// <summary>
        /// 房间详情页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoomDetail()
        {
            return View();
        }

        #endregion


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
            var data = buildingbll.GetPageList(pagination, queryJson, property_id);
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
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(int Type = 1)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var data = buildingbll.GetList(property_id);
            if (Type == 1)
            {
                var treeList = new List<TreeEntity>();
                foreach (BuildingEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = false;//data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                    tree.id = item.building_id;
                    tree.text = item.building_name;
                    tree.value = item.building_id;
                    tree.parentId = "0";
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }
                return Content(treeList.TreeToJson());
            }
            else
            {
                return Content(data.ToJson());
            }
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = buildingbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取房屋下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoomMedel(int dictid, int type = 1)
        {
            RoomBLL bll = new RoomBLL();
            var data = bll.GetListBySel(dictid);
            if (type == 2)
            {
                var treeList = new List<TreeEntity>();
                foreach (Entity.SysManage.DictitemEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = false;
                    tree.id = item.itemid;
                    tree.text = item.itemname;
                    tree.value = item.itemid;
                    tree.parentId = "0";
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }
                return Content(treeList.TreeToJson());
            }
            else
            {
                return Content(data.ToJson());
            }
        }

        /// <summary>
        /// 获取招聘单元信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetListRent(string keyValue)
        {
            var watch = CommonHelper.TimerStart();
            RoomBLL bll = new RoomBLL();
            var data = bll.GetListByRent(keyValue);
            var jsonData = new
            {
                rows = data,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        #region 房间
        /// <summary>
        /// 获取房间数树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataItemTreeJson(string keyValue)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            //string floor_list = buildingbll.Getfloor_list(keyValue, property_id);
            //if (!string.IsNullOrEmpty(floor_list))
            //{
            //    var treeList = new List<TreeEntity>();
            //    string[] floor_lists = floor_list.Split(',');
            //    for (int i = 0; i < floor_lists.Length; i++)
            //    {
            //        TreeEntity tree = new TreeEntity();
            //        bool hasChildren = false;
            //        tree.id = floor_lists[i];
            //        tree.text = floor_lists[i] + " 层";
            //        tree.value = floor_lists[i];
            //        tree.parentId = "0";
            //        tree.isexpand = true;
            //        tree.complete = true;
            //        tree.hasChildren = hasChildren;
            //        treeList.Add(tree);
            //    }

            //    return Content(treeList.TreeToJson());
            //}
            //else
            //{
            //    return Content("");
            //}

            BuildingEntity model = buildingbll.GetEntity(keyValue);
            if (model != null)
            {
                var treeList = new List<TreeEntity>();
                for (int i = 1; i <= model.floor_count; i++)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = false;
                    tree.id = i.ToString();
                    tree.text = i + " 层";
                    tree.value = i.ToString();
                    tree.parentId = "0";
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }
                return Content(treeList.TreeToJson());
            }
            else
            {
                return Content("");
            }
        }
        /// <summary>
        /// 获取房间帐号信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageListByRoomJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            RoomBLL bll = new RoomBLL();
            var data = bll.GetPageList(pagination, queryJson);
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetRoomFormJson(string keyValue)
        {
            RoomBLL bll = new RoomBLL();
            var data = bll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetInfoJson(string keyValue)
        {
            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            RoomBLL bll = new RoomBLL();
            var data = bll.GetInfo(keyValue, property_id);
            RoomModelEntity rm = new RoomModelEntity();
            foreach (RoomModelEntity item in data)
            {
                rm = item;
                break;
            }
            return ToJsonResult(rm);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="Type">1 全部 2空房间</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetRoomTreeJson(string keyValue, int Type = 1)
        {
            RoomBLL bll = new RoomBLL();
            var data = bll.GetTreeList(keyValue, Type);
            var treeList = new List<TreeEntity>();
            foreach (RoomEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;//data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.room_id;
                tree.text = item.room_name;
                tree.value = item.owner_id;
                tree.Attribute = "building_dim";
                tree.AttributeValue = string.Format("{0:0.##}", item.building_dim);
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetRoomEntityJson(string keyValue)
        {
            RoomBLL bll = new RoomBLL();
            var data = bll.GetEntitys(keyValue);
            return ToJsonResult(data);
        }
        #endregion


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
            buildingbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, BuildingEntity entity)
        {
            entity.floor_list.Replace("，", ",");
            if (entity.floor_count != entity.floor_list.Split(',').Length)
            {
                return Error("楼层数与楼层名称不符！");
            }
            else
            {
                if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
                {
                    entity.property_id = Utils.GetCookie("property_id");
                    buildingbll.SaveForm(keyValue, entity);
                    return Success("操作成功。");
                }
                else
                {
                    return Error("操作失败。");
                }
            }
        }

        /// <summary>
        /// 房间保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRoomForm(string keyValue, RoomEntity entity)
        {
            RoomBLL bll = new RoomBLL();
            entity.property_id = Utils.GetCookie("property_id");
            bll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 修改房间表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult EditRoomForm(RoomModelEntity entity)
        {
            RoomBLL bll = new RoomBLL();
            RoomEntity model = bll.GetEntity(entity.room_id);
            model.room_id = entity.room_id;
            model.floor_number = entity.floor_number;
            model.room_name = entity.room_name;
            model.repair_price = entity.repair_price;
            model.building_dim = entity.building_dim;
            model.room_dim = entity.room_dim;
            model.jf_date = entity.jf_date;

            //主表
            bll.SaveForm(model);

            //修改业主姓名
            if (!string.IsNullOrEmpty(entity.owner_id))
            {
                OwnerBLL bll_o = new OwnerBLL();
                bll_o.UpdateOwnerName(entity.owner_id, entity.owner_name);
            }

            return Success("操作成功。");
        }
        /// <summary>
        /// 视图列表Json转换视图树形Json   OwnerFeeModelEntity
        /// </summary>
        /// <param name="moduleColumnJson">视图列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult ListToListTreeJson(string moduleColumnJson)
        {
            var data = from items in moduleColumnJson.ToList<RoomModelEntity>() orderby items.room_id descending select items;
            return Content(data.ToJson());
        }
        #endregion
    }
}
