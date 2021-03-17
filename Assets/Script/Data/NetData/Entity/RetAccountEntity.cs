//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-17 16:27:09 
// 版本：1.0 
// 备注：
//***********************************************************
using System;

  /// <summary>
  /// 账户实体
  /// </summary>
public class RetAccountEntity
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Pwd { get; set; }

    public int Yuanbao { get; set; }

    public int LastServerId { get; set; }

    public string LastServerName { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}