namespace CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* learn 1 */

            // frequently used data type
            // int
            // float
            // string
            // bool

            // [data type] [name];

            /* learn 2 */

            // declaration variable
            int hp;

            // initialization variable
            hp = 100;

            // declaration & initialization variable
            int mp = 1000;

            // if we set the max HP of character, simply setting it to a "fixed value" may not be the right approach
            // ex) int maxHp = 100;
            // because if the value of "hp" changes, we would have to update "maxHp" every time as well
            // if we initialize "maxHp" with the value of "hp", then even if the initial value of "hp" changes later, we don't need to modify "maxHp" separately
            int maxHp = hp;

            // read variable
            Console.WriteLine(hp);

            /* learn 3 */

            // decimal(10) : 0 1 2 3 4 5 6 7 8 9     10 11 12 ..
            // binary(2)hexadecimal(16) : 0b00 0b01 0b10 0b11 0b100 0b101 ..
            // hexadecimal(16) : 0 1 2 3 4 5 6 7 8 9 A B C D E F

            int count = 100;
            count = 0x64;
            count = 0b01100100;

            /* learn 4 */

            // Even if we store values in decimal likes "int hp = 100;"
            // the computer actually saves them in "binary"
            // which is the mose efficient form

            // At this point, whether the binary value is interpreted as "unsigned or signed" makes as difference:
            // in unsigned representation the most significant bit contributes to the largest value,
            // unsigned 8bit integer "byte" represent : 0 to 255

            // while in signed representation (using two;s complement) the most significant bit represents the "sign",
            // which can make it the smallest possible value
            //signed 16bit integer "short" represent : 0 to 65,535

            /* learn 5 */
            // if write "cw" and input "tab", then it replace to "Console.WriteLine();"
            Console.WriteLine();

            /* learn 6 */
            // When converting from a larger data type to "a smaller one",
            // an "explicit cast" is required and data loss may occur
            int largerValue = 300;
            short smallValue = (short)largerValue; // must cast "explicit"
            Console.WriteLine(smallValue); // may not hold same value as 300

            // Conversely, when converting from a smaller type to a larger one,
            // an "implicit cast" is allowed and no data loss happens
            short s = 100;
            int i = s; // implicit conversion, no cast needed => int i = (i) s;
            Console.WriteLine(i);

            /* learn 7 */
            // if you want to get data from user, then can read data from user input
            string input = Console.ReadLine();
            Console.WriteLine(input);

            // how can cast user's string input to "int type"? like "100"
            int num = int.Parse(input); // parsing "input" to int type

            // if you input "not int type data", then CW will return system format exception
            Console.WriteLine(num);

            // then we can print int type of user hp data to string output
            string message = "your HP is ";
            Console.WriteLine(message);

            // can format int type data to string (past version)
            string formattingMessage = string.Format("your HP is {0}/{1}", hp, maxHp);
            Console.WriteLine(formattingMessage);

            // / can format int type data to string (now version)
            string formattingMessage2 = $"your HP is {hp}/{maxHp}";
            Console.WriteLine(formattingMessage2);

            /* learn 8 */
            // logical operators in C#
            bool a = true;
            bool b = false;

            Console.WriteLine(a && b); // AND
            Console.WriteLine(a || b); // OR
            Console.WriteLine(!a); // NOT

            /* learn 9 */
            // In C#, "var" allows the compiler to "infer the type" of local variable from its initializer

            // it is still statically typed, and the type cannot change after initialization
            // => it means var type will be cast to specific type by compiler
            // => and Once the type is determined, it cannot be changed
            var aa = 10; // == int aa = 10;
            var bb = 4.1; // == float bb = 4.1;
            var cc = "cc"; // == string cc = "cc";
            var dd = true; // == bool dd = true;


        }
    }
}
