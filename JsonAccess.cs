using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace OfilmAgePlatform
{
    public class JsonAccess
    {

        public static void Write<T>(string filePath, T obj)
        {
            int index = filePath.LastIndexOf("\\");
            string folderPath = filePath.Substring(0, index);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string jsonString = ToJson<T>(obj);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonString);
                }
            }

        }

        public static T Read<T>(string filePath)
        {
            try
            {
                int index = filePath.LastIndexOf("\\");
                string folderPath = filePath.Substring(0, index);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Json反序列化异常：\r\n{ ex.Message}\r\n\r文件路径：\r\n{filePath}");
                return default(T);
            }
        }

        //using Newtonsoft.Json
        /// <summary>
        /// JsonConvert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// JsonConvert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        static T ToObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }




        //使用System.Web.Script.Serialization进行序列化和反序列化，不过在VS中需要添加引用System.Web.Script.Serialization的时候，请先引用System.Web.Extensions。
        /// <summary>
        /// json序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static string JsonSerializer<T>(T t)
        //{
        //    JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
        //    return jsonSerialize.Serialize(t);
        //}

        /// <summary>
        /// json反序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        //public static T JsonDeserialize<T>(string jsonString)
        //{
        //    JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
        //    return (T)jsonSerialize.Deserialize<T>(jsonString);
        //}



        //使用System.Runtime.Serialization.Json命名空间下的DataContractJsonSerializer类进行json的序列化和反序列化，
        //该方法使用的二进制的方式来序列化和反序列化，使用该类方法时需要在对应的实体类中有相应的标识（如：[DataContract] [DataMember(Name = "")]）在下面的调用时有相关说明。
        /// <summary>
        /// JSON序列化(二进制方式，实体类使用[Serializable])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static string JsonSerializerIO<T>(T t)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ser.WriteObject(ms, t);
        //        string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //        ms.Close();
        //        return jsonString;
        //    }
        //}

        /// <summary>
        /// JSON反序列化(二进制方法，实体类使用[Serializable])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        //public static T JsonDeserializeIO<T>(string jsonString)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //    {
        //        T obj = (T)ser.ReadObject(ms);
        //        return obj;
        //    }
        //}


    }
}
