using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //allows you to create object instances from Create Menu (right click in project tab)
public class PlayerData : ScriptableObject //THIS IS A SCRIPTABLE OBJECT. NOT MONOBEHAVIOUR
{
    // ---------------------------------------------------------------------------
    //
    // PLAYER DATA SCRIPTABLE OBJECT SCRIPT
    //
    // This script defines the major variables and methods needed to manipulate
    // an individual character. This is done by creating SCRIPTABLE OBJECTS with
    // this script as its base.
    //
    // It is comprised of getters, setters, and modifiers for variables. It also
    // holds the card images.
    //
    // It DOES NOT define the job or the values on its own (though, it could...)
    // - JOBDATA defines individual jobs (and there is an object per job filled out)
    // - CREATECARD puts everything together to populate the object and data.
    // --------------------------------------------------------------------------


    //These variables are Private variables. That means that other scripts/classes cannot
    //access them directly. Instead, there are public methods/functions to get or set them.
    //using [SerializeField] allows us to expose them and show and edit the variables in the 
    //editor. This is *GREAT* for testing or for things that you want to be able to tweak
    //while editing, but, by the end of your project, it is best to take the Serialization off of
    //it and return it to just being private.

    [Header("Private, Serialized Variables")] //[Header()] lets us break up inspector into sections
    [Tooltip("(PRIVATE VAR, use get/set):The health points of the player")] //[Tooltip()] lets us create hover over text
    [SerializeField] //expose private variables in the inspector. See block above.
    private int hp; // health
    private int maxhp;

    [Tooltip("(PRIVATE VAR, use get/set): The defense points of the player")]
    [SerializeField]
    private int dp; // defense
    private int maxdp;

    [Tooltip("(PRIVATE VAR, use get/set): The physical attack power of the player")]
    [SerializeField]
    private int ap; // physical attack
    private int maxap;
    
    [Tooltip("(PRIVATE VAR, use get/set): The magical attack power of the player")]
    [SerializeField]
    private int mp; // magic attack? Or...points?
    private int maxmp;


    //Most of these could (easily) be private as well, you would follow the same sort of getter/setter setup
    //The one slight hiccup would be the texture array. Only because that might be a pain to work with if it 
    //was totally private.
    [Header("Public Variables, accessible to other classes")]
    [Tooltip("The base class of the player")]
    public string job; // class

    [Tooltip("The displayed class of the player")]
    public string displayJob; // class

    [Tooltip("The archetype of the class of the player")]
    public string jobMod; //adjective to describe class

    [Tooltip("Array of all the textures, 0-Body, 1-eyes, 2-5-clothes, 6-9, weapon")]
    public Texture2D[] cardTextures;


    //---------------------------------------
    //
    // Health Manipulation Methods
    //
    //---------------------------------------

    //Returns the hp (health). This is how other classes/scripts can see the health
    public int GetHP()
    {
        return hp;
    }

    //takes an integer and sets hp and max hp to that integer. This is how other classes/scripts set the health
    public void SetHP(int health)
    {
        hp = health;
        maxhp = hp;
        //Debug.Log("SetHP: " + health + " " + hp);
    }

    //takes an integer (damage) and adjusts hp
    public void TakeDamage(int damage)
    {
        hp = hp - damage;
    }

    //takes an integer (heal) and adjust hp
    //makes sure that the hp is over the max/starting hp
    public void Heal(int heal)
    {
        hp = hp + heal;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
    }

    //checks if hp is zero or below
    public bool IsDead()
    {
        if (hp <= 0)
        {
            return true;

        }
        else 
        {
            return false;
        }
    }


    //---------------------------------------
    //
    // Defense Manipulation Methods
    //
    //---------------------------------------
    
    //returns the defense value
    public int GetDP()
    {
        //Debug.Log("GetHP " + hp);
        return dp;
    }

    //sets the defense and max defense, taking in an external integer
    public void SetDP(int def)
    {
        dp = def;
        maxdp = dp;
    }

    //takes an external integer (heal), adds to defense. Makes sure not over max defense
    public void DefHeal(int heal)
    {
        dp += heal;
        if (dp > maxdp)
        {
            dp = maxdp;
        }
    }

