public class itemStats : Stats
{

    public itemStats()
    {
        arrNames = new string[] { "value", "durability", "volume", "weight" };
        arr = new float[arrNames.Length];
        statName = "itemStats";
    }
}