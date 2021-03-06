using System.Net;

namespace ConsoleApplication
{
    class Program
    {
        string FTPAddress = "ftp://***.***.***.**:21"; //ftp服务器地址
        string FTPUsername = "your username";   //用户名
        string FTPPwd = "your password";        //密码
        //上传
        private void UpFile()
        {
            string LocalPath = "F:\\ftp\\text.txt"; //待上传文件
            FileInfo f = new FileInfo(LocalPath);
            string FileName = f.Name;
            //Path = Path.Replace("\\", "/");
            string ftpRemotePath = "/home/test/";
            string FTPPath = FTPAddress + ftpRemotePath + FileName; //上传到ftp路径,如ftp://***.***.***.**:21/home/test/test.txt
            //实现文件传输协议 (FTP) 客户端
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd); //设置通信凭据
            reqFtp.KeepAlive = false; //请求完成后关闭ftp连接
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            //读本地文件数据并上传
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.Write("上传失败");
            }
        }

        //下载
        private void DownFile()
        {
            string FtpFilePath = "/home/test.txt";   //远程路径
            string LocalPath = "F:\\ftp\\test.txt"; //下载到的本地路径
            if (File.Exists(LocalPath))
            {
                File.Delete(LocalPath);
            }
            string FTPPath = FTPAddress + FtpFilePath;
            //建立ftp连接
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUsername, FTPPwd);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int buffersize = 2048;
            int readCount;
            byte[] buffer = new byte[buffersize];
            readCount = ftpStream.Read(buffer, 0, buffersize);
            //创建并写入文件
            FileStream OutputStream = new FileStream(LocalPath, FileMode.Create);
            while (readCount > 0)
            {
                OutputStream.Write(buffer, 0, buffersize);
                readCount = ftpStream.Read(buffer, 0, buffersize);
            }
            ftpStream.Close();
            OutputStream.Close();
            response.Close();
            if (File.Exists(LocalPath))
                Console.Write("下载完毕");
        }

        static void Main(string[] args)
        {
            //非静态方法必须先实例化对象，然后再使用
            ConsoleApplication.Program p = new ConsoleApplication.Program();
            p.UpFile();
            p.DownFile();
        }
    }
}