namespace Common
{
    /// <summary>
    /// 管道命令
    /// </summary>
    public static class PipeCommand
    {
        /// <summary>
        /// 分隔符号
        /// </summary>
        public const char SYM_SPLIT = '|';
        /// <summary>
        /// 连接断开
        /// </summary>
        public const string ERR_CONNECTION_BROKEN = "#ERR_CONNECTION_BROKEN";
        /// <summary>
        /// 读取超时
        /// </summary>
        public const string ERR_READ_TIME_OUT = "#ERR_READ_TIME_OUT";
        /// <summary>
        /// 退出连接
        /// </summary>
        public const string CMD_EXIT = "#CMD_EXIT";
        /// 开始发送
        /// </summary>
        public const string CMD_START = "#CMD_START";
        /// <summary>
        /// 数据大小
        /// </summary>
        public const string CMD_SIZE = "#CMD_SIZE";
        /// <summary>
        /// 文件路径
        /// </summary>
        public const string CMD_PATH = "#CMD_PATH";
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public const string CMD_SAVE_PATH = "#CMD_SAVE_PATH";
        /// <summary>
        /// 校验码
        /// </summary>
        public const string CMD_MD5 = "#CMD_MD5";
        /// <summary>
        /// 准备完成
        /// </summary>
        public const string CMD_READY = "#CMD_READY";
        /// <summary>
        /// 结束发送
        /// </summary>
        public const string CMD_END = "#CMD_END";
        /// <summary>
        /// 接收命令
        /// </summary>
        public const string CMD_REV = "#CMD_REV";
    }
}
