namespace System
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class SRCategoryAttribute : System.ComponentModel.CategoryAttribute
    {
        public SRCategoryAttribute(string category)
            : base(category)
        {
        }

        protected override string GetLocalizedString(string value)
        {
            return DCSR.GetString(value);
        }
    }

}
