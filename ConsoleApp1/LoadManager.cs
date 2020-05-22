using System;
using System.IO;

namespace ConsoleApp1
{
    interface ILoadManager
    {
        string ReadLine();
        IReadbleObject Read(IReadableObjectLoader loader);
    }

    interface IReadbleObject
    { }

    interface IReadableObjectLoader
    {
        IReadbleObject Load(ILoadManager man);
    }
    class LoadManager : ILoadManager
    {
        FileInfo file;
        StreamReader input;
        public event EventHandler<IReadbleObject> ObjectDidLoad;
        public event EventHandler<FileInfo> DidEndLoad;
        public event EventHandler<FileInfo> DidStartLoad;

        public LoadManager(string filename)
        {
            file = new FileInfo(filename);
            input = null;
        }

        public IReadbleObject Read(IReadableObjectLoader loader)
        {
            var obj = loader.Load(this);
            ObjectDidLoad?.Invoke(this, obj);
            return obj;
        }

        public void BeginRead()
        {
            if (input != null)
                throw new IOException("Load Error");

            input = file.OpenText();
            DidStartLoad?.Invoke(this, file);
        }

        public bool IsLoading
        {
            get { return input != null && !input.EndOfStream; }
        }

        public string ReadLine()
        {
            if (input == null)
                throw new IOException("Load Error");

            string line = input.ReadLine();
            return line;
        }

        public void EndRead()
        {
            if (input == null)
                throw new IOException("Load Error");

            input.Close();
            DidEndLoad?.Invoke(this, file);
        }
    }
}