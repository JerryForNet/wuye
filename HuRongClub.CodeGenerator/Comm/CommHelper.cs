namespace HuRongClub.CodeGenerator.Comm
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommHelper
    {
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindModelsType(string name)
        {
            name = name.ToLower();
            if (name == "int" || name == "number" || name == "integer")
            {
                return "int?";
            }
            else if (name == "smallint")
            {
                return "Int16?";
            }
            else if (name == "tinyint")
            {
                return "byte?";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single?";
            }
            else if (name == "float")
            {
                return "float?";
            }
            else if (name == "decimal" || name == "number(8,2)")
            {
                return "decimal?";
            }
            else if (name == "char" || name == "varchar" || name == "nvarchar2" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool?";
            }
            else if (name == "datetime" || name == "date" || name == "smalldatetime")
            {
                return "DateTime?";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double?";
            }
            else
            {
                return "string";
            }
        }
    }
}