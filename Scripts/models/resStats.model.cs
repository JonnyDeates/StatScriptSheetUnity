using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class resStats : Stats
{
    
    // Creates a instatance using the raw data to create the float array. 
    private resStats(float AirRes, float DeathRes, float EarthRes, float ElectrictyRes, float FireRes, float ForbiddenRes, float HolyRes, float IceRes, float OrganicRes, float PoisonRes, float SorceryRes, float TimeRes, float UnholyRes, float WaterRes)
    {
        arrNames = new string[] { "airRes", "deathRes", "earthRes", "electrictyRes", "fireRes", "forbiddenRes", "holyRes", "iceRes", "organicRes", "poisonRes", "sorceryRes", "timeRes", "unholyRes", "waterRes" };
        arr = new float[] { AirRes, DeathRes, EarthRes, ElectrictyRes, FireRes, ForbiddenRes, HolyRes, IceRes, OrganicRes, PoisonRes, SorceryRes,TimeRes, UnholyRes, WaterRes };
        statName = "resistanceStats";
    }
    // Creates an instance based on the Primary Stats 
    public resStats(pStats stats)
    {
        arrNames = new string[] { "airRes", "deathRes", "earthRes", "electrictyRes", "fireRes", "forbiddenRes", "holyRes", "iceRes", "organicRes", "poisonRes", "sorceryRes", "timeRes", "unholyRes", "waterRes" };
        arr = calculateBaseRes(stats.getStat("vitality"), stats.getStat("strength") , stats.getStat("endurance") , stats.getStat("intelligance"), stats.getStat("dexterity"), stats.getStat("faith"), stats.getStat("resistance")).arr;
        statName = "resistanceStats";
    }


    public resStats calculateBaseRes(float Vitality, float Strength, float Endurance, float Intelligance, float Dexterity, float Faith, float Resistance)
    {
       
        float DR = 0.025f, 
            AirRes = (Intelligance * DR),
            DeathRes = (Faith * DR + Resistance * DR),
            EarthRes = (Strength * DR + Resistance * DR), 
            ElectrictyRes = (Intelligance * DR), 
            FireRes = (Strength * DR), 
            ForbiddenRes = (Faith * DR),
            HolyRes = (Faith * DR), 
            IceRes = (Strength * DR),
            OrganicRes = (Resistance * DR),
            PoisonRes = (Resistance * DR), 
            SorceryRes = (Intelligance * DR),
            TimeRes = (Intelligance * DR),
            UnholyRes = (Faith * DR), 
            WaterRes = (Strength * DR);

        return new resStats(AirRes, DeathRes, EarthRes, ElectrictyRes, FireRes, ForbiddenRes, HolyRes, IceRes, OrganicRes, PoisonRes, SorceryRes, TimeRes, UnholyRes, WaterRes);
    }

    public float damageBlocked(float damage, string type)
    {
        if(damage - getStat(type) < 0)
        {
            return 0;
        }

        return damage - getStat(type);
    }
}