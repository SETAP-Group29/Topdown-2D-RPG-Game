using System;
using UnityEngine;

namespace State
{
    /// <summary>
    /// A byte array wrappper to allow for easy serialization of data.
    /// </summary>
    public class Buffer
    {
        private byte[] _buffer;
        private int _currentPtr;
        private int _bufferSize;
        
        /// <summary>
        /// Creates a buffer with 1kb of size.
        /// </summary>
        public Buffer() : this(1024) {}

        /// <summary>
        /// Used to set the buffer to read from.
        /// Intended for read-mode.
        /// </summary>
        /// <param name="buffer">the pre-existing buffer</param>
        public Buffer(byte[] buffer)
        {
            _buffer = buffer;
            _bufferSize = buffer.Length;
            _currentPtr = 0;
        }

        private Buffer(int bufferSize)
        {
            this._buffer = new byte[_bufferSize];
            this._currentPtr = 0;
            this._bufferSize = bufferSize;
        }

        /// <summary>
        /// Writes byte to the buffer.
        /// </summary>
        /// <param name="value">the byte</param>
        /// <exception cref="BufferOverFlow">if the buffer goes over the defined limit.</exception>
        public void WriteByte(byte value)
        {
            if (this._currentPtr >= this._bufferSize)
            {
                throw new BufferOverFlow($"Attempted to write go over buffer (sz: {_bufferSize})");
            }

            this._buffer[this._currentPtr] = value;
            this._currentPtr++;
        }
        
        /// <summary>
        /// Write an array of bytes to the buffer
        /// </summary>
        /// <param name="value">the bytes to write</param>
        /// <param name="arrSize">the amount of bytes to write.</param>
        public void WriteBytes(byte[] value, int arrSize)
        {
            for (int i = 0; i < arrSize; i++)
            {
                this.WriteByte(value[i]);
            }
        }
        
        /// <summary>
        /// Write a little-endian encoded 4 byte array.
        /// </summary>
        /// <param name="val"></param>
        public void WriteInt(int val) => this.WriteBytes(BitConverter.GetBytes(val), 4);
        
        /// <summary>
        /// Writes a float to the buffer.
        /// </summary>
        /// <param name="val"></param>
        public void WriteFloat(float val) => this.WriteBytes(BitConverter.GetBytes(val), 4);

        /// <summary>
        /// Writes a Vector 3 to the buffer.
        /// </summary>
        /// <param name="vector3"></param>
        public void WriteVector3(Vector3 vector3)
        {
            this.WriteFloat(vector3.x);
            this.WriteFloat(vector3.y);
            this.WriteFloat(vector3.z);
        }
        
        /// <summary>
        /// Writes a Vector2 to the buffer.
        /// </summary>
        /// <param name="vector2"></param>
        
        public void WriteVector2(Vector2 vector2)
        {
            this.WriteFloat(vector2.x);
            this.WriteFloat(vector2.y);
        }
        
        /// <summary>
        /// Writes a string to the buffer.
        /// </summary>
        /// <param name="str"></param>
        public void WriteString(string str)
        {
            char[] chars = str.ToCharArray();
            byte[] strBuffer = new byte[str.Length + 1];
            this.WriteInt(str.Length);
            for (int idx = 0; idx < str.Length; idx++)
            {
                strBuffer[1 + idx] = (byte)chars[idx];
            }
        }

        /// <summary>
        /// Writes a boolean to the buffer.
        /// </summary>
        /// <param name="val"></param>
        public void WriteBoolean(bool val)
        {
            this.WriteInt(val ? 1: 0);
        }
        
        /// <summary>
        /// Reads a byte from the buffer.
        /// </summary>
        /// <returns> the byte read</returns>
        public byte ReadByte()
        {
            byte val = this._buffer[this._currentPtr];
            this._currentPtr++;
            return val;
        }

        /// <summary>
        /// Reads a set amount of bytes from the buffer
        /// </summary>
        /// <param name="size"> the amount of bytes to read.</param>
        /// <returns>the bytes read</returns>
        public byte[] ReadBytes(int size)
        {
            byte[] newBuffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                newBuffer[i] = this.ReadByte();
            }

            return newBuffer;
        }

        /// <summary>
        /// Reads an integer from the buffer.
        /// </summary>
        /// <returns>The integer read</returns>
        public int ReadInt()
        {
            return BitConverter.ToInt32(this.ReadBytes(4));
        }
        
        /// <summary>
        /// Reads a float from the buffer
        /// </summary>
        /// <returns>the float read</returns>
        public float ReadFloat()
        {
            return BitConverter.ToSingle(this.ReadBytes(4));
        }
        
        /// <summary>
        /// Reads a boolean from the buffer.
        /// </summary>
        /// <returns> the boolean read</returns>
        public bool ReadBoolean()
        {
            return this.ReadInt() == 1;
        }

        /// <summary>
        /// Reads a string from the buffer
        /// </summary>
        /// <returns>the string read</returns>
        public string ReadString()
        {
            string val = "" ;
            int strSize = this.ReadInt();
            for (int i = 0; i < strSize; i++)
            {
                val += (char)this.ReadByte();
            }

            return val;
        }

        /// <summary>
        /// Reads a vector2 from the buffer
        /// </summary>
        /// <returns>the vector2 read</returns>
        public Vector2 ReadVector2()
        {
            float x = this.ReadFloat();
            float y = this.ReadFloat();
            return new Vector2(x, y);
        }
        
        /// <summary>
        /// Reads a vector3 from the buffer
        /// </summary>
        /// <returns>the vector3 read</returns>
        public Vector3 ReadVector3()
        {
            float x = this.ReadFloat();
            float y = this.ReadFloat();
            float z = this.ReadFloat();
            return new Vector3(x, y, z);
        }
        
        /// <summary>
        /// Operator overloading that combines two buffers
        /// </summary>
        /// <param name="a">first buffer</param>
        /// <param name="b">second buffer</param>
        /// <returns> combined buffer </returns>
        public static Buffer operator + (Buffer a, Buffer b)
        {
            Buffer newBuffSize = new Buffer(a._currentPtr + b._currentPtr);
            newBuffSize.WriteBytes(a._buffer, a._currentPtr);
            newBuffSize.WriteBytes(b._buffer, b._currentPtr);
            return newBuffSize;
        }

        public byte[] GetBuffer () => _buffer;
    }
}