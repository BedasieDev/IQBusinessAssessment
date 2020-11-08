using System;

namespace SendReceiveLib.Models
{
    public abstract class BasicCommand
    {
        protected T Execute<T>(Func<T> func)
        {
            return func();
        }
    }
}
