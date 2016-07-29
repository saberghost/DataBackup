using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBackup
{
    class Zip
    {
        public static void CompressFile(string SourceFile, string destinationFile)
        {
            if (!File.Exists(SourceFile))
            {
                throw new FileNotFoundException();
            }
            using (FileStream sourceStream = new FileStream(SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (GZipStream compressStream = new GZipStream(destinationStream, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[1024 * 1024 * 6];
                        int checkCount = 0;
                        double temp = 0;
                        int CursorTop = Console.CursorTop;
                        Console.CursorVisible = false;
                        while ((checkCount = sourceStream.Read(buffer, 0, buffer.Length)) >= buffer.Length)
                        {
                            compressStream.Write(buffer, 0, buffer.Length);
                            temp += buffer.Length;
                            Console.WriteLine("正在压缩：" + (temp / sourceStream.Length).ToString("0%"));
                            Console.SetCursorPosition(0, CursorTop);
                        }
                        compressStream.Write(buffer, 0, checkCount);
                        Console.WriteLine("正在压缩：" + "100%");
                        Console.CursorVisible = true;
                        Console.WriteLine("压缩完成！");
                    }
                }
            }
            File.Delete(SourceFile);
        }
    }
}
