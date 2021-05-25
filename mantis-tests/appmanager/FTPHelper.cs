using System.IO;
using System.Net;
using FluentFTP;

namespace mantis_tests.appmanager
{
    public class FTPHelper : HelperBase
    {
        private readonly FtpClient _client;

        public FTPHelper(ApplicationManager manager) : base(manager)
        {
            _client = new FtpClient {Credentials = new NetworkCredential("mantis", "mantis"), Host = "localhost"};
            _client.Connect();
        }

        public void BackUpFile(string path)
        {
            var backUpPath = path + ".bak";
            if (_client.FileExists(backUpPath))
            {
                return;
            }

            _client.Rename(path, backUpPath);
        }

        public void UploadFile(string path, Stream localFile)
        {
            if (_client.FileExists(path))
            {
                _client.DeleteFile(path);
            }

            using (var ftpStream = _client.OpenWrite(path))
            {
                var buffer = new byte[8 * 1024];
                var count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }

        public void RestoreBackUpFile(string path)
        {
            var backUpPath = path + ".bak";
            if (!_client.FileExists(backUpPath))
            {
                return;
            }

            if (!_client.FileExists(path))
            {
                _client.DeleteFile(path);
            }

            _client.Rename(backUpPath, path);
        }
    }
}