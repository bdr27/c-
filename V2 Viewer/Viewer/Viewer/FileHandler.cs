using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer
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

        private void setupRows(long offset)
        {
            if (fileStream == null) return;
            ListView.Items.Clear();
            var buffer = new byte[lineSize];
            fileStream.Seek(offset, 0);

            for (var i = 0; i < (int)(ListView.ActualHeight / (ListView.FontSize + 10)); ++i)
            {
                fileStream.Read(buffer, 0, lineSize);
                var line = Encoding.UTF8.GetString(buffer);
                System.Diagnostics.Debug.WriteLine("line: " + line);

                var startIndex = 0;
                var j = 0;

                var row = new Dictionary<string, object>();
                foreach (var col in columnNames)
                {
                    var field = line.Substring(startIndex, fieldSizes[j]);
                    row.Add(col, field.Trim());
                    startIndex += fieldSizes[j++];
                }

                ListView.Items.Add(row);
            }
        }
    }
}
