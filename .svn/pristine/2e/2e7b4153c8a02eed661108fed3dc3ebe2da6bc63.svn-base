using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DCSoft.Common
{
    /// <summary>
    /// 快速的写数据的流
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class FastMemoryStreamForWrite : System.IO.Stream
    {
        private byte[] _Buffer = null;
        private long _Length = 0;
        private long _Capacity = 0;
        private long _Position = 0;
        public FastMemoryStreamForWrite()
        {
            _Buffer = new byte[1024];
            this._Capacity = _Buffer.Length;
        }
        public FastMemoryStreamForWrite( int capacity)
        {
            if(capacity <= 0 )
            {
                throw new ArgumentOutOfRangeException("capacity=" + capacity);
            }
            _Buffer = new byte[capacity];
            this._Capacity = capacity;
        }

        public void CheckCapacity(long size)
        {
            if (size > this._Capacity)
            {
                var bs = new byte[(int)(size * 1.5)];
                if (this._Length > 0)
                {
                    Buffer.BlockCopy(this._Buffer, 0, bs, 0, (int)this._Length);
                }
                this._Buffer = bs;
                this._Capacity = bs.Length;
            }
        }
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        public override bool CanSeek
        {
            get
            {
                return true ;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return this._Length;
            }
        }

        /// <summary>
        /// 当前位置
        /// </summary>
        public override long Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                _Position = value;
            }
        }
        /// <summary>
        /// 无意义的操作
        /// </summary>
        public override void Flush()
        {
        }
        /// <summary>
        /// 移动当前位置
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="origin">移动方式</param>
        /// <returns>新的位置</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long result = 0;
            switch (origin)
            {
                case SeekOrigin.Begin: result = offset; break;
                case SeekOrigin.Current: result = this._Position + offset; break;
                case SeekOrigin.End: result = this._Length + offset; break;
                default: result = this._Position + offset; break;
            }
            if (result < 0)
            {
                result = 0;
            }
            if (result > this._Length)
            {
                result = this._Length ;
            }
            this._Position = result;
            return result;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
         
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            CheckCapacity(this._Position + count);
            Buffer.BlockCopy(buffer, offset , this._Buffer , (int)this._Position , count);
            this._Position += count;
            if(this._Length < this._Position)
            {
                this._Length = this._Position;
            }
        }
        public override void WriteByte(byte value)
        {
            CheckCapacity(this._Position + 1);
            this._Buffer[this._Position] = value;
            this._Position++;
            if (this._Length < this._Position)
            {
                this._Length = this._Position;
            }
        }

        public override void Close()
        {
            
        }

#if !DCWriterForWASM
        public byte[] ToArray()
        {
            if(this._Length == this._Capacity)
            {
                return this._Buffer;
            }
            var bs = new byte[this._Length];
            Buffer.BlockCopy(this._Buffer, 0, bs, 0, (int)this._Length);
            return bs;
        }
        public void WriteTo( System.IO.Stream stream )
        {
            if( stream == null )
            {
                throw new ArgumentNullException("stream");
            }
            if( this._Length > 0 )
            {
                stream.Write(this._Buffer, 0, (int)this._Length);
            }
        }
        public void WriteToFile( string fileName )
        {
            if( fileName == null || fileName.Length == 0 )
            {
                throw new ArgumentNullException("fileName");
            }
            using (var stream = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                stream.Write(this._Buffer, 0, (int)this._Length);
            }
        }
#endif
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this._Buffer = null;
            this._Position = 0;
            this._Length = 0;
            this._Capacity = 0;
        }
    }
}
