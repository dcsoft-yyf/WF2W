using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    internal static class ControlsDemoUtils
    {
        public static Image LoadImageAndCloseStream(Stream stream)
        {
            if (stream is MemoryStream)
            {
                var img = Image.FromStream(stream);
                stream.Close();
                return img;
            }

            var ms = new MemoryStream();
            var bs = new byte[1024];
            while (true)
            {
                var len = stream.Read(bs, 0, bs.Length);
                if (len <= 0)
                {
                    break;
                }
                ms.Write(bs, 0, len);
            }
            stream.Close();
            ms.Position = 0;
            return Image.FromStream(ms);
        }

        public static void FillCusors(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.DisplayMember = "Item1";
            comboBox.ValueMember = "Item2";
            foreach (var item in typeof(Cursors).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                comboBox.Items.Add(new Tuple<string, Cursor>(item.Name, (Cursor)item.GetValue(null, null)));
            }
        }

        public static void FillList<EnumType>(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(Enum.GetValues(typeof(EnumType)).Cast<object>().ToArray());
        }
    }
}
