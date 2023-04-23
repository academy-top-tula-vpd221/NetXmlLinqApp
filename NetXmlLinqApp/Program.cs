using System;
using System.Xml.Linq;
using System.Xml.Serialization;

/*
Employee employee = new Employee() 
{ 
    FirstName = "John",
    LastName = "Smith",
    Age = 34
};

List<Employee> employees = new()
{
    new()
    {
        FirstName = "Bob",
        LastName = "Whilson",
        Age = 29
    },
    new()
    {
        FirstName = "Leo",
        LastName = "Marshall",
        Age = 41
    },
    new()
    {
        FirstName = "Joe",
        LastName = "Green",
        Age = 37
    }
};

XmlSerializer serializerEmployee = new XmlSerializer(typeof(Employee));

using(FileStream stream = new("employee.xml", FileMode.OpenOrCreate))
{
    serializerEmployee.Serialize(stream, employee);
}

XmlSerializer serializerEmployeeList = new XmlSerializer(typeof(List<Employee>));
using (FileStream stream = new("employees.xml", FileMode.OpenOrCreate))
{
    serializerEmployeeList.Serialize(stream, employees);
}
*/

XmlSerializer serializerEmployee = new XmlSerializer(typeof(Employee));
using(FileStream stream = new("employee.xml", FileMode.OpenOrCreate))
{
    var empl = serializerEmployee.Deserialize(stream) as Employee;
    Console.WriteLine($"{empl.FirstName} {empl.LastName} {empl.Age}");
}
Console.WriteLine();

XmlSerializer serializerEmployeeList = new XmlSerializer(typeof(List<Employee>));
using (FileStream stream = new("employees.xml", FileMode.OpenOrCreate))
{
    var emplList = serializerEmployeeList.Deserialize(stream) as List<Employee>;
    foreach(var e in emplList)
        Console.WriteLine($"{e.FirstName} {e.LastName} {e.Age}");
}
void XmlLinqFindModify()
{
    XDocument xDoc = XDocument.Load("books.xml");
    XElement? root = xDoc.Root;


    /*
    if(root != null )
    {
        foreach( XElement element in root.Elements())
        {
            Console.WriteLine($"Element: {element.Name}");
            foreach(XAttribute attribute in element.Attributes())
                Console.WriteLine($"\t{attribute.Name}: {attribute.Value}");
            Console.WriteLine();
            foreach (XElement elemChild in element.Elements())
                Console.WriteLine($"\t{elemChild.Name}: {elemChild.Value}");
            Console.WriteLine("--------");
        }
    }
    */
    /*
    var booksEn = root?.Elements()?
                        .Where(b => b.Element("title")?.Attribute("lang")?.Value == "en")
                        .Select(b => new { 
                            Author = b.Element("author")?.Value,
                            Title = b.Element("title")?.Value
                        });
    foreach(var b in booksEn)
    {
        Console.WriteLine($"{b.Author} {b.Title}");
    }
    */
    /*
    foreach(XElement element in root.Elements())
    {
        var year = element.Element("year");
        if (year != null && year.Value == "2005")
            year.Value = "2006";
    }
    */

    var bookIt = root.Elements()
                        .FirstOrDefault(b => b.Element("title")?
                                            .Attribute("lang")?
                                            .Value == "it");
    bookIt?.Remove();

    xDoc.Save("books.xml");

    Console.WriteLine(xDoc);
}
void XmlEmployees()
{
    Employee employee = new Employee()
    {
        FirstName = "Joe",
        LastName = "Smith",
        Age = 28,
    };

    Employee employee2 = new Employee()
    {
        FirstName = "Sam",
        LastName = "Whilson",
        Age = 34,
    };

    XDocument xDoc = new();
    XElement root = new("users");
    xDoc.Add(root);

    XElement joe = new("user");
    XAttribute firstName = new("firstname", employee.FirstName);
    XElement lastName = new("lastname", employee.LastName);
    XElement age = new("age", employee.Age.ToString());
    joe.Add(firstName);
    joe.Add(lastName);
    joe.Add(age);

    root.Add(joe);

    root.Add(
        new XElement("user",
            new XAttribute("firstname", employee2.FirstName),
            new XElement("lastname", employee2.LastName),
            new XElement("age", employee2.Age.ToString()))
        );


    xDoc.Save("users.xml");
}

[Serializable]
public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
