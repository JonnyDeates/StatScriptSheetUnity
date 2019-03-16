

using System;

public static class StockExchange
{
    public static float[] OrbVals;
    public static float[] Count;
    private static string[] OrbList= {"Orb","Decorb","Centorb","Millorb"};
    public static void StartExchange()
    {
        OrbVals = new float[OrbList.Length];
        Count = new float[OrbList.Length];
    }


    public static void calculateExchange()
    {
        for(int i = 0; i < OrbList.Length; i++)
        {
            if (StockExchange.getRate(OrbList[i]) == 0)
            {
                OrbVals[i] = ((((i % OrbList.Length) + 1) * (float) Math.Round(Math.Pow(10, i))) + jMath.SumFloatArray(Count)) / Count[i]; // Takes a value made from the ranking spot, 
            }
        }
       
    }

    public static void decCount(string type)
    {
        for (int i = 0; i < OrbList.Length; i++)
        {
            if (type == OrbList[i])
            {
                Count[i]--;
            }
        }
    }
    public static void incCount(string type)
    {
        for(int i = 0; i < OrbList.Length; i++)
        {
            if (type == OrbList[i])
            {
                Count[i]++;
            }
        }
    }
    public static float getRate(string type)
    {
        for (int i = 0; i < OrbList.Length; i++)
        {
            if (type == OrbList[i])
            {
                return OrbVals[i];
            }
        }
        return 0;
    }

}