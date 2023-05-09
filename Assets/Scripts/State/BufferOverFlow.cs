using System;

namespace State
{
    /// <summary>
    /// Used to represent a buffer overflow exception.
    /// </summary>
    public class BufferOverFlow : Exception
    {
        public BufferOverFlow(string message) : base(message)
        {
        }
    }
}