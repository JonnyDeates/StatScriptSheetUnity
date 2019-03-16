using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStats : Stats
{ 
    public pStats()
    {
        arrNames = new string[] { "vitality", "intelligance", "endurance", "strength", "dexterity", "faith", "resistance" };
        arr = new float[arrNames.Length];
        statName = "primaryStats";
    }
    public pStats(string name)
    {
        arrNames = new string[] { "vitality", "intelligance", "endurance", "strength", "dexterity", "faith", "resistance" };
        arr = new float[arrNames.Length];
        statName = name;
    }
    public pStats(float Vitality, float Strength, float Endurance, float Intelligance, float Dexterity, float Faith, float Resistance)
    {
        arrNames = new string[] { "vitality", "intelligance", "endurance", "strength", "dexterity", "faith", "resistance" };
        arr = new float[arrNames.Length];
        statName = "primaryStats";
        arr[0] = Endurance;
        arr[1] = Strength;
        arr[2] = Vitality;
        arr[3] = Intelligance;
        arr[4] = Dexterity;
        arr[5] = Faith;
        arr[6] = Resistance;
    }
}
