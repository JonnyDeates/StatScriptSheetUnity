using System;

public class rankStats : Stats
{
    private const float baseXP = 20f;

    public rankStats()
    {
        arrNames = new string[] { "level", "rank", "experience", "kills", "deaths", "revives", "questsCompleted", "questsFailed", "allies", "alliesLost"};
        arr = new float[arrNames.Length];
        statName = "rankStats";
        setDefault();
    }
    public void addExperience(float val)
    {
        
        if (getStat("experience") + val > 100)
        {
            for (int i = 0; i < getStat("experience")/100; i++)
            {
                changeStat("level", 1f);
            }
                setStat("experience", val % 100);
        }
    }
    public void getKill(Entity entityKilled)
    {
        float eLevel = entityKilled.GetStats("rankStats").getStat("level");
        if (getStat("level")/eLevel <= 1) // If the player has an equal or smaller stat
        {// Adds a base xp of 20 if the two are the same level, else its that base + the differenece in level of the one killed   
            addExperience(baseXP + ((baseXP/2) * (eLevel - getStat("level"))));
        }
         else
        {// If the player has a larger stat
            addExperience((float) Math.Floor((baseXP / (getStat("level")-eLevel)))); // The base exp is divided by the difference in level
        }
        changeStat("kills", 1);
    }
    public void calRank()
    {
        //Multipliers to change how much each effect the rank
        float lMuliplier = 0.535f, // Level Multiplier
            qCMuliplier = 0.415f, // Quest Completed Multiplier
            kMuliplier = 0.0357f, // Kills Multiplier
            rMuliplier = 0.0143f, // Rivives Multiplier
        // **Adds up to 1f; 
        dMuliplier = 0.071f,
        qFMuliplier = 0.179f;
        // ** Adds up to 0.25f


        // Negative stats only effect the player 1/4th as much as positive stats.
        getArr()[Array.IndexOf(arrNames, "rank")] = (float) Math.Round((getStat("level") * lMuliplier + (getStat("questsCompleted") * qCMuliplier +  getStat("kills") * kMuliplier + getStat("revives") * rMuliplier) 
            / (getStat("deaths") * dMuliplier + getStat("questsFailed") * qFMuliplier)+1));
    }
    public void setDefault()
    {
        foreach(string name in arrNames)
        {
            if(name == "level")
            {
                setStat(name, 1f);
            }
            else
            {
                setStat(name, 0f);
            }
        }
        
    }

}