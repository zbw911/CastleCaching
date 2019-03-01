using System.Collections.Generic;
using System.Linq;
using DynamicExpresso;

namespace Comm.InterceptorCaching.Interceptor
{
    internal class DynamicExparser
    {
        private readonly string _key;
        private readonly IEnumerable<string> _paramnames;
        private readonly object[] _args;

        public DynamicExparser(string key, IEnumerable<string> paramnames, object[] args)
        {
            _key = key;
            _paramnames = paramnames;
            _args = args;
        }

        public T Parser<T>()
        {

            var target = new Interpreter();

            if (_paramnames.Any())
            {
                for (int i = 0; i < _paramnames.Count(); i++)
                {
                    target.SetVariable(_paramnames.ElementAt(i), _args[i]);
                }
            }


            return (T)target.Eval(_key);

        }
    }
}