namespace Common
{
    /// <summary>
    /// 日志上报委托
    /// </summary>
    /// <param name="location">上报位置</param>
    /// <param name="message">日志信息</param>
    public delegate void LogHandler(string location, string message);

    /// <summary>
    /// 指令上报委托
    /// </summary>
    /// <param name="command">指令</param>
    public delegate void CommandHandler(string command);
}
