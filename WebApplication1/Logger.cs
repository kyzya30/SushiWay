using System;

namespace WebApplication1
{
    public class Logger
    {
        public enum MessageType
        {
            Warn,
            Error,
            Info
        }
        private static object block = new Object();
        private static Logger instance;

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (block)
                    {
                        if (instance == null)
                        {
                            instance = new Logger();
                        }
                    }
                }
                return instance;
            }
        }

        public static void Write(LoggerExeption ex, MessageType messageType = MessageType.Info) { }
        public static void Write(LoggerExeption ex, string message, MessageType messageType = MessageType.Info) { }
        public static void Write(string message, MessageType messageType = MessageType.Info) { }
    }

    public class LoggerExeption : Exception
    {
        
    }
}