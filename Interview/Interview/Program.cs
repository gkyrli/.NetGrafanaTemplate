// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;
using Interview.Datalayer.Models;
using static Interview.XMLSerializerUtitlities;

Console.WriteLine("Hello, World!");

var persons = new List<Person>()
{
    new Person()
    {
        FirstName = "PName1", LastName = "PLastName1", Age = 10,
    },
    new Person()
    {
        FirstName = "PName2", LastName = "PLastName2", Age = 20
    }
};

Serialize("persons.xml",persons);