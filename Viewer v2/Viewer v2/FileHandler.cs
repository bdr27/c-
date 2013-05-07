using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Viewer_v2
{
    public class FileHandler
    {
        private int lineSize;
        private long fileLen;
        private long listSize;

        public FileHandler()
        {
        }

        public FileStream loadFile(string filename)
        {
            return new FileStream(filename, FileMode.Open);
        }

        public void setFileInfo(FileInfo fileinfo)
        {
            fileLen = fileinfo.Length;
        }

        public void closeFile(FileStream fileStream)
        {
            fileStream.Close();
        }
    }
}
