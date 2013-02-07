using System;
using System.Threading.Tasks;

namespace Gecko.NCore.Client
{
    internal class AsyncHelper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        internal static Task WrapAsTask(Action action)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            try
            {
                action();
                taskCompletionSource.SetResult(new object());
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
            return taskCompletionSource.Task;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        internal static Task<TResult> WrapAsTask<TResult>(Func<TResult> func)
        {
            var taskCompletionSource = new TaskCompletionSource<TResult>();
            try
            {
                var result = func();
                taskCompletionSource.SetResult(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
            return taskCompletionSource.Task;
        }

    }
}
