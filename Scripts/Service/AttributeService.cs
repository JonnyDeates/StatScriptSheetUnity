using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class  AttributeService
{


// Effect Paramters inclues Name, Type, Duration, Function effecting Player
// Name = any sized string
// Type has three classes for ticking of the effect.
//     C - a continous ticking of the function, 60 times a second
//     I - a 1 second interval ticking of the function
//     ?%XX - a random chance of it occuring on every second, runs continously until duration ends, XX = chance percentage to occur 
// Function is the string name of the funcition that will be applied to the applicant
//public static Effect fire = new Effect("Fire", "C", 6000f);

    public static List<Attribute> Attributes = new List<Attribute>();
    public static List<Effect> AdditionalEffects = new List<Effect>();

    public static void populateAttributes(int AttributeStandard)
    {
        foreach(string stat in new pStats(0,0,0,0,0,0,0).getArrNames())
        {
            Attribute tempA = new Attribute(stat + " Increase", "P", stat, stat + " is boosted by ");
            tempA.addToAlerations(stat.ToLower(), AttributeStandard);
            Attributes.Add(tempA);
        }
        DirectoryInfo d = new DirectoryInfo(@"D:\Attribute");
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
        string contents = "";
        foreach (FileInfo file in Files)
        {
            contents = File.ReadAllText(file.FullName);
        }
        Debug.Log(contents);


    }
    public static float addAttributes(Entity entity, Armor armor)
    {
        entity.SetStats("bonusPrimaryStats", new pStats("bonusPrimaryStats"));
        foreach (Item item in armor.equipped)
        {
            foreach(Attribute attribute in item.attributes)
            {
                addAttribute(attribute, entity, item);
            }
        }
        return -100.31f; // Error called if the effect name does not Exist in any context. 
    }
    public static float addAttribute(Attribute tempA, Entity entity, Item item)
    {
        // Base Equations for Calculations
        if (tempA.Class == "P")
            {
            float total = 0f;
            foreach (Alterations dmg in tempA.Types) {
               total += entity.GetStats("bonusPrimaryStats").changeStat(dmg.iStat, dmg.value);
            }
            return total;
        }
        if (tempA.Class == "G")
        {
            float total = 0f;
            foreach (Alterations dmg in tempA.Types)
            {
                total += entity.GetStats("gameStats").changeStat(dmg.iStat, dmg.value);
            }
            return total;
        }
        if (tempA.Class == "D")
        {
            float total = 0f;
            foreach (Alterations dmg in tempA.Types)
            {
                total += item.ItemStats.changeStat(dmg.iStat, dmg.value);
            }
            return total;
        }
        return -100.31f; // Error called if the effect name does not Exist in any context. 
    }



    public static Attribute getAttribute(string name)
    {
        foreach(Attribute attribute in Attributes)
        {
            if(attribute.Name == name)
            {
                return new Attribute(attribute.Name, attribute.Class, attribute.Description);
            }
        }
        return null;
    }
}