using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Extra
{
    public static class AsyncRunner
    {
        //public static void RunAsync<T, U>(Func<Task<T>> task, ref U res)
        //{
        //    object obj = null;
        //    //Task.Run(async () => obj = await task.Invoke());
        //    Task.Run(async () => obj = await task.Invoke()).Wait();
        //    if(obj != null)
        //        res = (U)obj;

        //    //object obj = null;
        //    //T t = func.Invoke();

        //    //Task.Run(async () => obj = await ).Wait();

        //    //return (T)obj;
        //}

        //public static void RunAsync<T, U>(Func<Task<T>> task, Func<U, object> func)
        //{
        //    var resTask = task.Invoke();
        //    resTask.ContinueWith((res) => func.Invoke((U)res.Result));

        //}

        public static void RunAsync<T>(Func<Task<T>> task, Action<T> callback)
        {
            Task.Run(async () => await task.Invoke().ContinueWith((res) => callback.Invoke(res.Result)));
            //var resTask = task.Invoke();
            //resTask;

        }

        public static Task<Task> RunTaskAsync<T>(Func<Task<T>> func, Action<T> callback)
        {
            var temp = func.Invoke().ContinueWith((res) =>
            {
                return Task.Run(() => callback.Invoke(res.Result));
            });

            //var res = temp.Result;
            //var tempId = temp.Id;
            //var resId = res.Id;

            return temp;
                //.ContinueWith((_) => 
                //MessageBox.Show($"{_.Id}"));

            //return Task.Run(async () =>
            //{
            //    await func.Invoke().ContinueWith((res) => callback.Invoke(res.Result));
            //    //var t = ;
            //    //return ;
            //});

        }

        public static Task<Task> ContinueAsync<T>(this Task<Task> task, Func<Task<T>> func, Action<T> callback)
        {
            //task -> CallBack Method
            return task.ContinueWith((temp) =>
            {
                var taskId = task.Id;
                var id = temp.Id;

                var t = func.Invoke().ContinueWith((res) =>
                //    callback.Invoke(res.Result));
                {
                    //var resId = res.Id;
                    var tRes = Task.Run(() => callback.Invoke(res.Result));
                    //var tId = tRes.Id;
                    return tRes;
                });

                return t.Result;
            });
            //return Task.Run(async () => 
            //{
            //    await task.ContinueWith( async (temp) => 
            //        await func.Invoke().ContinueWith((res) => callback.Invoke(res.Result)));

            //    //await task.ContinueWith( 
            //    //    async (temp) => await func.Invoke())
            //    //            .ContinueWith((res) => callback.Invoke(res.Result));
            //});

        }

        public static Task ContinueAsync(this Task task, Func<Task> funcTask)
        {
            return Task.Run(async () =>
            {
                await task.ContinueWith(async (temp) =>
                   await funcTask.Invoke());

                //await task.ContinueWith( 
                //    async (temp) => await func.Invoke())
                //            .ContinueWith((res) => callback.Invoke(res.Result));
            });
            //var resTask = task.Invoke();
            //resTask;

        }
    }
}
