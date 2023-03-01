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

        public Buffer(int bufferSize)
        {
            this._buffer = new byte[_bufferSize];
            this._currentPtr = 0;
            this._bufferSize = bufferSize;
        }

        public void WriteByte(byte value)
        {
            this._buffer[this._currentPtr] = value;
            this._currentPtr++;
        }
        
        public void WriteBytes(byte[] value, int arrSize)
        {
            for (int i = 0; i < arrSize; i++)
            {
                this.WriteByte(value[i]);
            }
        }


        public void WriteInt(int val) => this.WriteBytes(BitConverter.GetBytes(val), 4);
        
        public void WriteFloat(float val) => this.WriteBytes(BitConverter.GetBytes(val), 4);

        public void WriteVector3(Vector3 vector3)
        {
            this.WriteFloat(vector3.x);
            this.WriteFloat(vector3.y);
            this.WriteFloat(vector3.z);
        }
        
        public void WriteVector2(Vector2 vector2)
        {
            this.WriteFloat(vector2.x);
            this.WriteFloat(vector2.y);
        }
        
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

        public byte ReadByte()
        {
            byte val = this._buffer[this._currentPtr];
            this._currentPtr++;
            return val;
        }

        public byte[] ReadBytes(int size)
        {
            byte[] newBuffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                newBuffer[i] = this.ReadByte();
            }

            return newBuffer;
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(this.ReadBytes(4));
        }
        
        public float ReadFloat()
        {
            return BitConverter.ToSingle(this.ReadBytes(4));
        }

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

        public Vector2 ReadVector2()
        {
            float x = this.ReadFloat();
            float y = this.ReadFloat();
            return new Vector2(x, y);
        }

    }
}