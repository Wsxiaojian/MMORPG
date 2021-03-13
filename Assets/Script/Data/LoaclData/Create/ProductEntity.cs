//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-13 18:05:44 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// 商品名称
    /// </summary>
    public string Name {get;set; }

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
