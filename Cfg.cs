
using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace ImageGalleryListView
{
    public static class Cfg
    {
        // C의 dll함수 마샬링	
        // 함수의 파라미터는 section명, 키명, 값, 파일 위치	
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        // C의 dll함수 마샬링	
        // 함수의 파라미터는 section명, 키명, 디폴트 값(값이 없을 때, 나오는 값), String pointer, 크기, 파일 위치	
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // ini파일에 값을 작성하는 함수	
        public static void WriteConfig(string file, string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, GetFile(file));
        }
        // ini파일에서 값을 가져오는 함수	
        public static string ReadConfig(string file, string section, string key)
        {
            // C#에서는 포인터를 명시적으로 표현할 수 없기 때문 StringBuilder로 가져옵니다.	
            StringBuilder temp = new StringBuilder(255);
            int ret = GetPrivateProfileString(section, key, null, temp, 255, GetFile(file));
            return temp.ToString();
        }
        private static string GetFile(string file)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file + ".ini");
        }
    }
    /*class ProgramEx
    {
        static void Main(string[] args)
        {
            //config.ini 파일에 SectionTest 세션의 data키로 Hello world가 저장된다.	
            Cfg.WriteConfig("config", "SectionTest", "data", "Hello world");

            //config.ini 파일에 SectionTest 세션의 키가 data의 값을 가져온다.	
            Console.WriteLine(Cfg.ReadConfig("config", "SectionTest", "data"));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }*/
}
