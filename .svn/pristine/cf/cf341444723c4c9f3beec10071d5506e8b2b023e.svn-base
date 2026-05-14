//------------------------------------------------------------------------------
// <copyright file="Brush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing 
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable 
    {
        static Brush()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }
        public abstract object Clone();
       
         

        internal void SetNativeBrushInternal(IntPtr brush)
        {
            throw new NotSupportedException();
        }

    
        internal IntPtr NativeBrush
        { 
            get
            {
                throw new NotSupportedException();
            }
        }

        public virtual void Dispose() 
        {
        }

        internal virtual bool EqualsValue( Brush b )
        {
            return true ;
        }
    }
}
