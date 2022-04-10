using Common;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using static Common.PipeCommand;

namespace FileReceiverDemo
{
    /// <summary>
    /// 通讯管道客户端
    /// </summary>
    public sealed class PipeClient : PipeBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">管道名称</param>
        public PipeClient(string name) : base(name)
        {

        }

        /// <summary>
        /// 发送回复命令
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public bool SendRevCommand(string guid, string errMsg = "")
        {
            if (string.IsNullOrEmpty(errMsg))
            {
                return SendCommand($"{CMD_REV}{SYM_SPLIT}{guid}");
            }
            else
            {
                return SendCommand($"{CMD_REV}{SYM_SPLIT}{guid}{SYM_SPLIT}{errMsg}");
            }
        }

        /// <summary>
        /// 启动客户端
        /// </summary>
        public override void StartPipeStream()
        {
            //初始化服务端
            m_pipeStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            PushLog("PipeClient.StartPipeClient", $"管道创建成功: {PipeName}");

            Task.Factory.StartNew(() =>
            {
                try
                {
                    (m_pipeStream as NamedPipeClientStream).Connect(1000);
                }
                catch (TimeoutException)
                {
                    if (!m_pipeStream.IsConnected)
                    {
                        PushLog("PipeClient.ClientTask", $"服务端连接失败");
                        return;
                    }
                }

                IsConnected = true;
                PushLog("PipeClient.ClientTask", $"服务端连接成功");

                //初始化读取流
                m_streamReader = new StreamReader(m_pipeStream);

                //初始化写入流
                m_streamWriter = new StreamWriter(m_pipeStream)
                {
                    AutoFlush = true,
                };

                try
                {
                    while (IsConnected && m_pipeStream.IsConnected)
                    {
                        //读取命令
                        string command = m_streamReader.ReadLine();

                        // 异常命令
                        if (string.IsNullOrEmpty(command))
                        {
                            PushLog("PipeClient.ClientTask", $"管道已被关闭");
                            break;
                        }

                        // 其他命令
                        PushCommand(command);
                    }
                }
                catch (Exception ex)
                {
                    PushLog("PipeClient.ClientTask", $"管道发生异常: 断开连接: {ex.Message}");
                }

                IsConnected = false;
                PushLog("PipeClient.ClientTask", $"管道已经关闭");

                PushCommand(CMD_EXIT);
            });
        }

        /// <summary>
        /// 停止客户端 
        /// </summary>
        public override void StopPipeStream()
        {
            IsConnected = false;
        }
    }
}
