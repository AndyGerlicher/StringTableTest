using System;
using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.ContractsLight
{
    public static class Contract
    {
        public static void Assume(bool action, string str = "") { }
        //public static void Assert(Action<bool> action){ }
        public static void Assert(bool action, string str = "") { }
        public static void Requires(bool foo, string str = "") { }
        public static void RequiresForAll<T>(IEnumerable<T> collection, Func<T, bool> func) { }
        public static void Ensures(bool foo) { }

        public static ContractResult Result<T>()
        {
            return _result;
        }

        public static ContractResult ValueAtReturn<T>(out T value)
        {
            value = (default(T));
            return _result;
        }


        private static readonly ContractResult _result = new ContractResult();
    }

    
    public class ContractResult { public bool IsValid => true; }

    public class ContractVerification : Attribute
    {
        public ContractVerification()
        { }

        public ContractVerification(bool something)
        { }
    }

    public class Pure : Attribute
    {
    }
}
