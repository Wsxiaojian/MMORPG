//***********************************************************
// 描述：读取XML文件  数据访问层DAL
// 作者：fanwei 
// 创建时间：2021-03-22 07:21:52 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections.Generic;
using System.Xml.Linq;


/// <summary>
/// 读取XML文件  数据访问层DAL
/// </summary>
public class AssetBundleDAL
{
    /// <summary>
    /// xml 平台路径
    /// </summary>
    private string m_Path;

    /// <summary>
    /// 所有需要打包到资源
    /// </summary>
    private List<AssetBundleEntity> m_EntityList = null;


    public AssetBundleDAL(string path)
    {
        m_Path = path;
        m_EntityList = new List<AssetBundleEntity>();
    }

    /// <summary>
    /// 获取需要打包到资源实体集合
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> Get()
    {
        m_EntityList.Clear();

        XDocument xDoc = XDocument.Load(m_Path);
        XElement rootElement = xDoc.Element("Root");
        XElement assetBundle   = rootElement.Element("AssetBundle");
        IEnumerable<XElement> itemIE = assetBundle.Elements("Item");

        int index=0;
        foreach(XElement item in itemIE)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key" + ++index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.Version = item.Attribute("Version").Value.ToInt();
            entity.Size = item.Attribute("Size").Value.ToLong();
            entity.ToPath = item.Attribute("ToPath").Value;

            IEnumerable<XElement>  pahtIE = item.Elements("Path");
            foreach (var path in pahtIE)
            {
                entity.PathList.Add(path.Attribute("Value").Value);
            }
            //加入集合
            m_EntityList.Add(entity);
        }
        return m_EntityList;
    }

}
