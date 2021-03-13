//***********************************************************
// 描述：抽象数据实体控制类
// 作者：fanwei 
// 创建时间：2021-03-12 17:43:46 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象数据实体控制类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T, P>
    where T : class, new()          //实体控制类
    where P : AbstractEntity      //实体信息类
{
    /// <summary>
    /// 数据集合
    /// </summary>
    protected List<P> m_PDataList;
    /// <summary>
    /// 数据集合
    /// </summary>
    protected Dictionary<int, P> m_PDataDic;

    #region 单例
    private static T instance;
    /// <summary>
    /// 单例
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 构造方法
    /// </summary>
    protected AbstractDBModel()
    {
        m_PDataList = new List<P>();
        m_PDataDic = new Dictionary<int, P>();

        //加载数据
        LoadData();
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        //读取文件data数据
        string path = string.Format("{0}/DataToExcel/{1}", Application.dataPath, FileName);
        using(GameDataTableParser parser = new GameDataTableParser(path))
        {
            while (parser.Eof ==false)
            {
                //创建实体
                P p = MakeEntity(parser);

                m_PDataList.Add(p);
                m_PDataDic[p.ID] = p;

                //下一个
                parser.Next();
            }
        }
    }

    #region 子类实现
    /// <summary>
    /// 子类实现   文件名
    /// </summary>
    protected abstract string FileName { get; }
    /// <summary>
    /// 子类实现   创建实体
    /// </summary>
    protected abstract P MakeEntity(GameDataTableParser parser);
    #endregion


    #region  对外访问  获取数据
    /// <summary>
    /// 获取所有信息
    /// </summary>
    /// <returns></returns>
    public List<P> GetAll()
    {
        return m_PDataList;
    }

    /// <summary>
    /// 获取单个数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public P Get(int id)
    {
        if (m_PDataDic.ContainsKey(id))
        {
            return m_PDataDic[id];
        }
        return null;
    }
    #endregion
}