    //takes external integer (dam), subtracts defense. Does not go under 0
    public void DefDamage(int dam)
    {
        dp -= dam;
        if (dp <= 0)
        {
            dp = 0;
        }
    }

    //takes an external integer (buff), adds to defense. Can go over max defense
    //Could combine with DefHeal and use a boolean to check if it's a buff or just a heal
    public void DefBuff(int buff)
    {
        dp += buff;
        maxdp += buff;
    }

    //takes external integer (nerf), subtracts from defense and max defense.
    //Can't go under 0
    public void DefNerf(int nerf)
    {
        dp -= nerf;
        maxdp -= nerf;
        if (dp <= 0)
        {
            dp = 0;
        }
        if (maxdp <= 0)
        {
            maxdp = 0;
        }
    }


    //---------------------------------------
    //
    // Attack Manipulation Methods
    //
    //---------------------------------------
    
    //Get the attack power
    public int GetAP()
    {
        //Debug.Log("GetHP " + hp);
        return ap;
    }


    //Using external integer (att), set the attack power and max attack
    public void SetAP(int att)
    {
        ap = att;
        maxap = ap;
    }

    //Using external integer (heal), add to attack. Cannot go over maxap
    public void AttHeal(int heal)
    {
        ap += heal;
        if (ap > maxap)
        {
            ap = maxap;
        }
    }

    //Using external integer (dam), subtract from attack (non permanent). Cannot go under 0
    public void AttDamage(int dam)
    {
        ap -= dam;
        if (ap <= 0)
        {
            ap = 0;
        }
        if (maxap <= 0)
        {
            maxap = 0;
        }
    }

    //Using external variable (buff), add to attack and max attack (permanent)
    public void AttBuff(int buff)
    {
        ap += buff;
        maxap += buff;
    }

    //using external variable (nerf), subtract from attack and max attack (permanent). Cannot go below 0
    public void AttNerf(int nerf)
    {
        ap -= nerf;
        maxap -= nerf;
        if (ap <= 0)
        {
            ap = 0;
        }
        if (maxap <= 0)
        {
            maxap = 0;
        }

    }

    //Roll a dice of external variable number of sides (diceSides).
    //If one of last two numbers, return true. Otherwise, false.
    public bool DidItCrit(int diceSides)
    {
        int critNum = Random.Range(1, diceSides);
        if (critNum >= diceSides - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //take external dice num, roll it, multiply attack by it
    public int CritAttack(int multiplierDice)
    {
        int mult = Random.Range(1, multiplierDice);
        return ap * mult;
    }

    //---------------------------------------
    //
    // Magic Manipulation Methods
    //
    //---------------------------------------

    //Get MP
    public int GetMP()
    {
        //Debug.Log("GetHP " + hp);
        return mp;
    }

    //Set magic and max magic with external integer
    public void SetMP(int mag)
    {
        mp = mag;
        maxmp = mp;
    }

    //Heal damage to mp using external variable (heal). Cannot go over maxmp
    public void MagHeal(int heal)
    {
        mp += heal;
        if (mp > maxmp)
        {
            mp = maxmp;
        }
    }

    //Damage mp using external variable. Cannot go below zero.
    public void MagDamage(int dam)
    {
        mp -= dam;
        if (mp <= 0)
        {
            mp = 0;
        }
        if (maxmp <= 0)
        {
            maxmp = 0;
        }
    }

    //Adds to magic using external variable. Buff permanently increases mp and maxmp
    public void MagBuff(int buff)
    {
        mp += buff;
        maxmp += buff;
    }

    //Subtracts from magic using external variable. Nerf permanently decreases mp and maxmp (cannot go below 0)
    public void MagNerf(int nerf)
    {
        mp -= nerf;
        maxmp -= nerf;
        if (mp <= 0)
        {
            mp = 0;
        }
        if (maxmp <= 0)
        {
            maxmp = 0;
        }

    }

    //Takes dice, rolls it, multiplies attack by dice
    public int CritMagic(int multiplierDice)
    {
        int mult = Random.Range(1, multiplierDice);
        return mp *= mult;
    }


}
