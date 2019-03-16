using System;
using System.Collections.Generic;

public class Attribute
{
    public string Description;
    public string ID { get; set; }
    public string Name { get; set; }
    public List<Alterations> Types = new List<Alterations>();
    public string Class;
    public string Identifier;

    public Attribute(string name, string type,string description)
    {
        ID = Guid.NewGuid().ToString();
        Description = description;
        Name = name;
        Class = type;
    }

    public Attribute(string name, string type, string identifier,string description)
    {
        ID = Guid.NewGuid().ToString();
        Description = description;
        Name = name;
        Class = type;
        Identifier = identifier;
    }

    public void addToAlerations(string str, float val)
    {
        Types.Add(new Alterations(str, val));
    }
}