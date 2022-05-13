using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UI.Extra
{
    public static class AsyncRunner
    {
        public static void RunAsync<T, U>(Func<Task<T>> task, ref U res)
        {
            object obj = null;
            Task.Run(async () => obj = await task.Invoke()).Wait();
            if(obj != null)
                res = (U)obj;

            //object obj = null;
            //T t = func.Invoke();

            //Task.Run(async () => obj = await ).Wait();

            //return (T)obj;
        }
    }
}
