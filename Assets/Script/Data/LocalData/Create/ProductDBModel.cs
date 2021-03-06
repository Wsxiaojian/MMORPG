//***********************************************************
// 描述：Product数据管理类
// 作者：fanwei 
// 创建时间：2021-03-15 12:22:34 
// 版本：1.0 
// 备注：此代码为工具生成 请勿手工修改
//***********************************************************
/// <summary>
/// Product数据管理类
/// </summary>
public partial class ProductDBModel : AbstractDBModel<ProductDBModel, ProductEntity>
{
    /// <summary>
   /// 文件名称
   /// </summary>
   protected override string FileName { get {  return "Product.data"; } }
   /// <summary>
   /// 创建实体
   /// </summary>
   /// <param name="parser"></param>
   /// <returns></returns>
   protected override ProductEntity MakeEntity(GameDataTableParser parser)
   {
      ProductEntity entity = new ProductEntity();
      entity.ID = parser.GetFileValue(parser.FieldName[0]).ToInt();
      entity.Name = parser.GetFileValue(parser.FieldName[1]);
      entity.Piece = parser.GetFileValue(parser.FieldName[2]).ToInt();
      entity.PicName = parser.GetFileValue(parser.FieldName[3]);
      entity.Desc = parser.GetFileValue(parser.FieldName[4]);
      entity.Pos = parser.GetFileValue(parser.FieldName[5]);
      return entity;
   }
}
