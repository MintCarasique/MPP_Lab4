using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OSHandle
{
    public abstract class OSHandle<T>: IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!AlreadyDisposed)
            {
                if (disposing)
                {
                    var disposable = Resource as IDisposable;
                    disposable?.Dispose();
                }

                if (Handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(Handle);
                    Handle = IntPtr.Zero;
                }
                AlreadyDisposed = true;
            }
        }

        public IntPtr Handle { get; set; }

        protected bool AlreadyDisposed { get; set; }

        public T Resource { get; set; }
    }
}
