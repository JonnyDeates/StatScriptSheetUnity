public class Orb
{
    public float value;
    private string Name;
    public string[] Names = { "Orb", "Decorb","Centorb", "Millorb" };


    public Orb(string nameJ) {
        foreach(string nameK in Names)
        {
            if(nameK == nameJ)
            {
                Name = nameK;
                value = StockExchange.getRate(nameK);
                StockExchange.incCount(nameK);
            }
        }

    } 

    public string getName()
    {
        return Name;
    }

    /*public void useOrb(Item item)
    {
        if(Name == "Orb")
        {
            item.changeRAttribute();
        }
    }*/


}