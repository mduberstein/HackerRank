using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicType
{
    class Program
    {
        // essence of this code is not clear
        static void Main(string[] args)
        {
            System.IO.Directory.CreateDirectory(@"Y:\TestFolder\TestSubFolder");
            Console.WriteLine(new ClassA().MethodA(0));
        }

        private void buttonClick(object sender, EventArgs e)
        {
            dynamic cs = new ClassA();
            cs.MethodA("XYZ");
        }
    }
    public class ClassA
    {
        public string MethodA(int val)
        {
            object count = null;
            //string s1 = count.ToString();
            string s2 = Convert.ToString(count);
            DayOff d = DayOff.Sat;
            d = (DayOff)5;
            return (val + d).ToString();
        }
    }

    enum DayOff { Sat = 1, Sun = 2 }
    public class ClassB
    {
        public int Count { get; set; } = 2;
    }

}
