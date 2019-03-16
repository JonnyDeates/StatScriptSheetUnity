using System.Collections.Generic;

public class netWorth
{
    public List<Orb> OrbList;
    // public Orb[] Orb,Decorb,Centorb, Millorb;

    private float totalValue;

    public netWorth() {
        OrbList = new List<Orb>();
        totalValue = 0; 
    }

    public float calcTotal()
    {
        foreach(Orb orb in OrbList)
        {
            totalValue += orb.value;
        }
        return totalValue;
    }



}