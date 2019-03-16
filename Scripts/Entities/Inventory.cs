using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;
    private float Capacity, currentCapacity;

    public Inventory()
    {
        Capacity = 20; // Base Capacity for all base Inventories in Cubic Meters 
    }
    public Inventory(int capacity)
    {
        Capacity = capacity; // Adjustable Capacity for Inventories in Cubic Meters 
    }
    public void updateCurrentCapacity(int Val)
    {
        currentCapacity += Val;
    }

    public bool addItem(Item item) // Adds an item to the inventory if it can fit based on the volume of the Item
    {
        if(Capacity >= currentCapacity + item.ItemStats.getStat("volume"))
        {
            setCurrentCapacity(item.ItemStats.getStat("volume"));
            items.Add(item);
            return true;
        }
        else
        {
            return false; 
        }
    }
    public bool removeItem(Item item) // Adds an item to the inventory if it can fit based on the volume of the Item
    {
        if (items.Contains(item))
        {
            setCurrentCapacity(-1 * item.ItemStats.getStat("volume"));
            items.Remove(item);
            return true;
        }
        else
        {
            return false;
        }
    }
    public Item dropItem(Item item) // Adds an item to the inventory if it can fit based on the volume of the Item
    {
        if (items.Contains(item))
        {
            setCurrentCapacity(-1*item.ItemStats.getStat("volume"));
            items.Remove(item);
            return item;
        }
        else
        {
            return item;
        }
    }

    public void addToCapacity(float volume)
    {
        Capacity = jMath.NumLimiter(Capacity, 0, volume);
    }
    public void setCurrentCapacity(float volume)
    {
        currentCapacity = jMath.NumLimiter(currentCapacity, 0, Capacity, volume);
    }
}