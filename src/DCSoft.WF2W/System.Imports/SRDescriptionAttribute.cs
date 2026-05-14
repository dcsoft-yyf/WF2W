namespace System
{
    public class SRDescriptionAttribute :System.ComponentModel.DescriptionAttribute
    {
        private bool replaced;

        public override string Description
        {
            get
            {
                if (!replaced)
                {
                    replaced = true;
                    base.DescriptionValue = DCSR.GetString(base.Description);
                }
                return base.Description;
            }
        }

        public SRDescriptionAttribute(string description)
            : base(description)
        {
        }
    }
}
