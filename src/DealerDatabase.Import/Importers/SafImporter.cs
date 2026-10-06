using DealerDatabase.Import.Sources.XML.SAF;
using System.Xml.Serialization;

namespace DealerDatabase.Import.Importers
{
    public class SafImporter
    {
        public List<SafMember> Import(string filePath)
        {
            var serializer = new XmlSerializer(typeof(SafRegister));

            using var stream = File.OpenRead(filePath);

            var register = serializer.Deserialize(stream) as SafRegister;

            if (register == null)
            {
                throw new InvalidOperationException(
                    $"Unable to deserialize SAF register from '{filePath}'.");
            }

            return register.Members;
        }
    }
}
