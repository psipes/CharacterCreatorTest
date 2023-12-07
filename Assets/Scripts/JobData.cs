using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines JOB object (SCRIPTABLE OBJECTS)
[CreateAssetMenu(menuName = "JobStats")]
public class JobData : ScriptableObject
{
    public string jobName;
    public string[] titles;
    public string[] modifiers;

    [SerializeField]
    private int minHP;
    [SerializeField]
    private int maxHP;

    [SerializeField]
    public int minDef;
    [SerializeField] 
    private int maxDef;

    [SerializeField] 
    private int minAtt;
    [SerializeField] 
    private int maxAtt;

    [SerializeField] 
    private int minMag;
    [SerializeField] 
    private int maxMag;

    [Header("Clothing And Weapons")]
    [Tooltip("Base, Random, Metal, Lines")]
    public Texture2D[] clothes;
    [Tooltip("Base, Metal, Random, Lines")]
    public Texture2D[] weapon;

    public int RandomHP()
    {
        return Random.Range(minHP, maxHP);
    }

    public int RandomDef()
    {
        return Random.Range(minDef, maxDef);
    }

    public int RandomAtt()
    {
        return Random.Range(minAtt, maxAtt);
    }

    public int RandomMag()
    {
        return Random.Range(minMag, maxMag);
    }
}
