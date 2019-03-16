using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters;
using UnityStandardAssets.Characters.ThirdPerson;

public class EffectImageManager : MonoBehaviour
{

    [SerializeField] public GameObject pFire;
    [SerializeField] public GameObject pPoison;
    [SerializeField] public Entity player;
    private int indexLength; 
    private List<GameObject> Images = new List<GameObject>();

    void Awake()
    {
        indexLength = 0;
        EffectsService.Effects.Add(new Effect("Fire", "C", 3));
        EffectsService.Effects.Add(new Effect("Poison", "I", 10));
        Images.Add(pFire);
        Images.Add(pPoison);
        addStatusEffects();

    }

    // Runs at the Fixed framerate of the game
    void FixedUpdate()
    {
        if(indexLength <= player.activeEffects.Count)
        {
            addStatusEffects();
        }
    }

    public void addStatusEffects()
    {
        if (player.activeEffects.Count > 0  && player.activeEffects.Count != indexLength)
        {

           // int i = 0;
            for(int i = 0; i < player.activeEffects.Count; i++) // Effect effect in player.activeEffects)
            {
                Populate(player.activeEffects[i].Name, i);
             //   i += 1;
            }
            indexLength = player.activeEffects.Count;
        }
    }

    public void  Populate(string name, int index)
    {
        // Create GameObject instance
        GameObject newObj; 
   
        // Create new instance of our prefab 
        GameObject prefab = getImg(name);
        prefab.transform.localPosition = new Vector3(0, 0 + 320 * index, 0);

        newObj = (GameObject)Instantiate(prefab, transform);
    }




    internal GameObject getImg(string name)
    {
        foreach(GameObject image in Images)
        {
            if(image.name == name)
            {
                return image;
            }
        }
        return null;
    }
}