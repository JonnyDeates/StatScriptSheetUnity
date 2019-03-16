using System.Collections;
using System.Collections.Generic;

public class Alterations
{
    public float value { get; set; }
    public string iStat { get; set; }

    public Alterations(string istat, float val)
    {
        iStat = istat;
        value = val;
    }
}