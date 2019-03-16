
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public itemStats ItemStats;
    private string creationDate;
    public string Class { get; set; }
    public List<Attribute> attributes;

    public Item()
    {
        attributes = new List<Attribute>();
         creationDate = new DateTime().ToShortDateString();
        Class = "item";
        ItemStats = new itemStats();
    }
    public Item(string ClassX)
    {
        attributes = new List<Attribute>();
        creationDate = new DateTime().ToShortDateString();
        Class = ClassX;
    }
}
