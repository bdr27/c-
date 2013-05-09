using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        private List<Dictionary<string, object>> rows;

        public FileHandler()
        {
        }

        /// <summary>
        /// Loads the file
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFile(string filename)
        {
            this.filename = filename;
            fileStream = new FileStream(filename, FileMode.Open);
        }

        /// <summary>
        /// Cacluculates the info of the file
        /// </summary>
        public void CalculateFileInfo()
        {
            var fileInfo = new FileInfo(filename);
            filelen = fileInfo.Length;
            listSize = filelen / lineSize;
            maxSliderSize = listSize - 1; // out by one
        }

        /// <summary>
        /// Sets the file length based on info
        /// </summary>
        /// <param name="fileinfo"></param>
        public void SetFileInfo(FileInfo fileinfo)
        {
            fileLen = fileinfo.Length;
        }

        /// <summary>
        /// Closes the filestream
        /// </summary>
        public void CloseFile()
        {
            fileStream.Close();
        }

        /// <summary>
        /// Generates the list of headers based on the file input
        /// </summary>
        public void SetupHeaders()
        {
            headers = new List<string>();
            foreach (var col in columnNames)
            {
                headers.Add(col);
            }
        }

        /// <summary>
        /// Set's up the rows of data that will be displayed
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="height"></param>
        /// <param name="font"></param>
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

        /// <summary>
        /// Calculates the Field Size
        /// </summary>
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
