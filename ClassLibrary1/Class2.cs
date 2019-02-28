using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace ClassLibrary1
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
       
        int Go(int input);
    }

    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[CacheIt]
        public int Go(int input)
        {

            return input;
        }
    }


}
