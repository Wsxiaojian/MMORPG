//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-15 13:37:30 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// Product实体类
/// </summary>
public partial class ProductEntity : AbstractEntity
{
    /// <summary>
    /// 例子  获取保存的坐标值
    ///     等等 
    /// </summary>
    public Vector3 RelPos
    {
        get
        {
            string[] posStr = Pos.Split('_');
            if(posStr.Length == 3)
            {
                return new Vector3(posStr[0].ToFloat(), posStr[1].ToFloat(), posStr[2].ToFloat());
            }
            return Vector3.zero;
        }
    }
}
