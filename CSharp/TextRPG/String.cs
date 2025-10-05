using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    class String
    {
        static void Main(string[] args)
        {
            string name = "iron man";

            // 1. 문자열 찾기
            bool found = name.Contains("man"); // 문자열 포함 여부
            int index = name.IndexOf("n"); // 특정 문자열의 위치 인덱스

            // 2. 문자열 변형
            name = "king " + name;
            name.ToLower(); // 전부 소문자로
            name.ToUpper(); // 전부 대문자로
            name.Replace('n', 'l'); // 특정 문자를 특정 문자로 변경

            // 3. 분할
            string[] names = name.Split(new char[] { ' ' }); // 이름을 ' '공백을 통해 구분하여 string[] 배열에 저장함
            // Console.WriteLine(names[0]);
            string substringName = name.Substring(4); // 특정 인덱스부터 잘라서 반환
        }
    }
}
