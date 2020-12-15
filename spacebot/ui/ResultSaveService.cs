using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace spacebot.ui
{
    public class ResultSaveService
    {
        static readonly string FileName = "results.txt";
        FileStream file;
        List<UserResultDto> userResults;

        public List<UserResultDto> GetUserResults()
        {
            return userResults;
        }

        public ResultSaveService()
        {
            file = File.Open(FileName, FileMode.OpenOrCreate);
            byte[] arr = new byte[65535];
            file.Read(arr, 0, 65535);

            string data = Encoding.ASCII.GetString(arr);

            userResults = JsonConvert.DeserializeObject<List<UserResultDto>>(data);
            if(userResults == null)
            {
                userResults = new List<UserResultDto>();
            }
            file.Close();
        }

        public void AddResult(UserResultDto result)
        {
            result.datetime = DateTime.Now.ToShortDateString();

            userResults.Add(result);
            string serializedData = JsonConvert.SerializeObject(userResults);

            var outstream = File.Open(FileName, FileMode.OpenOrCreate);
            outstream.Seek(0, SeekOrigin.End);
            byte[] temp = Encoding.ASCII.GetBytes(serializedData);
            int count = temp.Length;
            outstream.SetLength(0);
            outstream.Write(temp, 0, count);
            outstream.Close();
        }
    }
}
