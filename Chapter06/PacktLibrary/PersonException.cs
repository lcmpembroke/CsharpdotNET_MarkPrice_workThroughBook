using System;

namespace Packt.Shared
{
    [Serializable]
    public class PersonException : Exception
    {
        public PersonException() { }
        
        public PersonException(string message) : base(message) { }
        
        public PersonException(string message, Exception inner) : base(message, inner) { }

        // protected PersonException(
        //     System.Runtime.Serialization.SerializationInfo info,
        //     System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class MyException : System.Exception
    {
        public MyException() { }
        public MyException(string message) : base(message) { }
        public MyException(string message, System.Exception inner) : base(message, inner) { }
        protected MyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }                                                                                
}