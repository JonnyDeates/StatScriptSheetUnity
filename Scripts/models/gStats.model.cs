using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gStats : Stats
{
    // health, maxHealth, archeryDamage, magikaDamage, meleeDamage, dodge, stamina, maxStamina, magika, maxMagika;
    public gStats(pStats stats)
    {
        arrNames = new string[] { "health", "maxHealth", "magikaDamage", "archeryDamage", "meleeDamage", "dodge", "stamina", "maxStamina", "magika", "maxMagika" };
        arr = calculateBaseStats(stats.getStat("vitality"), stats.getStat("strength") , stats.getStat("endurance") , stats.getStat("intelligance") , stats.getStat("dexterity") , stats.getStat("faith"), stats.getStat("resistance")).arr;
        statName = "gameStats";
    }
    public gStats(float Vitality, float Strength, float Endurance, float Intelligance, float Dexterity, float Faith, float Resistance)
    {
        arrNames = new string[] { "health", "maxHealth", "magikaDamage", "archeryDamage", "meleeDamage", "dodge", "stamina", "maxStamina", "magika", "maxMagika" };
        arr = calculateBaseStats(Vitality, Strength, Endurance, Intelligance, Dexterity, Faith, Resistance).arr;
        statName = "gameStats";
    }
    protected gStats(float Health, float MaxHealth, float ArcheryDamage, float MagikaDamage, float MeleeDamage, float Dodge, float Stamina, float MaxStamina, float Magika, float MaxMagika)
    {
        arrNames = new string[] { "health", "maxHealth", "magikaDamage", "archeryDamage", "meleeDamage", "dodge", "stamina", "maxStamina", "magika", "maxMagika" };
        arr = new float[] { Health, MaxHealth, MagikaDamage, MeleeDamage, ArcheryDamage, Dodge, Stamina, MaxStamina, Magika, MaxMagika };
        statName = "gameStats";
    }

    public gStats calculateBaseStats(float Vitality, float Strength, float Endurance, float Intelligance, float Dexterity , float Faith, float Resistance)
    {
        float MaxHealth = Vitality * 5, // Base Health && MaxHealth 
        ArcheryDamage = Mathf.RoundToInt((Dexterity * 0.45f) + (Strength * 0.05f)), // Base Archery Damage
        MagikaDamage = Mathf.RoundToInt(((Intelligance * 0.3f) + (Faith * 0.2f) + (Resistance * 0.1f))), // Base Magic Damage on Projectile Spell 
        MeleeDamage = Mathf.RoundToInt((Strength * 0.45f) + (Dexterity * 0.05f)), // Base Melee Damage 
        Dodge = (Dexterity * 0.05f) + (Strength * 0.001f), // Dodge Percentage chance
        Stamina = Endurance * 5, // Base Stamina && MaxStamina
        Magika = Intelligance * 5; // Base Magika && MaxMagika

        return new gStats(MaxHealth, MaxHealth, ArcheryDamage, MagikaDamage, MeleeDamage, Dodge, Stamina, Stamina, Magika, Magika);
    }

    public new float changeStat(string Key, float x)
    {
        switch (Key) {
            case "health":
                arr[0] = jMath.NumLimiter(arr[0], 0, arr[1], x);
                return arr[0];
            case "maxHealth":
                arr[1] += x;
                return arr[1];
            case "magikaDamage":
                arr[2] += x;
                return arr[2];
            case "archeryDamage":
                arr[3] += x;
                return arr[3];
            case "meleeDamage":
                arr[4] += x;
                return arr[4];
            case "dodge":
                if(arr[5] + x <= 50) {
                    arr[5] += x;
                }
                return arr[5];
            case "stamina":
                arr[6] = jMath.NumLimiter(arr[6], 0, arr[7], x);
                return arr[6];
            case "maxStamina":
                arr[7] += x;
                return arr[7];
            case "magika":
                arr[8] = jMath.NumLimiter(arr[8], 0, arr[9], x);
                return arr[8];
            case "maxMagika":
                arr[9] += x;
                return arr[9];
            default:
               return -99; // Error -99 , Returns digits if Key is input in wrong
        }
    }
    
}
