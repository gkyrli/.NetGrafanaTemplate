using System.Xml.Serialization;
using Interview.Datalayer.Models;

namespace Interview;

public static class XMLSerializerUtitlities
{
    public static void Serialize(string path,object o)
    {
        var xmlSerializer = new XmlSerializer(o.GetType());
        var stream = new FileStream(path,FileMode.OpenOrCreate);
        xmlSerializer.Serialize(stream,o);
    } 
}