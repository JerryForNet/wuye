//=====================================================================================
// All Rights Reserved , Copyright © Learun 2013
//=====================================================================================
using System;
using System.Security.Cryptography;
using System.Text;

namespace HuRongClub.Util
{
    /// <summary>
    /// DES 对称加密辅助类
    /// 需要注意的是，DES加密是以数据块为单位加密的，8个字节一个数据块，如果待加密明byte[]的长度不是8字节的整数倍，算法先用值为“0”的byte补足8个字节，然后进行加密。
    /// Author:LiJun Time:2016/06/24 17:39:08
    /// Versions: 1.0.0.1
    /// </summary>
    public class DESEncrypt
    {
        // 默认key
        private static string[] privateKey = new string[] { "learun###***", "Hu!@Rong", "MATICSOFT", "HURONGCLUB" };

        public static string HuRong = "Hu!@Rong";

        #region ========加密========

        /// <summary>
        /// 使用默认key加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        [Obsolete]
        public static string Encrypt(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }
            return Encrypt(Text, privateKey[0]);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========

        /// <summary>
        /// 解密,请使用带有key的 Decrypt
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        [Obsolete]
        public static string Decrypt(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }

            string val = "";
            foreach (var key in privateKey)
            {
                val = Decrypt(Text, key);
                if (!string.IsNullOrEmpty(val))
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(val))
            {
                throw new Exception("解密失败,请使用正确的key");
            }

            return val;

            //return Decrypt(Text, privateKey[0]);
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }

            string val = "";
            string[] newKey = new string[] { sKey, "Hu!@Rong", "MATICSOFT", "HURONGCLUB" };
            foreach (var key in newKey)
            {
                val = DecryptDo(Text, key);
                if (!string.IsNullOrEmpty(val))
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(val))
            {
                throw new Exception("解密失败,请使用正确的key");
            }

            return val;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptDo(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                int len;
                len = Text.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}