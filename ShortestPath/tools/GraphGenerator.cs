using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.tools
{
    class GraphGenerator
    {
        public static void Main(string[] args)
        {
            string fileName = @"grid1000.txt";
            int k = 1000;


            makeGideTestCase(fileName, k);
            //makeQkTestCase(fileName, k);
        }

        private static void makeGideTestCase(string fileAddress, int k)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileAddress))
            {
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        int weight1 = 1;
                        string str1 = (i + "-" + j + "," + i + "-" + (j + 1) + "," + weight1);
                        file.WriteLine(str1);

                        int weight2 = 1;
                        string str2 = (i + "-" + j + "," + (i + 1) + "-" + j + "," + weight2);
                        file.WriteLine(str2);
                    }
                }

                int w1 = 1;
                string s1 = (k + "-" + (k - 1) + "," + k + "-" + k + "," + w1);
                file.WriteLine(s1);

                int w2 = 1;
                string s2 = ((k - 1) + "-" + k + "," + k + "-" + k + "," + w2);
                file.WriteLine(s2);
            }
        }

        private static void makeQkTestCase(string fileAddress, int k)
        {
            using
            (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileAddress))
            {
                for (int i = 0; i < Math.Pow(2, k); i++)
                {
                    String binary = Convert.ToString(Convert.ToInt32(i), 2);
                    char[] chars = binary.ToCharArray();
                    char[] binaryNumber = new char[k];
                    for (int j = 0; j < k - chars.Length; j++)
                    {
                        binaryNumber[j] = '0';
                    }

                    for (int j = 0; j < chars.Length; j++)
                    {
                        binaryNumber[k - chars.Length + j] = chars[j];
                    }

                    //binaryNumber

                    for (int j = 0; j < binaryNumber.Length; j++)
                    {
                        if (binaryNumber[j] == '1')
                        {
                            string str = new string(binaryNumber);
                            str += ",";
                            binaryNumber[j] = '0';
                            str += new string(binaryNumber);
                            binaryNumber[j] = '1';
                            str += ",";
                            str += Math.Pow(2, k - 1 - j).ToString();
                            file.WriteLine(str);
                        }
                    }
                }
            }
        }
    }
}
