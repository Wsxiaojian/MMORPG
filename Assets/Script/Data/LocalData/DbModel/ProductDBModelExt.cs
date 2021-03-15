//***********************************************************
// 描述：Product数据管理类  扩展
// 作者：fanwei 
// 创建时间：2021-03-15 12:23:25 
// 版本：1.0 
// 备注：
//***********************************************************

/// <summary>
/// Product数据管理类  扩展
/// </summary>
public partial class ProductDBModel : AbstractDBModel<ProductDBModel, ProductEntity>
{
    /// <summary>
    /// 例子 获取最高价格的 商品
    /// </summary>
    /// <returns></returns>
   public ProductEntity GetHighestPiece()
    {
        int MaxPiece =-999;
        ProductEntity highestProd = null;
        for (int i = 0; i <m_PDataList.Count ; i++)
        {
            if(MaxPiece < m_PDataList[i].Piece)
            {
                MaxPiece = m_PDataList[i].Piece;
                highestProd = m_PDataList[i];
            }
        }
        return highestProd;
    }
}
