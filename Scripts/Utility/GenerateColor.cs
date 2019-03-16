using UnityEngine;
using UnityEditor;

public static class GenerateColor
{

    public static string getRandHex()
    {
        int[] nums = { 0, 0, 0, 0, 0, 0, 0 };
        string color = "";

        for (int i = 0; i < nums.Length; i++) {
            if (Mathf.RoundToInt(Random.Range(0, 1)) == 0)
            {
                nums[i] = Mathf.RoundToInt(Random.Range(48, 57));
            }
            else
            {
                nums[i] = Mathf.RoundToInt(Random.Range(65, 70));
            }
        }

        foreach(int i in nums)
        {
            color += GenerateColor.I2S(i);
        }
        return color;
    }

    public static string I2S(int x)
   {
       return ((char)x).ToString();
   }

    public static string getSeededHex(int seed)
    {
        Random.InitState(seed);
        return getRandHex();
    }

}