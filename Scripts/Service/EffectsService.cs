using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class  EffectsService
{


// Effect Paramters inclues Name, Type, Duration, Function effecting Player
// Name = any sized string
// Type has three classes for ticking of the effect.
//     C - a continous ticking of the function, 60 times a second
//     I - a 1 second interval ticking of the function
//     ?%XX - a random chance of it occuring on every second, runs continously until duration ends, XX = chance percentage to occur 
// Function is the string name of the funcition that will be applied to the applicant
//public static Effect fire = new Effect("Fire", "C", 6000f);

    public static List<Effect> Effects = new List<Effect>();
    public static List<Effect> AdditionalEffects = new List<Effect>();

    public static float runEffect(string effectName, Entity entity)
    {

        // Base Equations for Calculations
        
        if (effectName == "Fire")
            {
            return entity.GetStats("gameStats").changeStat("health", -0.1f);
        }
        if (effectName == "Poison")
        {
            return entity.GetStats("gameStats").changeStat("health", -4f);
        }
        DirectoryInfo d = new DirectoryInfo(@"D:\EffectServices"); //Assuming Test is your Folder
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
        string contents = "";
        foreach (FileInfo file in Files)
        {
            contents = File.ReadAllText(file.FullName);
        }
        Debug.Log(contents);


        return -100.31f; // Error called if the effect name does not Exist in any context. 
    }



    public static Effect getEffect(string name)
    {
        foreach(Effect effect in Effects)
        {
            if(effect.Name == name)
            {
                return new Effect(effect.Name, effect.Class, effect.Duration/60);
            }
        }
        return null;
    }
}