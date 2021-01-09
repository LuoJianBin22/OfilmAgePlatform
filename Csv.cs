using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Jim.IO
{
    public class Csv
    {
        public static void Write(string filepath, string strMsg, bool append = true)
        {
            if (!File.Exists(filepath))
            {
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);//如果文件不存在，则创建文件
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(filepath, append, Encoding.GetEncoding("gb2312"));
            sw.WriteLine(strMsg);
            sw.Flush();
            sw.Close();
        }

        public static void Write(string filepath, string strMsg, bool append, char Splitchar = ',')
        {
            if (!File.Exists(filepath))
            {
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);//如果文件不存在，则创建文件
                fs.Close();
            }

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Clear();
            string[] strArray = strMsg.Split(Splitchar);
            if (strArray.Length > 0)
            {
                foreach (string str in strArray)
                {
                    strBuilder.Append(str + ",");
                }
            }

            StreamWriter sw = new StreamWriter(filepath, append, Encoding.GetEncoding("gb2312"));
            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        //write a file, existed file will be overwritten if append = false  
        public static void Write(string filePathName, List<string[]> ls, bool append = true)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
            foreach (string[] strArr in ls)
            {
                fileWriter.WriteLine(string.Join(",", strArr));
            }
            fileWriter.Flush();
            fileWriter.Close();

        }

        public static List<string[]> Read(string filePathName)
        {
            List<string[]> ls = new List<string[]>();
            StreamReader fileReader = new StreamReader(filePathName, Encoding.Default);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                }
            }
            fileReader.Close();
            return ls;
        }

        public static List<string> Read2(string filePathName)
        {
            List<string> ls = new List<string>();
            StreamReader fileReader = new StreamReader(filePathName, Encoding.Default);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine);
                }
            }
            fileReader.Close();
            return ls;
        }

        /// 将CSV文件的数据读取到DataTable中
        public static DataTable Open(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }

    }
}
