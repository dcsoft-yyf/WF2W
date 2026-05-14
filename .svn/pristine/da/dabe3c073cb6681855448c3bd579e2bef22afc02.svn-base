using System;
using System.Collections.Generic;

namespace DCSoft
{
    internal static class Win32ObjectTable
    {
        private static readonly Dictionary<int, object> _ObjectTable = new Dictionary<int, object>();
        public static void SetValue(int key, object value)
        {
            _ObjectTable[key] = value;
        }
        public static object GetValue(int key, object defaultValue = null )
        {
            if( _ObjectTable.TryGetValue(key, out var value))
            {
                return value;
            }
            return defaultValue;
        }
        public static bool TryGetValue(int key, out object value)
        {
            return _ObjectTable.TryGetValue(key, out value);
        }
        public static void Remove(int key)
        {
            _ObjectTable.Remove(key);
        }
    }
}
