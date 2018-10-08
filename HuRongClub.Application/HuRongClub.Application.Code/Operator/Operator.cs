using System;

namespace HuRongClub.Application.Code
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.10.10
    /// 描 述：当前操作者信息类
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登陆账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secretkey { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 对象用户关系Id,对象分类:1-部门2-角色3-岗位4-群组
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 登录IP地址所在地址
        /// </summary>
        public string IPAddressName { get; set; }

        /// <summary>
        /// 是否系统账户；拥有所以权限
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 登录Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户数据权限
        /// </summary>
        public AuthorizeDataModel DataAuthorize { get; set; }

        /// <summary>
        /// 老系统用户ID
        /// </summary>
        public string OldSystemUserID { get; set; }
        /// <summary>
        /// 用户与物业关联
        /// </summary>
        public string UserProperty { set; get; }
        /// <summary>
        /// 是否是公司领导0 否 1是
        /// </summary>
        public int IsLeader { set; get; }
        /// <summary>
        /// 管辖部门
        /// </summary>
        public string ManageDepartment { set; get; }
    }
}