using System.IO;
using System.Text;

namespace K_Means.Models
{
    public class Input
    {
        public double[] InputData { get; set; }

        private Input()
        {
            //
        }

        public static void CreateInput(
            string input,
            string path = @"E:\Programming\C_Sharp\K-Means\Additional\input.txt")
        {
            byte[] bytesToWrite = Encoding.ASCII.GetBytes(input);
            using (FileStream fileStream = new FileStream(
                path,
                FileMode.OpenOrCreate))
            {
                fileStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            }

        }
        public static string ReadInput(
            string path = @"E:\Programming\C_Sharp\K-Means\Additional\input.txt")
        {
            return File.ReadAllText(path);
        }
    }


}
