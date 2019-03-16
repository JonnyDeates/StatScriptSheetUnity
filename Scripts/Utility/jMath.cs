public static class jMath
{
    public static float SumFloatArray(float[] arr)
    {
        float val = 0f;
        for (int i = 0; i < arr.Length; i++)
        {
            val += arr[i];
        }
        return val;
    }

    public static float NumLimiter(float x, float MinValue, float MaxValue, float modifier)// Limits a number to a certain range I.e. Min-Max while adding a value to it.
    {
        if (x + modifier > MinValue)
        {
            if (x + modifier >= MaxValue)
            {
                x = MaxValue;
            }
            else
            {
                x += modifier;
            }
        }
        else
        {
            x = MinValue;
        }
        return x;
    }

    public static float NumLimiter(float x, float MinValue, float modifier)// Limits a number to a certain range I.e. Min-Infinity while adding a value to it. 
    {
        if (x + modifier > MinValue)
        {
            x += modifier;
        }
        else
        {
            x = MinValue;
        }
        return x;
    }

    public static float MinMLimiter(float x, float MinValue, float multiplier)// Limits a number to above a value while multipling a value to it.
    {
        if (x * multiplier > MinValue)
        {
           x *= multiplier;
        }
        else
        {
            x = MinValue;
        }
        return x;
    }
}