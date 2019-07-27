using System.IO;

namespace ShortestPath
{
    public static class ResultWriter
    {
        private static readonly string resultPath = "../../../SearchResults/";
        public static void WriteLine(string fileName, object text)
        {
            File.AppendAllText(resultPath + fileName, text + "\n");
        }
    }
}