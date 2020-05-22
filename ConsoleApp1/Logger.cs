using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp1;

namespace ConsoleApp1
{
    public class Logger
    {
        protected TextWriter output;

        public Logger(TextWriter output)
        {
            this.output = output;
        }

        public virtual void WriteLine(string message)
        {
            output.WriteLine(message);
        }

        public void Close()
        {
            output.Close();
        }
    }

    class LoadLogger
    {
        LoadManager loadManager;
        Logger logger;

        public LoadLogger(LoadManager loadManager, Logger logger)
        {
            this.loadManager = loadManager;
            this.logger = logger;
            loadManager.DidStartLoad += LoadStarted;
            loadManager.DidEndLoad += LoadFinished;
            loadManager.ObjectDidLoad += ObjectLoaded;
        }

        private void LoadStarted(object sender, FileInfo e)
        {
            logger.WriteLine($"{DateTime.Now.ToString()} Started loading from: {e.FullName}");
        }

        private void ObjectLoaded(object sender, IReadbleObject e)
        {
            logger.WriteLine($"{DateTime.Now.ToString()} Object loaded: {e.ToString()}");
        }

        private void LoadFinished(object sender, FileInfo e)
        {
            logger.WriteLine($"{DateTime.Now.ToString()} Loading from {e.FullName} finished.");
            logger.Close();
        }
    }
}
