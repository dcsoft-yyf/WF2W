using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 表示默认颜色值的特性
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class DefaultColorValueAttribute : System.ComponentModel.DefaultValueAttribute
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultColorValueAttribute( ) : base( Color.Empty )
        {

        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="v">字符串颜色值</param>
        public DefaultColorValueAttribute( string v ):base( DCSoft.Common.XMLSerializeHelper.StringToColor( v , Color.Empty))
        {

        }
        /// <summary>
        /// 初始化值
        /// </summary>
        /// <param name="a">颜色A值</param>
        /// <param name="r">颜色R值</param>
        /// <param name="g">颜色G值</param>
        /// <param name="b">颜色B值</param>
        public DefaultColorValueAttribute( int a , int r , int g , int b ):base( Color.FromArgb( a , r, g , b ))
        {

        }
        /// <summary>
        /// 初始化值
        /// </summary>
        /// <param name="argb">颜色ARGB值</param>
        public DefaultColorValueAttribute( int argb ):base(Color.FromArgb( argb ))
        {

        }
    }
}
