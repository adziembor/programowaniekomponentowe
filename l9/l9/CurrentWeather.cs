using System.IO;
using System.Xml.Serialization;

namespace l9
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class CurrentWeather
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(CurrentWeather));

        /// <remarks />
        public string Location { get; set; }

        /// <remarks />
        public string Time { get; set; }

        /// <remarks />
        public string Wind { get; set; }

        /// <remarks />
        public string Visibility { get; set; }

        /// <remarks />
        public string SkyConditions { get; set; }

        /// <remarks />
        public string Temperature { get; set; }

        /// <remarks />
        public string DewPoint { get; set; }

        /// <remarks />
        public string RelativeHumidity { get; set; }

        /// <remarks />
        public string Pressure { get; set; }

        /// <remarks />
        public string Status { get; set; }

        public static CurrentWeather ConvertXml(string xml)
        {
            return (CurrentWeather)Serializer.Deserialize(new StringReader(xml));
        }
    }
}