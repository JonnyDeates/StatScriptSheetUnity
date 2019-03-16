using System;
using System.Collections.Generic;
using UnityEngine;

public class Armor 
{
    public List<Item> equipped;
    public string[] slots;
    public bool[] filledSlots;

    float totalProtection = 0f;

    public Armor()
    {
        equipped = new List<Item>();
        slots = new string[] { "helmet","chestpiece","leggings","boots","gloves","cloak", "amulet", "lh-ring", "rh-ring", "belt", "backpack"};
        filledSlots = new bool[slots.Length];
    }

    public void equipItem(Entity entity, Item item)
    {
        if (!equipped.Contains(item) && filledSlots[Array.IndexOf(slots,item.Class)]  )
        {
            equipped.Add(item);
            entity.Inventory.removeItem(item);
        }
    }




}