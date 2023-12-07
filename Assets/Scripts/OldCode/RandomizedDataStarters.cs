using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedDataStarters : MonoBehaviour
{
    //-------------------------------
    // PLAYER DATA. This is all the info about the player.
    // Contains stuff that changes and things that don't/
    //-----------------------------
    [Header("Player Data")]
    public PlayerData playerData;

    //---------------------------------
    //CLASS DATA. This is all stuff that gets used, but not really manipulated
    //-------------------------------------
    [Header("Base Class Data")]
    public JobData rogueData;
    public JobData mageData;
    public JobData healerData;
    public JobData barbarianData;
    public JobData sworderData;
    public JobData archerData;
    public JobData druidData;

    private JobData chosenJobData; //helps so no more super switch statements
    

    //Jobs, just written out long form. THESE NEED TO MATCH EXACTLY TO HOW YOU TYPE THEM FOR YOUR SWITCH STATEMENT
    private string[] jobOptions = new string[] { "Rogue", "Mage", "Healer", "Barbarian", "Sworder", "Archer", "Druid" };



    //-----------------------------------------------------------------------------------------------------------
    // We need this to be called on Awake so things are already set up before start
    void Awake()
    {
        //Setting Up the Title/Class of the character
        string chosenJob = JobPicker(jobOptions); //pick a job
        chosenJobData = JobReference(chosenJob); //set a reference to the job object
        string[] fulltitle = TitlePicker(chosenJobData); //get the Modifier and Title (as an array)
        playerData.jobMod = fulltitle[0]; //Set the modifier
        playerData.displayJob = fulltitle[1]; //Set the Title


        //Setting up the NumericStats
        playerData.SetHP(chosenJobData.RandomHP());
        playerData.GetHP();
        //Debug.Log(playerData.hp);
        playerData.SetDP(chosenJobData.RandomDef());
        playerData.SetAP(chosenJobData.RandomAtt());
        playerData.SetMP(chosenJobData.RandomMag());


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Setting Up the Title/Class of the character
            string chosenJob = JobPicker(jobOptions); //pick a job
            chosenJobData = JobReference(chosenJob); //set a reference to the job object
            string[] fulltitle = TitlePicker(chosenJobData); //get the Modifier and Title (as an array)
            playerData.jobMod = fulltitle[0]; //Set the modifier
            playerData.displayJob = fulltitle[1]; //Set the Title

            //Setting up the NumericStats
            playerData.SetHP(chosenJobData.RandomHP());
            playerData.GetHP();
            //Debug.Log(playerData.hp);
            playerData.SetDP(chosenJobData.RandomDef());
            playerData.SetAP(chosenJobData.RandomAtt());
            playerData.SetMP(chosenJobData.RandomMag());

        }
    }


    //---------------------------------------------------------------------------------------------
    //picks the starting class/job from the list. Sets that as the job in the player data.
    //Assigns the new job to the player data, though it doesn't have to.
    //Returns the string of the job
    private string JobPicker( string[] jobs )
    {
        string newJob = jobs[Random.Range(0, jobs.Length - 1)]; // creating this variable self contains the script
        playerData.job = newJob;
        
        return newJob;
    }


    //--------------------------------------------------------------------------------------------
    //creates a reference to which job file we'll be using for everything.
    // takes the STRING of the class job to check in a switch statement.
    private JobData JobReference(string jobTitle)
    {
        switch (jobTitle)
        {
            case "Rogue":
                chosenJobData = rogueData;
                break;
            case "Mage":
                chosenJobData = mageData;
                break;
            case "Healer":
                chosenJobData = healerData;
                break;
            case "Barbarian":
                chosenJobData = barbarianData;
                break;
            case "Sworder":
                chosenJobData = sworderData;
                break;
            case "Archer":
                chosenJobData = archerData;
                break;
            case "Druid":
                chosenJobData = druidData;
                break;
        }
        return chosenJobData;
    }
 

    //--------------------------------------------------------------------------------------------------
    // returns the appropriate modifier and display job title for the class but does NOT assign them.
    // returns them as a string array (modifier in slot 0, title in slot 1)
    private string[] TitlePicker(JobData job)
    {
        string displayMod   = "";
        string displayTitle = "";
        
        displayMod   = job.modifiers[Random.Range(0, job.modifiers.Length - 1)];
        displayTitle = job.titles[Random.Range(0, job.titles.Length - 1)];

        //Debug.Log(displayMod);
        return new string[] { displayMod, displayTitle };

    }

    private int StartingHP (int minHP, int maxHP)
    {
        int total = 0;
        total = Random.Range(minHP, maxHP);
        Debug.Log(total);
        return total;
    }
    private int StartingDef (int minDef, int maxDef)
    {
        int total = 0;
        total = Random.Range(minDef, maxDef);
        return total;
    }

    private int StartingAtt (int minAtt, int maxAtt)
    {
        int total = 0;
        total = Random.Range(minAtt, maxAtt);
        return total;
    }

    private int StartingMag(int minMag, int maxMag)
    {
        int total = 0;
        total = Random.Range(minMag, maxMag);
        return total;
    }


}
