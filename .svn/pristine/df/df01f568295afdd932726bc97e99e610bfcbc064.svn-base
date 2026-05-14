namespace DCSoft
{
    /// <summary>
    /// 二进制数据比较器
    /// </summary>
    internal class BinaryEqualityComparer : System.Collections.Generic.IEqualityComparer<byte[]>
    {
        public BinaryEqualityComparer()
        {

        }
        public BinaryEqualityComparer(bool useNativeGetHashCode)
        {
            this._UseNativeGetHashCode = useNativeGetHashCode;
        }
        private bool _UseNativeGetHashCode = false;

        public bool Equals(byte[] x, byte[] y)
        {
            if (x == y)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            if (x.Length != y.Length)
            {
                return false;
            }
            if (x.Length > 0)
            {
                for (int iCount = x.Length - 1; iCount >= 0; iCount--)
                {
                    if (x[iCount] != y[iCount])
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public unsafe int GetHashCode(byte[] obj)
        {
            if (obj == null || obj.Length == 0)
            {
                return 0;
            }
            if (this._UseNativeGetHashCode)
            {
                return obj.GetHashCode();
            }
            fixed (byte* pStart = obj)
            {
                byte* p = pStart + obj.Length - 1;
                int num = 5381;
                while (p >= pStart)
                {
                    num = ((num << 5) + num) ^ *p;
                    p--;
                }
                return num;
            }
        }
    }
}
