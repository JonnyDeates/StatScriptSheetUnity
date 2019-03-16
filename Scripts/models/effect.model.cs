using System;
using System.Collections.Generic;

public class Effect
{
    public float Duration;
    public string ID { get; set; }
    public string Name { get; set; }
    public List<Alterations> Types = new List<Alterations>();
    public string Class;

    public Effect(string name, string type,float duration)
    {
        ID = Guid.NewGuid().ToString();
        Duration = jMath.MinMLimiter(duration, 1, 60);
        Name = name;
        Class = type;
    }

    public float Run(Entity entity)
    {
       return EffectsService.runEffect(Name, entity);
    }
}
