using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecureCommunicationComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectOperation c = new ObjectOperation();
            c.Dispose();

            try
            {
                c.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            //wrap the ObjectOperation instance that implements dispose inside using.
            using (ObjectOperation c1 = new ObjectOperation())
            {
                //Console.WriteLine(1);
            }
            //Console.WriteLine(2);
            
        }
    }


    class MutexPool
    {
        private static IList<Mutex> _muList;

        private MutexPool()
        {
            // Load list of available Mutex objects
            _muList = new List<Mutex>
            {
                    new Mutex(),
                    new Mutex(),
                    new Mutex(),
                    new Mutex(),
                    new Mutex()
            };
        }

        public static MutexPool GetMutex()
        {
            MutexPool _muList = new MutexPool();
            return _muList;
        }


        public static class PoolWrapper
        {
            public static Mutex GetObject()
            {
                Mutex myObjects = (Mutex)_muList;
                if (myObjects is ICollection)
                {
                    var count = ((ICollection)myObjects).Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (i <= _muList.Count)
                        {
                            var myObject = _muList[i];
                            _muList.Remove(myObject);
                        }

                        else if (i > _muList.Count)
                        {
                            var myObject = _muList[i];
                            _muList.Add(myObject);
                        }

                        else if (i > 10)
                        {
                            var myObject = _muList[i];
                            myObject = null;
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                            try
                            {
                                Mutex flag = new Mutex();
                                if (flag.IsAcquired == true)
                                {
                                    WeakReference objectRef = new WeakReference(myObject);
                                    if (objectRef.IsAlive == true)
                                    { return myObject; }

                                }

                            }
                            catch (Exception)
                            {

                                throw new Exception("Garbage Collection failed to recover any objects :");
                            }
                        }
                    }
                }
                return null;
            }

            public static void ReturnObject(Mutex myMutex)
            {
                //check if already existing
                if (!_muList.Contains(myMutex))
                {
                    //add the mutex object back to the list
                    _muList.Add(myMutex);
                    //I wasn't sure if I should do this or set myMutex to null
                }
            }
        }
    }

    class ObjectOperation : IDisposable
    {
        Mutex _myObject;

        // to detect redundant calls
        private bool disposed = false;
        public bool _isDisposed
        {
            get
            {
                return disposed;
            }
        }

        public ObjectOperation()
        {
            //Get an object from the list and keep it alive until the end of an app
            _myObject = MutexPool.PoolWrapper.GetObject();
            GC.KeepAlive(_myObject);
        }
        
        private void CleanUp(bool disposing)
        {
            if (_isDisposed == true)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    this.Dispose();
                    throw new ObjectDisposedException("Object is already disposed");

                }

                MutexPool.PoolWrapper.ReturnObject(_myObject);
            }

            // Dispose of unmanaged resources.
            // object will be cleaned up by the Dispose method.
            disposed = true;
            // take this object off the finalization queue so as not to execute again
            GC.SuppressFinalize(this);
        }


        public void Dispose()
        {
            //Console.WriteLine(0);

            try
            {

                disposed = true;
                GC.SuppressFinalize(this);
            }
            catch
            {
                throw new NotImplementedException();

            }
        }


        ~ObjectOperation()
        {
            try
            {
                CleanUp(false);
            }
            catch { }
        }
    }
    class Mutex
    {
        public int Identifier { get; set; }
        public bool IsAcquired { get; set; }
    }
}
