using Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using static Common.PipeCommand;

namespace FileSenderDemo
{
    /// <summary>
    /// 通讯管道服务端
    /// </summary>
    public class PipeServer : PipeBase
    {
        /// <summary>
        /// 接收执行GUID
        /// </summary>
        private string m_lastRevGuid = string.Empty;
        /// <summary>
        /// 接收执行消息
        /// </summary>
        private string m_lastRevMsg = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PipeServer(string name) : base(name)
        {

        }

        /// <summary>
        /// 判断接收GUID
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="guid">GUID</param>
        /// <param name="timeOut">超时时间[s]（-1 无超时）</param>
        /// <returns></returns>
        public bool JudgeRevGuid(out string message, string guid, int timeOut = -1)
        {
            DateTime startTime = DateTime.Now;
            while (timeOut == -1 || (DateTime.Now - startTime).TotalSeconds <= timeOut)
            {
                if (m_lastRevGuid == guid) { message = m_lastRevMsg; return true; }

                Thread.Sleep(1);
            }

            message = string.Empty;
            return false;
        }

        /// <summary>
        /// 启动服务端
        /// </summary>
        public override void StartPipeStream()
        {
            //初始化服务端
            m_pipeStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            PushLog("PipeServer.StartPipeServer", $"管道创建成功: {PipeName}");

            Task.Factory.StartNew(() =>
            {
                (m_pipeStream as NamedPipeServerStream).WaitForConnection();

                IsConnected = true;
                PushLog("PipeServer.ServerTask", $"客户端连接成功");

                //初始化读取流
                m_streamReader = new StreamReader(m_pipeStream);

                //初始化写入流
                m_streamWriter = new StreamWriter(m_pipeStream)
                {
                    AutoFlush = true,
                };

                try
                {
                    while (true)
                    {
                        //读取命令
                        string command = m_streamReader.ReadLine();

                        // 异常命令
                        if (string.IsNullOrEmpty(command))
                        {
                            PushLog("PipeServer.ServerTask", $"管道已被关闭");
                            break;
                        }

                        // 指令命令
                        if (command.StartsWith($"{CMD_REV}{SYM_SPLIT}"))
                        {
                            m_lastRevMsg = command.Split(SYM_SPLIT).Length > 2 ? command.Split(SYM_SPLIT)[2] : string.Empty;
                            m_lastRevGuid = command.Split(SYM_SPLIT)[1];
                        }

                        // 其他命令
                        PushCommand(command);
                    }
                }
                catch (Exception ex)
                {
                    PushLog("PipeServer.ServerTask", $"管道发生异常: 断开连接: {ex.Message}");
                }

                IsConnected = false;
                PushLog("PipeServer.ServerTask", $"管道已经关闭");

                PushCommand(CMD_EXIT);
            });
        }

        /// <summary>
        /// 停止服务端
        /// </summary>
        public override void StopPipeStream()
        {
            IsConnected = false;
        }
    }
}
