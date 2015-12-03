using Catrobat.Player.StandAlone.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace Catrobat.Player.StandAlone.Parser
{
    public class XMLParser
    {
        public async Task<DataTypes.Program> FakeParsing()
        {
            var t = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("projects\\testalphavalue\\code.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(DataTypes.Program));

            DataTypes.Program result = null;
            using (StreamReader reader = new StreamReader(await t.OpenStreamForReadAsync()))
            {
                result = (DataTypes.Program)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
