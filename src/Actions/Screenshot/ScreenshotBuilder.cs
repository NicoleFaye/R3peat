using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class ScreenshotBuilder
    {
        private string _name;
        private string _savePath;
        private NameIncrementer NameIncrementer;

        public ScreenshotBuilder()
        {
            NameIncrementer = new NameIncrementer("Screenshot");
        }

        public ScreenshotBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ScreenshotBuilder SetSavePath(string savePath)
        {
            _savePath = savePath;
            return this;
        }

        public Screenshot Build()
        {
            string finalName = _name ?? NameIncrementer.Next();
            return new Screenshot(finalName, _savePath);
        }
    }

}
