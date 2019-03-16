using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : WorldSpace
{
    [SerializeField] string Name = "Null";
    [SerializeField] float Vit = 10;
    [SerializeField] float Str = 10;
    [SerializeField] float End = 10;
    [SerializeField] float Int = 10;
    [SerializeField] float Dex = 10;
    [SerializeField] float Faith = 10;
    [SerializeField] float Res = 10f;
    
   public string id = Guid.NewGuid().ToString();

    public List<Stats> Stats;
    public Inventory Inventory;

    public Armor armor; 

    public List<Effect> activeEffects = new List<Effect>();
    ParticleSystem pSystem;

    public static GameObject entityModel;
    private Rigidbody rig;


    private void Start()
    {
        Stats = new List<Stats>(); // All the Stats of the Entity
        Inventory = new Inventory(); // Inventory of the Entity 
        Stats.Add(new pStats(Vit, Str, End, Int, Dex, Faith, Res)); // **Primary Stats -- Primary Stats for the Entity 
        Stats.Add(new pStats("bonusPrimaryStats"));  // ** Additional Primary Stats -- Manages any passive/active effects/attributes/spells.
        Stats.Add(new gStats((pStats) GetStats("primaryStats"))); // **General Stats -- General Stats for the Entity during Gameplay 
        Stats.Add(new resStats((pStats) GetStats("primaryStats"))); // **Resistance Stats -- Primary Resistance Stats for the Entity during Gameplay 
        Stats.Add(new rankStats());// **Rank Stats -- Primary Rank Stats for the Entity during Gameplay 
    }

    public Stats GetStats(string statName) // Gets a stat from the name of the stat -- primaryStats, bonusPrimaryStats, gameStats, resistanceStats, rankStats
    {
        foreach (Stats stat in Stats)
        {
            Debug.Log(stat.toString() + "  " + (stat.toString() == statName) + "  "+statName);
            if (stat.toString() == statName)
            {
                return stat;
            }
        }
        return null;
    }
    public void SetStats(string statName, Stats stats) // Sets a stat based on the stat's Name to a Stat
    {
        Stats[Stats.IndexOf(Stats.Find((Stats stat) => stat.toString() == statName))] = stats; 
    }
   

    public void ChangeStat(string statName, float Val) // Changes a Stat from any Stat List, based on the statName to be changed i.e. health
    {
        foreach(Stats stat in Stats)
        {
            if (Array.Find(stat.getArrNames(), (string el) => el == statName) != null && stat.toString() != "primaryStats")
            {
                stat.changeStat(statName, Val);
            }
        }
    }
    public void DamageRecieved(string statName, string type, float dmg) // Access to any stat based on name
    {
        GetStats("gameStats").changeStat(statName, ((resStats)GetStats("resistanceStats")).damageBlocked(dmg, type));
    }
    public void DamageRecieved(string type, float dmg) // Direct health access, for moves that have a type of resistance
    {
        GetStats("gameStats").changeStat("health", ((resStats) GetStats("resistanceStats")).damageBlocked(dmg, type));
    }
    public void DamageRecieved(float dmg) // Direct health access, for moves that avoid all protection
    {
        GetStats("gameStats").changeStat("health", dmg);
    }

    public override void IntervalUpdate() // Gets Called every Second
    {
        if (activeEffects.Count > 0) //Checks if there is any active effects on the player
        {
           
            foreach (Effect effect in activeEffects)
            {
                if (effect.Class == "I") // Checks if the effect is just an interval effect
                {
                    effect.Run(this); //Effect Ran
                }
                if (effect.Class.Substring(0, 1) == "?") //Checks if the effect is a special kind of effect
                {
                    if (Int32.Parse(effect.Class.Substring(2)) >= UnityEngine.Random.Range(1, 100)) //Gets the chance of this effect being applied
                    {
                        effect.Run(this); //Effect Ran
                    }
                }
            }
        }
    }

    public void FixedUpdate() // Unities FixedUpdate function called on every frame of the game
    {
        if (activeEffects.Count > 0)
        {
            List<Effect> tempEffects = new List<Effect>();
            foreach (Effect effect in activeEffects)
            {
                if (effect.Duration < 0)
                {
                    tempEffects.Add(effect);
                }
                if (effect.Class == "C")
                {
                    effect.Run(this);
                }
                effect.Duration -= 1;
            }

            if (tempEffects.Count > 0)
            {
                foreach(Effect effect in tempEffects)
                {
                    activeEffects.Remove(effect);
                }
            }
        }
    }

    public void AddPassiveAttributes() // Adds the attributes to the Entity
    {
      AttributeService.addAttributes(this, armor);
    }

    public void AddEffectLimited(Effect effect) // Adds an effect as long as it is not already added. 
    {
        Effect tempEff = activeEffects.Find((Effect el) => el.Name == effect.Name);
       
        if (tempEff == null )
        {
            activeEffects.Add(effect);
        }
    }
}
