using System.IO;
using System.Text;

namespace Catrobat.Core
{
    public class XmlStringWriter : StringWriter
    {
        private readonly Encoding encoding;

        public XmlStringWriter(StringBuilder builder, Encoding encoding)
            : base(builder)
        {
            this.encoding = encoding;
        }

        public XmlStringWriter(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public XmlStringWriter()
        {
            encoding = Encoding.UTF8;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}