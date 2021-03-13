//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-14 02:32:10 
// 版本：1.0 
// 备注：
//***********************************************************
//***********************************************************
// 描述：Product实体类
// 作者：fanwei 
// 创建时间：2021-03-14 02:32:05 
// 版本：1.0 
// 备注：此代码为工具生成 请勿手工修改
//***********************************************************
/// <summary>
/// Product实体类
/// </summary>
public class ProductEntity : AbstractEntity
{
   /// <summary>
   /// 商品名称
   /// </summary>
   public string Name { get; set; }
   /// <summary>
   /// 商品价格
   /// </summary>
   public int Piece { get; set; }
   /// <summary>
   /// 商品图片名称
   /// </summary>
   public string PicName { get; set; }
   /// <summary>
   /// 商品描述
   /// </summary>
   public string Desc { get; set; }
}
