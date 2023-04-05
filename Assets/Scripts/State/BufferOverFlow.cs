using System;

namespace State
{
    public class BufferOverFlow : Exception
    {
        public BufferOverFlow(string message) : base(message)
        {
        }
    }
}