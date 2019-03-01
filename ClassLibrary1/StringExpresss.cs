using System;
using System.Collections.Generic;
using System.Text;
using DynamicExpresso;

namespace ClassLibrary1
{
    public class StringExpresss
    {
        public static void Test1()
        {
            var target = new Interpreter().SetVariable("myVar", 23);

            //Assert.AreEqual(23, target.Eval("myVar"));
            Console.WriteLine(target.Eval("myVar + 1"));
            Console.WriteLine(target.Eval("(myVar)"));
            Console.WriteLine(target.Eval("(myVar) + \"aa\""));



            MyT my = new MyT();
            my.Str = "my.SSSSSSS";


            target.SetVariable("my", my);

            var sss = target.Eval("my.Str + myVar");

            Console.WriteLine(sss);
             

        }
    }


    class MyT
    {
        public string Str { get; set; }
    }
}
