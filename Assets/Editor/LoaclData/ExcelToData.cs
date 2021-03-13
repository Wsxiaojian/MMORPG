//***********************************************************
// 描述：将Excel文件转化为 bytedata文件
// 作者：fanwei 
// 创建时间：2021-03-13 14:44:44 
// 版本：1.0 
// 备注：
//***********************************************************
using Excel;
using System.Data;
using System.IO;
using UnityEditor;


/// <summary>
/// 将Excel转化为data文件
/// </summary>
public class ExcelToData:Editor
{
    //异或因子
    private static byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子

    /// <summary>
    /// 将Excel转化为data数据
    /// </summary>
    /// <param name="path"></param>
    public static bool DoExcelToData(string path)
    {
        DataTable dt = LoadExcelData(path);

        if (dt == null)
        {
            return false;
        }

        CreateData(path, dt);

        return true;
    }

    /// <summary>
    /// 读取Excel表格数据
    /// </summary>
    /// <returns></returns>
    public static DataTable LoadExcelData(string path)
    {

        if (string.IsNullOrEmpty(path)) return null;

        DataTable dataTable = null;

        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            //2003版本 使用CreateBinaryReader
            //2007以上版本 使用CreateOpenXmlReader
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
            {
                DataSet result = excelReader.AsDataSet();

                dataTable = result.Tables[0];
            }
        }
        return dataTable;
    }

    /// <summary>
    /// 创建加密数据
    /// </summary>
    /// <param name="path"></param>
    /// <param name="dt"></param>
    public static void CreateData(string path, DataTable dt)
    {
        //文件夹路径
        string filePath = path.Substring(0, path.LastIndexOf('\\') + 1);
        //文件全称含后缀
        string fileFullName = path.Substring(path.LastIndexOf('\\') + 1);
        //文件名称
        string fileName = fileFullName.Substring(0, fileFullName.LastIndexOf('.'));

        //-------------------------------
        // 第一步 读取文件byte数组
        //------------------------------
        byte[] buffer = null;
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            int row = dt.Rows.Count;
            int column = dt.Columns.Count;

            //先写入行列
            ms.WriteInt(row);
            ms.WriteInt(column);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    //写入内容
                    ms.WriteUTF8String(dt.Rows[i][j].ToString().Trim());
                }
            }
            buffer = ms.ToArray();
        }

        //-------------------------------
        // 第二步 xor 加密
        //------------------------------
        int iScaleLen = xorScale.Length;
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
        }

        //-------------------------------
        // 第三步 压缩
        //------------------------------
        buffer = ZlibHelper.CompressBytes(buffer);

        //-------------------------------
        // 第四步 写入文件
        //------------------------------
        FileStream fs = new FileStream(string.Format("{0}{1}", filePath, fileName + ".data"), FileMode.Create);
        fs.Write(buffer,0,buffer.Length);
        fs.Close();

        //同时创建 DBModel 和 Entity
        
    }
  

    private void CreateDBModel()
    {

    }

    private void CreateEntity()
    {

    }
}
