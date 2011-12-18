using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Storage;

namespace Smiley.Lib.Framework
{
    public enum BitStreamMode
    {
        Read,
        Write
    }

    public class BitStream : IDisposable
    {
        #region Private Variables

        private BitStreamMode _mode;
        private int _numWritten;
        private int _numRead;
        Stream _stream;
        private byte _currentByte;
        private int _counter;
        private List<byte> _outBytes;

        #endregion

        #region Constructors

        public BitStream(StorageContainer storageContainer, string fileName, BitStreamMode mode)
        {
            if (mode == BitStreamMode.Read)
            {
                _stream = storageContainer.OpenFile(fileName, FileMode.Open);
            }
            else
            {
                _stream = storageContainer.OpenFile(fileName, FileMode.OpenOrCreate);
            }

            _outBytes = new List<byte>();
            _mode = mode;
            _numRead = 0;
            _counter = 0;
        }

        #endregion

        #region Public Methods

        public void Close()
        {
            if (_mode == BitStreamMode.Write)
            {
                while (!WriteBit(false)) ; // fill out the last byte if necessary
                _stream.Write(_outBytes.ToArray(), 0, _outBytes.Count);

            }
            _stream.Close();
        }

        /// <summary>
        /// Writes a single bit to the output stream. Returns true if this bit was the last
        /// bit in a byte.
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public bool WriteBit(bool bit)
        {

            if (_mode != BitStreamMode.Write) throw new Exception("Attempting to write to a stream that is opened in read mode!");

            if (bit)
            {
                _currentByte = Convert.ToByte(_currentByte | (byte)Math.Pow(2, 7 - _counter));
            }

            _counter++;
            _numWritten++;
            if (_counter > 7)
            {
                //End of this bit - add it to the output string and start a new byte
                _outBytes.Add(_currentByte);
                _currentByte = 0;
                _counter = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a single bit from the input stream.
        /// </summary>
        /// <returns></returns>
        public bool ReadBit()
        {

            if (_mode != BitStreamMode.Read) throw new Exception("Attemping to read from a stream that is opened in write mode!");

            if (_counter == 0)
            {
                //We are at the beginning of a new byte, so read one in from the input file
                _currentByte = (byte)_stream.ReadByte();
            }

            bool bit = Convert.ToBoolean(_currentByte & (byte)Math.Pow(2, 7 - _counter));
            _numRead++;

            _counter++;
            if (_counter > 7)
            {
                _counter = 0;
            }

            return bit;
        }

        /// <summary>
        /// Writes a byte to the output stream. The byte should be passed in as an integer from 0-255.
        /// </summary>
        /// <param name="b"></param>
        public void WriteByte(int b)
        {
            if (b > byte.MaxValue) throw new Exception("Value too large to write as a byte!");

            WriteBits(b, 8);
        }

        /// <summary>
        /// Reads a byte from the input stream.
        /// </summary>
        /// <returns></returns>
        public int ReadByte()
        {
            return ReadBits(8);
        }

        /// <summary>
        /// Writes bits to the output stream.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="numBits"></param>
        public void WriteBits(long data, int numBits)
        {
            if (_mode != BitStreamMode.Write) throw new Exception("Attempting to write to a file that is opened in read mode!");

            for (int i = numBits - 1; i >= 0; i--)
            {
                if (data >= Math.Pow(2, i))
                {
                    WriteBit(true);
                    data -= (int)Math.Pow(2, i);
                }
                else
                {
                    WriteBit(false);
                }
            }
        }

        /// <summary>
        /// Reads a byte from the input stream.
        /// </summary>
        /// <param name="numBits"></param>
        /// <returns></returns>
        public int ReadBits(int numBits)
        {
            if (_mode != BitStreamMode.Read) throw new Exception("Attemping to read from a stream that is opened in write mode!");

            int data = 0;
            for (int i = numBits - 1; i >= 0; i--)
            {
                if (ReadBit())
                {
                    data += (int)Math.Pow(2, i);
                }
            }
            return data;
        }

        /// <summary>
        /// Reads a float from the stream.
        /// </summary>
        /// <returns></returns>
        public float ReadFloat()
        {
            if (_mode != BitStreamMode.Read) throw new Exception("Attemping to read from a stream that is opened in write mode!");

            byte[] bytes = new byte[4];
            bytes[0] = (byte)ReadByte();
            bytes[1] = (byte)ReadByte();
            bytes[2] = (byte)ReadByte();
            bytes[3] = (byte)ReadByte();

            return BitConverter.ToSingle(bytes, 0);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
