using System;
using System.Text.Json.Nodes;
using System.Drawing;

namespace System.Windows.Forms
{
    public partial class Control
    {
        internal virtual void WriteAdditionJsonForCreateControl( JsonObject json )
        {

        }
        protected TResult InvokeJSMethod<TResult>( string strMethodName , JsonNode args = null)
        {
            if( this.IsHandleCreated )
            {
                return DCWin32API.JSRuntime.Invoke<TResult>("__DCWin32API.InvokeControlMethod", this.Handle.ToInt32(), strMethodName, args);
            }
            return default( TResult );
        }
        protected void LogWF2WPropertyValue(string name, bool value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, string value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, Color value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, int value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, float value) { PropertyValueLogger.Log(this ,name,value); }
        protected void LogWF2WPropertyValue(string name, DateTime value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, double value) {PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, Image value) {  PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, Enum value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, JsonNode value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, Size value) { PropertyValueLogger.Log(this, name, value); }
        protected void LogWF2WPropertyValue(string name, Padding value) { PropertyValueLogger.Log(this, name, value); }

        internal void WF2WRedraw(Rectangle rect)
        {
            var msg = new Message();
            msg.ObjectLParam = rect;
            this.WmPaint(ref msg);
        }
        /// <summary>
        /// 设置控件启用用户自定义绘制
        /// </summary>
        /// <param name="value">是否自定义绘制</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void WF2WSetUserPaint(bool value)
        {
            this.SetStyle(ControlStyles.UserPaint, value);
        }
        internal virtual Point GetAutoScrollPositionForPaint()
        {
            return Point.Empty;
        }
        internal bool IsUserPaint()
        {
            return this.GetStyle(ControlStyles.UserPaint);
        }

        //public virtual void WriteAttributeTo(JsonObject json)
        //{
        //    if (this.GetStyle(ControlStyles.UserPaint))
        //    {
        //        json["UserPaint"] = true;
        //    }

        //    //json["DCOwnerDrawMode"] = this.DCOwnerDrawMode.ToString();
        //    //if( this.DCOwnerDrawMode == DCOwnerDrawModeConsts.OwnerDraw || this.DCOwnerDrawMode == DCOwnerDrawModeConsts.OwnerDrawWithScroll)
        //    //{
        //    //    json["IsOwnerDraw"] = true;
        //    //}
        //    json["Visible"] = this.Visible;
        //    json["Enabled"] = this.Enabled;
        //    json["Width"] = this.Width;
        //    json["Height"] = this.Height;
        //    json["ImeMode"] = this.ImeMode.ToString();
        //    if (this.Cursor != null)
        //    {
        //        json["Cursor"] = this.Cursor.ToString();
        //    }
        //    json["BackColor"] = ColorTranslator.ToHtml(this.BackColor);
        //    json["ForeColor"] = ColorTranslator.ToHtml(this.ForeColor);
        //    json["Font"] = this.Font.ToJsonObject();
        //}

        //private DCOwnerDrawModeConsts _DCOwnerDrawMode = DCOwnerDrawModeConsts.None;
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public DCOwnerDrawModeConsts DCOwnerDrawMode
        //{
        //    get { return _DCOwnerDrawMode; }
        //    set
        //    {
        //        _DCOwnerDrawMode = value; 
        //        if( value == DCOwnerDrawModeConsts.OwnerDraw 
        //            || value == DCOwnerDrawModeConsts.OwnerDrawWithScroll )
        //        {
        //            this.SetStyle(ControlStyles.UserPaint, true);
        //        }
        //    }
        //}
    }
    ///// <summary>
    ///// 控件自定义绘制模式
    ///// </summary>
    //[System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
    //public enum DCOwnerDrawModeConsts
    //{
    //    /// <summary>
    //    /// 无自定义绘制
    //    /// </summary>
    //    None,
    //    /// <summary>
    //    /// 普通的自定义绘制
    //    /// </summary>
    //    OwnerDraw,
    //    /// <summary>
    //    /// 带滚动条的自定义绘制
    //    /// </summary>
    //    OwnerDrawWithScroll
    //}
}
