using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    public class EmployinfoService : RepositoryFactory<EmployinfoEntity>, EmployinfoIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EmployinfoListEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" SELECT * FROM dbo.hr_employinfo where 1=1 ");

            strSql.Append(@"select empid,empname,empcode,sex,birthday,hiredate,firedate,address,postcode,homephone,mobilephone,email,identityno=identityno+'~',indate,politicsid,specialty,degreeid,dutyid,techclassid,jobid,careerid,careerdegree,contractfromid,contracttypeid,status,deptid,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.politicsid) as zzmm,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.degreeid) as xl,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.dutyid) as zc,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.techclassid) as jsdj,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.jobid) as gw,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.careerid) as zw,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.careerdegree) as zj,
                            (select top 1 itemname from sys_dictitem where itemid=hr_employinfo.contractfromid) as ygly,
                            (select top 1 deptname from hr_department where deptid=hr_employinfo.deptid) as deptname,
                            fireoutdate
                            from hr_employinfo where 1=1  ");


            var parameter = new List<DbParameter>();

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "empname"://员工姓名
                        strSql.Append(" AND empname LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", "%"+ keyword + "%"));
                        break;

                    case "empcode"://员工工号
                        strSql.Append(" AND empcode LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", "%" + keyword + "%"));
                        break;
                    default:
                        break;
                }
            }
            //部门
            if (!queryParam["deptid"].IsEmpty())
            {
                int deptid = queryParam["deptid"].ToInt();
                strSql.Append(" AND deptid=@deptid ");
                parameter.Add(DbParameters.CreateDbParameter("@deptid", deptid));
            }
            //员工状态
            if (!queryParam["status"].IsEmpty())
            {
                int status = queryParam["status"].ToInt();
                if (status != -1)
                {
                    strSql.Append(" AND status=@status ");
                    parameter.Add(DbParameters.CreateDbParameter("@status", status));
                }
            }
            else
            {
                strSql.Append(" AND status=0 ");
            }
            //性别
            if (!queryParam["sex"].IsEmpty())
            {
                string sex = queryParam["sex"].ToString();
                strSql.Append(" AND sex=@sex ");
                parameter.Add(DbParameters.CreateDbParameter("@sex", sex));
            }
            //用工性质
            if (!queryParam["contracttypeid"].IsEmpty())
            {
                int contracttypeid = queryParam["contracttypeid"].ToInt();
                strSql.Append(" AND contracttypeid=@contracttypeid ");
                parameter.Add(DbParameters.CreateDbParameter("@contracttypeid", contracttypeid));
            }
            //岗位
            if (!queryParam["jobid"].IsEmpty())
            {
                Array list = queryParam["jobid"].ToArray();
                string jobid ="";
                foreach (var item in list)
                {
                    jobid += item.ToInt() + ",";
                }
                if (!string.IsNullOrEmpty(jobid))
                {
                    jobid = jobid.Substring(0, (jobid.Length - 1));
                }
                strSql.AppendFormat(" AND jobid in ({0}) ", jobid);
                //parameter.Add(DbParameters.CreateDbParameter("@jobid", jobid));
            }
            //职级
            if (!queryParam["careerdegree"].IsEmpty())
            {
                int careerdegree = queryParam["careerdegree"].ToInt();
                strSql.Append(" AND careerdegree=@careerdegree ");
                parameter.Add(DbParameters.CreateDbParameter("@careerdegree", careerdegree));
            }
            //职务
            if (!queryParam["careerid"].IsEmpty())
            {
                int careerid = queryParam["careerid"].ToInt();
                strSql.Append(" AND careerid=@careerid ");
                parameter.Add(DbParameters.CreateDbParameter("@careerid", careerid));
            }
            //职称 
            if (!queryParam["dutyid"].IsEmpty())
            {
                int dutyid = queryParam["dutyid"].ToInt();
                strSql.Append(" AND dutyid=@dutyid ");
                parameter.Add(DbParameters.CreateDbParameter("@dutyid", dutyid));
            }
            //学历 
            if (!queryParam["degreeid"].IsEmpty())
            {
                int degreeid = queryParam["degreeid"].ToInt();
                strSql.Append(" AND degreeid=@degreeid ");
                parameter.Add(DbParameters.CreateDbParameter("@degreeid", degreeid));
            }
            //政治面貌 
            if (!queryParam["politicsid"].IsEmpty())
            {
                int politicsid = queryParam["politicsid"].ToInt();
                strSql.Append(" AND politicsid=@politicsid ");
                parameter.Add(DbParameters.CreateDbParameter("@politicsid", politicsid));
            }
            //婚姻状况  
            if (!queryParam["ifmarry"].IsEmpty())
            {
                int ifmarry = queryParam["ifmarry"].ToInt();
                strSql.Append(" AND ifmarry=@ifmarry ");
                parameter.Add(DbParameters.CreateDbParameter("@ifmarry", ifmarry));
            }
            //是否无固定期限：
            if (!queryParam["contractnotime"].IsEmpty())
            {
                int contractnotime = queryParam["contractnotime"].ToInt();
                strSql.Append(" AND contractnotime=@contractnotime ");
                parameter.Add(DbParameters.CreateDbParameter("@contractnotime", contractnotime));
            }

            //进公司日期
            if (!queryParam["StartDate"].IsEmpty()&& !queryParam["EndDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                string EndDate = queryParam["EndDate"].ToString();

                strSql.Append(" AND  CONVERT(VARCHAR(10),indate,120)>=@StartDate AND  CONVERT(VARCHAR(10),indate,120)<=@EndDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
                
            }
            //合同到期
            if (!queryParam["hiredate"].IsEmpty() && !queryParam["firedate"].IsEmpty())
            {
                string hiredate = queryParam["hiredate"].ToString();
                string firedate = queryParam["firedate"].ToString();

                strSql.Append(" AND  CONVERT(VARCHAR(10),firedate,120)>=@hiredate AND  CONVERT(VARCHAR(10),firedate,120)<=@firedate ");
                parameter.Add(DbParameters.CreateDbParameter("@hiredate", hiredate));
                parameter.Add(DbParameters.CreateDbParameter("@firedate", firedate));
            }
            //离职时间
            if (!queryParam["outStart"].IsEmpty() && !queryParam["outEnd"].IsEmpty())
            {
                string outStart = queryParam["outStart"].ToString();
                string outEnd = queryParam["outEnd"].ToString();

                strSql.Append(" AND  CONVERT(VARCHAR(10),fireoutdate,120)>=@outStart AND  CONVERT(VARCHAR(10),fireoutdate,120)<=@outEnd ");
                parameter.Add(DbParameters.CreateDbParameter("@outStart", outStart));
                parameter.Add(DbParameters.CreateDbParameter("@outEnd", outEnd));
            }
            //年龄
            if (!queryParam["sage"].IsEmpty() && !queryParam["eage"].IsEmpty())
            {
                int sage= queryParam["sage"].ToInt();
                int eage = queryParam["eage"].ToInt();                

                strSql.Append(" AND  datediff(year,birthday,getdate())>="+ sage + " AND  datediff(year,birthday,getdate())<="+ eage);          
            }
            //职级变动日期
            if (!queryParam["scdate"].IsEmpty() && !queryParam["ecdate"].IsEmpty())
            {
                string scdate = queryParam["scdate"].ToString();
                string ecdate = queryParam["ecdate"].ToString();

                strSql.Append(" AND empid IN (select distinct(empid) from hr_employexchange where CONVERT(VARCHAR(10),sdate,120)>=@scdate AND CONVERT(VARCHAR(10),sdate,120)<=@ecdate )");

                parameter.Add(DbParameters.CreateDbParameter("@scdate", scdate));
                parameter.Add(DbParameters.CreateDbParameter("@ecdate", ecdate));
            }
            //用工来源 
            if (!queryParam["contractfromid"].IsEmpty())
            {
                int contractfromid = queryParam["contractfromid"].ToInt();
                strSql.Append(" AND contractfromid=@contractfromid ");
                parameter.Add(DbParameters.CreateDbParameter("@contractfromid", contractfromid));
            }

            RepositoryFactory<EmployinfoListEntity> repository = new RepositoryFactory<EmployinfoListEntity>();

            if (pagination == null)
            {
                return repository.BaseRepository().FindList(strSql.ToString());
            }
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 员工列表(ALL)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetAllList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"  SELECT e.*,d.deptname FROM dbo.hr_employinfo e LEFT JOIN dbo.hr_department d ON e.deptid=d.deptid ORDER BY e.empid ");
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 根据IDs 查询员工信息集合
        /// </summary>
        /// <param name="employinfoIds"></param>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetAllList(List<string> employinfoIds)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"  SELECT e.*,d.deptname FROM dbo.hr_employinfo e LEFT JOIN dbo.hr_department d ON e.deptid=d.deptid where 1=1 ");

            if (employinfoIds != null && employinfoIds.Count > 0)
            {
                string ids = "";
                foreach (string id in employinfoIds)
                {
                    ids += id + ",";
                }
                if (ids.Length > 0) ids = ids.Substring(0, ids.Length - 1);
                strSql.AppendFormat(" and empid in ({0})", ids);
            }

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EmployinfoEntity> GetList()
        {
            var expression = LinqExtensions.True<EmployinfoEntity>();
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmployinfoEntity GetEntity(int keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取信息--导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetListInfo(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"
SELECT  emp.empid ,
        emp.empcode ,
        emp.empname ,
        emp.sex ,
        dep.deptname ,
        CONVERT(VARCHAR(10), emp.indate, 120) AS indate ,
        CONVERT(VARCHAR(10), emp.hiredate, 120) AS hiredate ,
        CONVERT(VARCHAR(10), emp.firedate, 120) AS firedate ,
        emp.address ,
        emp.postcode ,
        emp.homephone ,
        emp.mobilephone ,
        emp.identityno ,
        emp.contractfromid ,
        CONVERT(VARCHAR(10), emp.birthday, 120) AS birthday ,
        dic_p.itemname AS p_name ,
        dic_c.itemname AS c_name ,
        dic_d.itemname AS d_name ,
        dic_du.itemname AS du_name ,
        dic_t.itemname AS t_name ,
        dic_j.itemname AS j_name ,
        dic_ca.itemname AS ca_name ,
        dic_co.itemname AS co_name ,
        ( CASE WHEN emp.contracttypeid = 0 THEN '合同工'
               ELSE '劳务工'
          END ) AS contracttype
FROM    dbo.hr_employinfo emp
        LEFT JOIN dbo.hr_department dep  WITH (NOLOCK)  ON dep.deptid = emp.deptid
        LEFT JOIN dbo.sys_dictitem dic_p  WITH (NOLOCK)  ON dic_p.itemid = emp.politicsid
        LEFT JOIN dbo.sys_dictitem dic_c  WITH (NOLOCK)  ON dic_c.itemid = emp.careerid
        LEFT JOIN dbo.sys_dictitem dic_d  WITH (NOLOCK)  ON dic_d.itemid = emp.degreeid
        LEFT JOIN dbo.sys_dictitem dic_du  WITH (NOLOCK)  ON dic_du.itemid = emp.dutyid
        LEFT JOIN dbo.sys_dictitem dic_t  WITH (NOLOCK)  ON dic_t.itemid = emp.techclassid
        LEFT JOIN dbo.sys_dictitem dic_j  WITH (NOLOCK)  ON dic_j.itemid = emp.jobid
        LEFT JOIN dbo.sys_dictitem dic_ca  WITH (NOLOCK)  ON dic_ca.itemid = emp.careerdegree
        LEFT JOIN dbo.sys_dictitem dic_co  WITH (NOLOCK)  ON dic_co.itemid = emp.contractfromid
");

            strSql.Append(" WHERE 1=1 ");

            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "empname"://员工姓名
                        strSql.AppendLine(" AND emp.empname LIKE @empname ");
                        parameter.Add(DbParameters.CreateDbParameter("@empname", "%"+ keyword + "%"));
                        break;
                    case "empcode"://员工工号
                        strSql.AppendLine(" AND emp.empcode LIKE @empcode ");
                        parameter.Add(DbParameters.CreateDbParameter("@empcode", "%" + keyword + "%"));
                        break;
                    default:
                        break;
                }
            }
            //部门
            if (!queryParam["deptid"].IsEmpty())
            {
                int deptid = queryParam["deptid"].ToInt();
                strSql.AppendLine(" AND emp.deptid=@deptid ");
                parameter.Add(DbParameters.CreateDbParameter("@deptid", deptid));                
            }
            //员工状态
            if (!queryParam["status"].IsEmpty())
            {
                int status = queryParam["status"].ToInt();
                strSql.AppendLine(" AND emp.status=@status ");
                parameter.Add(DbParameters.CreateDbParameter("@status", status));
            }
            //性别
            if (!queryParam["sex"].IsEmpty())
            {
                string sex = queryParam["sex"].ToString();
                strSql.AppendLine(" AND emp.sex=@sex ");
                parameter.Add(DbParameters.CreateDbParameter("@sex", sex));
            }
            //用工性质
            if (!queryParam["contracttypeid"].IsEmpty())
            {
                int contracttypeid = queryParam["contracttypeid"].ToInt();
                strSql.AppendLine(" AND emp.contracttypeid=@contracttypeid ");
                parameter.Add(DbParameters.CreateDbParameter("@contracttypeid", contracttypeid));
            }
            //岗位
            if (!queryParam["jobid"].IsEmpty())
            {
                //int jobid = queryParam["jobid"].ToInt();
                //strSql.AppendLine(" AND emp.jobid=@jobid ");
                //parameter.Add(DbParameters.CreateDbParameter("@jobid", jobid));
                Array list = queryParam["jobid"].ToArray();
                string jobid = "";
                foreach (var item in list)
                {
                    jobid += item.ToInt() + ",";
                }
                if (!string.IsNullOrEmpty(jobid))
                {
                    jobid = jobid.Substring(0, (jobid.Length - 1));
                }
                strSql.AppendFormat(" AND jobid in ({0}) ", jobid);
            }
            //职级
            if (!queryParam["careerdegree"].IsEmpty())
            {
                int careerdegree = queryParam["careerdegree"].ToInt();
                strSql.AppendLine(" AND emp.careerdegree=@careerdegree ");
                parameter.Add(DbParameters.CreateDbParameter("@careerdegree", careerdegree));
            }
            //职务
            if (!queryParam["careerid"].IsEmpty())
            {
                int careerid = queryParam["careerid"].ToInt();
                strSql.AppendLine(" AND emp.careerid=@careerid ");
                parameter.Add(DbParameters.CreateDbParameter("@careerid", careerid));
            }

            // 职称
            if (!queryParam["dutyid"].IsEmpty())
            {
                int dutyid = queryParam["dutyid"].ToInt();
                strSql.Append(" AND dutyid=@dutyid ");
                parameter.Add(DbParameters.CreateDbParameter("@dutyid", dutyid));
            }

            // 学历
            if (!queryParam["degreeid"].IsEmpty())
            {
                int degreeid = queryParam["degreeid"].ToInt();
                strSql.Append(" AND degreeid=@degreeid ");
                parameter.Add(DbParameters.CreateDbParameter("@degreeid", degreeid));
            }

            // 政治面貌
            if (!queryParam["politicsid"].IsEmpty())
            {
                int politicsid = queryParam["politicsid"].ToInt();
                strSql.Append(" AND politicsid=@politicsid ");
                parameter.Add(DbParameters.CreateDbParameter("@politicsid", politicsid));
            }

            // 婚姻状况
            if (!queryParam["ifmarry"].IsEmpty())
            {
                int ifmarry = queryParam["ifmarry"].ToInt();
                strSql.Append(" AND ifmarry=@ifmarry ");
                parameter.Add(DbParameters.CreateDbParameter("@ifmarry", ifmarry));
            }

            // 是否无固定期限
            if (!queryParam["contractnotime"].IsEmpty())
            {
                int contractnotime = queryParam["contractnotime"].ToInt();
                strSql.Append(" AND contractnotime=@contractnotime ");
                parameter.Add(DbParameters.CreateDbParameter("@contractnotime", contractnotime));
            }

            // 年龄
            if (!queryParam["sage"].IsEmpty() && !queryParam["eage"].IsEmpty())
            {
                int sage = queryParam["sage"].ToInt();
                int eage = queryParam["eage"].ToInt();

                strSql.Append(" AND  datediff(year,birthday,getdate())>=" + sage + " AND  datediff(year,birthday,getdate())<=" + eage);
            }

            //进公司日期
            if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
            {
                DateTime StartDate = queryParam["StartDate"].ToDate();
                DateTime EndDate = queryParam["EndDate"].ToDate();

                strSql.AppendLine(" AND CONVERT(VARCHAR(10),emp.indate,120) BETWEEN @StartDate AND @EndDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            //合同到期
            if (!queryParam["hiredate"].IsEmpty() && !queryParam["firedate"].IsEmpty())
            {
                DateTime hiredate = queryParam["hiredate"].ToDate();
                DateTime firedate = queryParam["firedate"].ToDate();

                strSql.AppendLine(" AND CONVERT(VARCHAR(10),emp.hiredate,120)>=@hiredate AND CONVERT(VARCHAR(10),emp.firedate,120)<=@firedate ");
                parameter.Add(DbParameters.CreateDbParameter("@hiredate", hiredate));
                parameter.Add(DbParameters.CreateDbParameter("@firedate", firedate));
            }
            // 用工来源
            if (!queryParam["contractfromid"].IsEmpty())
            {
                int contractfromid = queryParam["contractfromid"].ToInt();
                strSql.AppendLine(" AND emp.contractfromid=@contractfromid ");
                parameter.Add(DbParameters.CreateDbParameter("@contractfromid", contractfromid));
            }

            // 离职时间
            if (!queryParam["outStart"].IsEmpty() && !queryParam["outEnd"].IsEmpty())
            {
                string outStart = queryParam["outStart"].ToString();
                string outEnd = queryParam["outEnd"].ToString();

                strSql.Append(" AND  CONVERT(VARCHAR(10),fireoutdate,120)>=@outStart AND  CONVERT(VARCHAR(10),fireoutdate,120)<=@outEnd ");
                parameter.Add(DbParameters.CreateDbParameter("@outStart", outStart));
                parameter.Add(DbParameters.CreateDbParameter("@outEnd", outEnd));
            }

            // 职级变动时间
            if (!queryParam["scdate"].IsEmpty() && !queryParam["ecdate"].IsEmpty())
            {
                string scdate = queryParam["scdate"].ToString();
                string ecdate = queryParam["ecdate"].ToString();

                strSql.Append(" AND empid IN (select distinct(empid) from hr_employexchange where CONVERT(VARCHAR(10),sdate,120)>=@scdate AND CONVERT(VARCHAR(10),sdate,120)<=@ecdate )");

                parameter.Add(DbParameters.CreateDbParameter("@scdate", scdate));
                parameter.Add(DbParameters.CreateDbParameter("@ecdate", ecdate));
            }


            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="deptid">部门编号</param>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetList(int deptid)
        {
            var expression = LinqExtensions.True<EmployinfoEntity>();
            if (deptid != 0)
            {
                expression = expression.And(t => t.deptid == deptid);
            }
            expression = expression.And(t => t.status == 0);
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, Int16 State, DateTime? fireoutdate)
        {
            EmployinfoEntity employinfoEntity = new EmployinfoEntity();
            employinfoEntity.Modify(keyValue);
            employinfoEntity.status = State;
            if (State == 1)
            {
                employinfoEntity.fireoutdate = fireoutdate;
            }
            else
            {
                employinfoEntity.fireoutdate = null;
            }
            this.BaseRepository().Update(employinfoEntity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, EmployinfoEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);

                if (entity.fireoutdate == null) 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update hr_employinfo set fireoutdate=null where empid = " + keyValue);
                    this.BaseRepository().ExecuteBySql(strSql.ToString());
                }
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 合同续费
        /// </summary>
        /// <param name="keyValue">续费员工编号</param>
        /// <param name="firedate">续费时间</param>
        public void ExpireFrom(string keyValue, DateTime? firedate)
        {
            if (keyValue != null && !string.IsNullOrEmpty(keyValue) && firedate != null && !string.IsNullOrEmpty(firedate.ToString()))
            {
                EmployinfoEntity ent = new EmployinfoEntity();
                ent.firedate = firedate;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update hr_employinfo set firedate='"+ firedate + "' ");

                var expression = LinqExtensions.True<EmployinfoEntity>();
                if (keyValue.IndexOf(',') == -1)
                {
                    strSql.Append(" where empid="+ keyValue);
                }
                else
                {
                    string[] empid = keyValue.Split(',');
                    string str = "";
                    foreach (var item in empid)
                    {
                        str += item.ToInt() + ",";
                    }
                    str = str.Substring(0, str.Length - 1);

                    strSql.AppendFormat("  where empid in ({0}) ", str);                    
                }
                this.BaseRepository().ExecuteBySql(strSql.ToString());
            }
        }

        #endregion 提交数据
    }
}