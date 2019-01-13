using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HuRongClub.Util
{
    public class XmlHelper
    {
        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch
            {
                throw new Exception("反序列化失败");
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }

        #endregion

        #region 序列化XML文件

        /// <summary>
        /// 序列化XML文件
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            //创建序列化对象
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            return str;
        }

        #endregion

        #region 将XML转换为DATATABLE

        /// <summary>
        /// 将XML转换为DATATABLE
        /// </summary>
        /// <param name="FileURL"></param>
        /// <returns></returns>
        public static DataTable XmlAnalysisArray()
        {
            try
            {
                string FileURL = System.Configuration.ConfigurationManager.AppSettings["Client"].ToString();
                DataSet ds = new DataSet();
                ds.ReadXml(FileURL);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write(ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 将XML转换为DATATABLE
        /// </summary>
        /// <param name="FileURL"></param>
        /// <returns></returns>
        public static DataTable XmlAnalysisArray(string FileURL)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(FileURL);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write(ex.Message.ToString());
                return null;
            }
        }

        #endregion

        #region 获取对应XML节点的值

        /// <summary>
        /// 摘要:获取对应XML节点的值
        /// </summary>
        /// <param name="stringRoot">XML节点的标记</param>
        /// <returns>返回获取对应XML节点的值</returns>
        public static string XmlAnalysis(string stringRoot, string xml)
        {
            if (stringRoot.Equals("") == false)
            {
                try
                {
                    XmlDocument XmlLoad = new XmlDocument();
                    XmlLoad.LoadXml(xml);
                    return XmlLoad.DocumentElement.SelectSingleNode(stringRoot).InnerXml.Trim();
                }
                catch
                {
                    throw new Exception("解析XML失败");
                }
            }
            return "";
        }


        /// <summary>
        /// 摘要:获取对应XML节点的值
        /// </summary>
        /// <param name="stringRoot">XML节点的标记</param>
        /// <returns>返回获取对应XML节点的值</returns>
        public static string XmlNodeFind(string stringRoot, string xml)
        {
            if (stringRoot.Equals("") == false)
            {
                try
                {
                    XmlDocument XmlLoad = new XmlDocument();
                    XmlLoad.LoadXml(xml);
                    XmlNodeList nodes = XmlLoad.SelectNodes(stringRoot);
                    if (nodes != null && nodes.Count > 0)
                    {
                        return nodes[0].InnerText.Trim();
                    }
                }
                catch
                {
                    throw new Exception("解析XML失败");
                }
            }
            return "";
        }

        #endregion
    }
}