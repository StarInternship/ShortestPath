using System.IO;

namespace ShortestPath
{
    /// <summary>
    /// a class thar writes the result graph in .csv format
    /// </summary>
    public class ResultWriter
    {
        /// <summary>
        /// relative path of results directory
        /// </summary>
        private static readonly string resultPath = "../../../SearchResults/";
        /// <summary>
        /// .csv format
        /// </summary>
        private static readonly string format = ".csv";
        /// <summary>
        /// relative path to file
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// recreates the file.
        /// </summary>
        /// <param name="fileName">name of the result file(without format)</param>
        public ResultWriter(string fileName)
        {
            filePath = resultPath + fileName + format;
            File.Delete(filePath);
            File.Create(filePath);
        }

        /// <summary>
        /// writes a line in result file
        /// </summary>
        /// <param name="obj">object</param>
        public void WriteLine(object obj)
        {
            File.AppendAllText(filePath, obj + "\n");
        }
    }
}