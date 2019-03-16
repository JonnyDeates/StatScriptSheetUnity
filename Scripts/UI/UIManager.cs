using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class UIManager : MonoBehaviour
{
    public GameObject healthBar;
    public Text healthPerc;
    public GameObject magikaBar;
    public Text magikaPerc;
    public GameObject staminaBar;
    public Text staminaPerc;
    public GameObject statusContent;
    public EffectImageManager EffectImageManager;
    public Entity player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthPerc.text = Mathf.RoundToInt(getStat("health")) + "/" + getStat("maxHealth");
        magikaPerc.text = Mathf.RoundToInt(getStat("stamina")) + "/" + getStat("maxStamina");
        staminaPerc.text = Mathf.RoundToInt(getStat("magika")) + "/" + getStat("maxMagika");
        //healthbar.transform.localScale.Set((ThirdPersonCharacter.playerGStats.getStat("health") / ThirdPersonCharacter.playerGStats.getStat("maxHealth")) * 100, 0, 0);
        healthBar.gameObject.transform.localScale = new Vector3(getPerc("health","maxHealth"), 1f, 1f);
        magikaBar.gameObject.transform.localScale = new Vector3(getPerc("magika", "maxMagika"), 1f, 1f);
        staminaBar.gameObject.transform.localScale = new Vector3(getPerc("stamina", "maxStamina"), 1f, 1f);
    }

    void FixedStart()
    {

    }

    
private float getPerc(string stat, string maxStat) // Gets the percentage of hp/stanima/magika left
    {
        return (getStat(stat) / getStat(maxStat));
    }
    private float getStat(string stat)
    {
        return player.GetStats("gameStats").getStat(stat);
    }
}
