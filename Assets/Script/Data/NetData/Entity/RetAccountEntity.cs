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
    /// <summary>
    /// 账户id
    /// </summary>
    public int Id;
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName;
    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd;
    /// <summary>
    /// 元宝数量
    /// </summary>
    public int Yuanbao;
    /// <summary>
    /// 最后登陆服务器id
    /// </summary>
    public int LastLogonServerId;
    /// <summary>
    /// 最后登陆服务器名称
    /// </summary>
    public string LastLogOnServerName;
    /// <summary>
    /// 最后登陆服务器ip
    /// </summary>
    public string LastLogOnServerIp;
    /// <summary>
    /// 最后登陆服务器端口
    /// </summary>
    public int LastLogOnServerPort;

}