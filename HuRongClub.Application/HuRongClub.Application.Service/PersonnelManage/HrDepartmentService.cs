using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 10:41
    /// �� ����������Ϣ
    /// </summary>
    public class HrDepartmentService : RepositoryFactory<HrDepartmentEntity>, HrDepartmentIService
    {
        #region ��ȡ����

        /// <summary>
        /// �б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDepartmentEntity> GetList()
        {
            var expression = LinqExtensions.True<HrDepartmentEntity>();
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<HrDepartmentEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "deptname":            //��������
                        expression = expression.And(t => t.deptname.Contains(keyword));
                        break;

                    case "manager":          //������
                        expression = expression.And(t => t.manager.Contains(keyword));
                        break;

                    case "phone":          //���ŵ绰
                        expression = expression.And(t => t.phone.Contains(keyword));
                        break;

                    case "managerphone":          //�����˵绰
                        expression = expression.And(t => t.managerphone.Contains(keyword));
                        break;

                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<HrDepartmentEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public HrDepartmentEntity GetEntity(int keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            BaseManage.DepartmentService dal = new BaseManage.DepartmentService();
            dal.RemoveForm(keyValue);

            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HrDepartmentEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);

                Entity.BaseManage.DepartmentEntity dent = new Entity.BaseManage.DepartmentEntity();
                dent.DepartmentId = keyValue;
                dent.OrganizeId = "207fa1a9-160c-4943-a89b-8fa4db0547ce";
                dent.ParentId = "0";
                dent.EnCode = keyValue;
                dent.FullName = entity.deptname;
                dent.OuterPhone = entity.managerphone;
                dent.Manager = entity.manager;
                BaseManage.DepartmentService dal = new BaseManage.DepartmentService();

                dal.SavesForm(keyValue, dent);
            }
            else
            {
                //entity.Create();
                //this.BaseRepository().Insert(entity);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into hr_department(");
                strSql.Append("deptname,manager,phone,managerphone)");
                strSql.Append(" values (");
                strSql.Append("@deptname,@manager,@phone,@managerphone)");
                strSql.Append(";select @@IDENTITY");

                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@deptname",entity.deptname),
                    DbParameters.CreateDbParameter("@manager",entity.manager),
                    DbParameters.CreateDbParameter("@phone",entity.phone),
                    DbParameters.CreateDbParameter("@managerphone",entity.managerphone)
                };

                object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
                if (obj != null)
                {
                    Entity.BaseManage.DepartmentEntity dent = new Entity.BaseManage.DepartmentEntity();
                    dent.DepartmentId = obj.ToString();
                    dent.OrganizeId = "207fa1a9-160c-4943-a89b-8fa4db0547ce";
                    dent.ParentId = "0";
                    dent.EnCode= obj.ToString();
                    dent.FullName = entity.deptname;
                    dent.OuterPhone = entity.managerphone;
                    dent.Manager = entity.manager;
                    BaseManage.DepartmentService dal = new BaseManage.DepartmentService();

                    dal.SavesForm("", dent);
                }


            }
        }

        #endregion �ύ����
    }
}