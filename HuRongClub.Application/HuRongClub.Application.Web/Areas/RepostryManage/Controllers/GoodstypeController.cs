using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    [HandlerOperateLog]
    public class GoodstypeController : MvcControllerBase
    {
        private GoodstypeBLL goodstypebll = new GoodstypeBLL();
        private GoodstypeCache goodstypeCache = new GoodstypeCache();

        #region ��ͼ����

        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        #endregion ��ͼ����

        #region ��ȡ����

        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        /// <param name="keyword">�ؼ���</param>
        /// <returns>��������Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, int flayer)
        {
            var data = goodstypeCache.GetList().ToList();

            if (flayer == 0)
            {
                var d = data.Where(t => t.flayer == flayer).ToList<GoodstypeEntity>();
                return Content(d.ToJson());
            }
            else
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.ftypename.Contains(keyword), "ftypecode");
                }
                var treeList = new List<TreeEntity>();
                foreach (GoodstypeEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = data.Count(t => t.frootid == item.ftypecode) == 0 ? false : true;
                    tree.id = item.ftypecode;
                    tree.text = item.ftypename;
                    tree.value = item.ftypecode;
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    tree.parentId = item.frootid;
                    treeList.Add(tree);
                }

                return Content(treeList.TreeToJson());
            }
        }

        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns>���������б�Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string queryJson)
        {
            var data = goodstypebll.GetList(queryJson).ToList();
            if (data.Count <= 1)
            {
                return Content(data.ToJson());
            }
            else
            {
                var treeList = new List<TreeGridEntity>();
                foreach (GoodstypeEntity item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.frootid == item.ftypecode) == 0 ? false : true;
                    tree.id = item.ftypecode;
                    tree.hasChildren = hasChildren;
                    tree.parentId = item.fparentcode;
                    tree.expanded = true;
                    tree.entityJson = item.ToJson();
                    treeList.Add(tree);
                }
                return Content(treeList.TreeJson());
            }
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = goodstypebll.GetPageList(pagination, queryJson);
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string fparentcode)
        {
            var data = goodstypebll.GetListJson(fparentcode);
            var treeList = new List<TreeEntity>();
            foreach (GoodstypeEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;//data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.ftypecode;
                tree.text = item.ftypename;
                tree.value = item.ftypecode;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = goodstypebll.GetEntity(keyValue);
            return Content(data.ToJson());
        }

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            goodstypebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, GoodstypeEntity entity)
        {
            if (entity.frootid != "" || entity.frootid != null)  //������Ϊ��
            {
                entity.ftypecode = entity.frootid + entity.ftypecode; //����µ�code
            }
            if (entity.flayer == 0 && entity.fparentcode == "")
            {
                entity.fparentcode = "0";
            }
            else
            {
                entity.fparentcode = entity.frootid;
            }
            goodstypebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        #endregion �ύ����
    }
}