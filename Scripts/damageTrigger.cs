using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class damageTrigger : MonoBehaviour
{
    private static GameObject colObj; // Object that collides with the trigger. 

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Collider>().tag == "Player") {
           // Debug.Log("Called " + other.gameObject.GetComponent<ThirdPersonCharacter>().player.gStats.changeStat("health", -3f));
           // other.gameObject.GetComponent<Entity>().activeEffects.Add(EffectsService.getEffect("Fire"));
            other.gameObject.GetComponent<Entity>().AddEffectLimited(EffectsService.getEffect("Poison"));
            // Debug.Log("Worked?  " + other.gameObject.GetComponent<Entity>().activeEffects[0].Run(other.gameObject.GetComponent<Entity>()));
            //player.GetComponent<Player>().gStats.changeStat("health",-5f)
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Collider>().tag == "Player")
        {
            // Debug.Log(EffectsService.fire.Name);
          
           // Debug.Log("Called " + other.gameObject.GetComponent<ThirdPersonCharacter>().player.gStats.changeStat("health", -0.1f));
            //player.GetComponent<Player>().gStats.changeStat("health",-5f)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
