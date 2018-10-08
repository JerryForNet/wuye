using HuRongClub.Application.Busines;
using HuRongClub.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace HuRongClub.Application.Web.Content.Handler
{
    /// <summary>
    /// upload_ajax 的摘要说明
    /// </summary>
    public class upload_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = GetQueryString("action");

            switch (action)
            {
                case "EditorFile": //编辑器文件
                    EditorFile(context);
                    break;
                case "ManagerFile": //管理文件
                    ManagerFile(context);
                    break;
                default: //普通上传
                    UpLoadFile(context);
                    break;
            }
        }

        #region 上传文件处理===================================

        private void UpLoadFile(HttpContext context)
        {
            string _delfile = GetString("DelFilePath");
            HttpPostedFile _upfile = context.Request.Files["Filedata"];
            string filepath = GetQueryString("filepath");
            string filetypes = GetQueryString("filetypes");//文件类型
            string filetype = GetQueryString("filetype");//上传类型 3-任务上传
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (GetQueryString("IsWater") == "1")
                _iswater = true;
            if (GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;
            if (_upfile == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, filepath, filetypes, filetype);

            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }

        #endregion 上传文件处理===================================

        #region 编辑器上传处理===================================

        private void EditorFile(HttpContext context)
        {
            string filepath = GetQueryString("filepath");
            string filetypes = GetQueryString("filetypes");//文件类型
            string filetype = GetQueryString("filetype");//上传类型 3-任务上传
            bool _iswater = false; //默认不打水印
            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError(context, "请选择要上传文件！");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string remsg = upFiles.fileSaveAs(imgFile, false, _iswater, filepath, filetypes, filetype);
            Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(remsg);
            string status = dic["status"].ToString();
            string msg = dic["msg"].ToString();
            if (status == "0")
            {
                showError(context, msg);
                return;
            }
            string filePath = dic["path"].ToString(); //取得上传后的路径
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = filePath;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(hash));
            context.Response.End();
        }

        //显示错误
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(hash));
            context.Response.End();
        }

        #endregion 编辑器上传处理===================================

        #region 浏览文件处理=====================================

        private void ManagerFile(HttpContext context)
        {  
            //根目录路径，相对路径
            String rootPath = "/upload/file/"; //站点目录+上传目录
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = "/upload/file/"; //siteConfig.webpath + siteConfig.filepath + "/";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = Utils.GetMapPath(rootPath);
            String dirName = context.Request.QueryString["dir"];

            //根据path参数，设置各路径和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;

                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;

                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(result));
            context.Response.End();
        }

        #region Helper

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }

        #endregion Helper

        #endregion 浏览文件处理=====================================
               

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 方法
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, false);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            return HttpContext.Current.Request.QueryString[strName];
        }
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetQueryString(strName, false);
        }
        #endregion
    }


}