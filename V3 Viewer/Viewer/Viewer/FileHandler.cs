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
        private string filename;
        private long fileLen;
        private long listSize;
        private long filelen;
        private long maxSliderSize;
        private int[] fieldSizes;
        private string[] columnNames;
        private FileStream fileStream;
        private List<string> headers;
        List<Dictionary<string, object>> rows;

        public FileHandler()
        {
        }

        public void LoadFile(string filename)
        {
            this.filename = filename;
            fileStream = new FileStream(filename, FileMode.Open);
        }

        public void CalculateFileInfo()
        {
            var fileInfo = new FileInfo(filename);
            filelen = fileInfo.Length;
            listSize = filelen / lineSize;
            maxSliderSize = listSize - 1; // out by one
        }

        public void SetFileInfo(FileInfo fileinfo)
        {
            fileLen = fileinfo.Length;
        }

        public void CloseFile()
        {
            fileStream.Close();
        }

        public void SetupHeaders()
        {
            headers = new List<string>();
            foreach (var col in columnNames)
            {
                headers.Add(col);
            }
        }

        public void SetupRows(long offset, int height, int font)
        {
            if (fileStream == null) return;
            var buffer = new byte[lineSize];
            fileStream.Seek(offset, 0);

            rows = new List<Dictionary<string, object>>();
            rows.Clear();
            for (var i = 0; i < (int)(height / (font + 10)); ++i)
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
                rows.Add(row);
            }
        }

        public List<Dictionary<string, object>> GetRows()
        {
            return rows;
        }

        public void CalculateFieldSizes()
        {
            using (var stream = new StreamReader(filename + ".hd"))
            {
                var names = stream.ReadLine();
                var widths = stream.ReadLine();
                columnNames = new string[names.Split(',').Length];
                fieldSizes = new int[widths.Split(',').Length];
                var i = 0;
                foreach (var name in names.Split(','))
                {
                    fieldSizes[i] = int.Parse(widths.Split(',')[i]);
                    columnNames[i++] = name;
                }

                lineSize = fieldSizes.Sum() + 1;
                System.Diagnostics.Debug.WriteLine("line size: " + lineSize);
            }
        }

        public List<string> GetHeaders()
        {
            return headers;
        }

        public long GetLineSize()
        {
            return lineSize;
        }
    }
}
