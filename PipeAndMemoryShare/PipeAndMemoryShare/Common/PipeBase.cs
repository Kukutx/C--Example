using System;
using System.IO;
using System.IO.Pipes;

namespace Common
{
    /// <summary>
    /// 管道基类
    /// </summary>
    public abstract class PipeBase
    {
        /// <summary>
        /// 管道流
        /// </summary>
        protected PipeStream m_pipeStream;
        /// <summary>
        /// 数据读取流
        /// </summary>
        protected StreamReader m_streamReader;
        /// <summary>
        /// 数据写入流
        /// </summary>
        protected StreamWriter m_streamWriter;

        /// <summary>
        /// 日志上报事件
        /// </summary>
        public event LogHandler LogEvent;
        /// <summary>
        /// 指令上报事件
        /// </summary>
        public event CommandHandler CommandEvent;

        /// <summary>
        /// 管道名称
        /// </summary>
        public string PipeName { get; protected set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected { get; protected set; }

        /// <summary>
        /// 日志上报
        /// </summary>
        /// <param name="location">上报位置</param>
        /// <param name="message">日志信息</param>
        protected void PushLog(string location, string message) => LogEvent?.Invoke(location, message);

        /// <summary>
        /// 指令上报
        /// </summary>
        /// <param name="command">指令</param>
        protected void PushCommand(string command) => CommandEvent?.Invoke(command);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">管道名称</param>
        public PipeBase(string name)
        {
            PipeName = name;
        }

        /// <summary>
        /// 发送一行命令
        /// </summary>
        /// <param name="command">命令</param>
        public bool SendCommand(string command)
        {
            try
            {
                m_streamWriter.WriteLine(command);
                m_pipeStream.WaitForPipeDrain();
                return true;
            }
            catch (Exception ex)
            {
                PushLog("PipeBase.SendCommand", $"管道消息发送异常: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 启动管道流
        /// </summary>
        public abstract void StartPipeStream();

        /// <summary>
        /// 停止管道流
        /// </summary>
        public abstract void StopPipeStream();
    }
}
