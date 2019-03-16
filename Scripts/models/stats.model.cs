using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats
{
    protected string[] arrNames;
    protected float[] arr;
    protected string statName;

    public float changeStat(string Key, float x)
    {
        if (Array.Find(arrNames, element => element.StartsWith(Key, StringComparison.Ordinal)) != null)
        {
            if (arr[Array.IndexOf(arrNames, Key)] - x > 0)
            {
                arr[Array.IndexOf(arrNames, Key)] += x;
            }
            else
            {
                arr[Array.IndexOf(arrNames, Key)] = 0;
            }
            return arr[Array.IndexOf(arrNames, Key)];
        }
        return -99f; // Returns -99 Error code if Key does not Exsist
    
    }

    public void setStat(string Key, float Val)
    { // Returns the value of the the stat based on the string entered
        if (Array.Find(arrNames, element => element.StartsWith(Key, StringComparison.Ordinal)) != null)
        {
             arr[Array.IndexOf(arrNames, Key)] = Val;
        }
    }

    public float getStat(string Key)
    { // Returns the value of the the stat based on the string entered
        if (Array.Find(arrNames, element => element.StartsWith(Key, StringComparison.Ordinal)) != null)
        {
            return arr[Array.IndexOf(arrNames, Key)];
        }
        return -99f; // Returns -99 Error code if Key does not Exsist
    }
    public string toString()
    {
        return statName;
    }
    public string toString(int Key)
    {
        if (Key >= 0 && Key < arrNames.Length)
        {
            return arrNames[Key];
        }
        return "Key Input Wrong";

    }

    public string[] getArrNames()
    {
        string[] arr = new string[arrNames.Length];
        for (int i = 0; i < arrNames.Length; i++)
        {
            arr[i] = arrNames[i];
        }
        return arr;
    }
    public float[] getArr()
    {
        return arr;
    }
}
