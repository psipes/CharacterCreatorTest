using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard : MonoBehaviour
{
    //THIS CLASS TALKS DIRECTLY TO THE FACE CARD SHADER and PlayerData object
    //It accesses:
    //              -PlayerData Scriptable Object
    //              -JobData Scriptable Object(s)
    //              -FaceCard Shader
    //              -Base Textures (lots of textures)
    //              -UI script (Display Data)

    [Header("Cat Bodies")]
    //gives access to the renderer component
    private Renderer rend;

    //body colors
    [SerializeField] //This allows us to easily put these in via the inspector
    private Texture2D[] catBodies; //these are the textures that are the different colors for the base bodies


    //-------------------------------
    // PLAYER DATA. This is all the info about the player.
    // Contains stuff that changes and things that don't/
    //-----------------------------
    [Header("Player Data")]
    public PlayerData playerData;

    public DisplayData uiDisplay;

    //JobData objects in an array
    [SerializeField]
    private JobData[] jobs;

    private JobData chosenJobData; //reference to the Job object that has been chosen

    private Animator myAnim;

    //metallic colors predefined (colors are on a 0-1 float, rgb. Can also be rgba, if only 3, a is assumed 1)
    //only know your colors on a 256 scale? divide the number by 255 to get the value.
    private Color bronze = new Color(.87f, .59f, .17f);
    private Color gold = new Color(.99f, .76f, .29f);
    private Color whiteGold = new Color(.94f, .87f, .64f);
    private Color roseGold = new Color(.89f, .56f, .53f);
    private Color silver = new Color(.45f, .55f, .6f);
    private Color steel = new Color(.29f, .32f, .35f);
    private Color platinum = Color.white;
    private Color[] metallicColors; //because of "issues" you can't populate the array here

    //eye colors predefined (colors are on a 0-1 float, rgb. Can also be rgba, if only 3, a is assumed 1)
    //only know your colors on a 256 scale? divide the number by 255 to get the value.
    private Color lime = new Color(.59f, 1f, .1f);
    private Color water = new Color(.8f, .99f, 1f);
    private Color china = new Color(.47f, .62f, .82f);
    private Color yellow = new Color(.9f, .84f, .02f);
    private Color brown = new Color(.67f, .55f, .38f);
    private Color purple = new Color(.85f, .68f, 1f);
    private Color orange = new Color(1f, .47f, 0f);
    private Color[] eyeColors; //because of "issues" you can't populate the array here

    //-------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();  
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeCard();
        }
        
    }

    //Flip card when clicked
    private void OnMouseDown()
    {
        if (!myAnim.GetBool("flipIt"))
        {
            myAnim.SetBool("flipIt", true);
            RandomizeCard();
        }
    }

    //-----------------------------------------------------------------------------------------------
    //Create a brand new randomized card
    private void RandomizeCard()
    {
        RandomizeStats();
        RandomizeColors();
        uiDisplay.UpdateText(playerData);
        

    }


    //This sets up the basic numeric and string stats for the card.
    //It picks the class and everything else that isn't purely visual.
    private void RandomizeStats()
    {
        //Setting Up the Title/Class of the character
        chosenJobData = ChooseJob(); //set a reference to the job object
        playerData.job = chosenJobData.jobName;
        playerData.jobMod = chosenJobData.modifiers[Random.Range(0, chosenJobData.modifiers.Length - 1)];
        playerData.displayJob = chosenJobData.titles[Random.Range(0, chosenJobData.titles.Length - 1)];


        //Setting up the NumericStats
        playerData.SetHP(chosenJobData.RandomHP());
        playerData.SetDP(chosenJobData.RandomDef());
        playerData.SetAP(chosenJobData.RandomAtt());
        playerData.SetMP(chosenJobData.RandomMag());

        AssignTextures(chosenJobData);
    }

    //Pick a job, any job...
    private JobData ChooseJob()
    {
        return jobs[Random.Range(0, jobs.Length-1)];
    }

    //take the textures from the job data arrays and save them into the PlayerData
    private void AssignTextures(JobData theJob)
    {
        playerData.cardTextures[2] = theJob.clothes[0];
        playerData.cardTextures[3] = theJob.clothes[1];
        playerData.cardTextures[4] = theJob.clothes[2];
        playerData.cardTextures[5] = theJob.clothes[3];

        playerData.cardTextures[6] = theJob.weapon[0];
        playerData.cardTextures[7] = theJob.weapon[1];
        playerData.cardTextures[8] = theJob.weapon[2];
        playerData.cardTextures[9] = theJob.weapon[3];


    }

    //setup all the colors of things
    //This speaks directly to the shader used, so some of the code looks funky
    //WARNING. THIS NEEDS TO BE CALLED AFTER BASE DATA FOR BEST RESULTS.
    private void RandomizeColors()
    {
        //fill the predefined color arrays
        metallicColors = new Color[] { bronze, gold, whiteGold, roseGold, silver, steel, platinum, platinum };
        eyeColors = new Color[] { lime, water, china, yellow, brown, purple, orange, orange };

        //get the renderer
        rend = GetComponent<Renderer>();

        //This is hamfisted. And not a great way to do this...but since we're working with shaders, 
        //hamfisted kind of happens.
        //NOTE: This calls rend.materials[1]. This is because the mesh has TWO materials on it, so we
        //need to get access to the right one. Which is the one in slot 1. 
        //This replaces the textures in our custom shader with correct textures for the class.
        //This directly calls to the shader and the spelling has to match EXACTLY for variables
        rend.materials[1].SetTexture("_clothesStaticTexture", playerData.cardTextures[2]);
        rend.materials[1].SetTexture("_clothesRandomTexture", playerData.cardTextures[3]);
        rend.materials[1].SetTexture("_clothesMetalTexture", playerData.cardTextures[4]);
        rend.materials[1].SetTexture("_clothesLinesTexture", playerData.cardTextures[5]);
        rend.materials[1].SetTexture("_weaponNormTexture", playerData.cardTextures[6]);
        rend.materials[1].SetTexture("_weaponMetalTexture", playerData.cardTextures[7]);
        rend.materials[1].SetTexture("_weaponRandTexture", playerData.cardTextures[8]);
        rend.materials[1].SetTexture("_weaponLines", playerData.cardTextures[9]);

        //-------------------------------------------------------------------------------------
        //Random.ColorHSV() is an option for a random color...but I'm not sure if I like it.
        //rend.material.SetColor("_eyeColor", (Random.ColorHSV(0f, 1f, .25f, 1f, .5f, 1f)));
        //-------------------------------------------------------------------------------------

        //These call directly to the SHADER VARIABLES
        //fur texture
        rend.materials[1].SetTexture("_bodyTexture", catBodies[Random.Range(0, catBodies.Length - 1)]);

        //Why am I making color variables? Because I might want to use them later or save it
        //eye color
        Color eyeTint = eyeColors[Random.Range(0, eyeColors.Length - 1)];
        rend.materials[1].SetColor("_eyeColor", eyeTint);

        //accent clothing color
        Color accentColor = new Color(Random.Range(0f, .75f), Random.Range(0f, .75f), Random.Range(0f, .75f), 1f);
        rend.materials[1].SetColor("_clothesRandomColor", accentColor);

        //weapon accent color
        //a "quick" formula for finding a complementary color is that every value of two colors should equal 255
        //there are some issues with this, such as red is now partnered with cyan. But it's a decent formula for
        //quick, easy color coordinates that aren't usually the same. Since I throttle it so that the top can't be
        //full 255 (1), I reduce it to so that each value needs to add up to .75f. You can find some other cool 
        //combinations of matching colors by shifting the equation so that it's the .75-b, .75-r, .75-g or
        //.75-g, .75-b, .75-r
        Color weaponRandColor = new Color((.75f - accentColor.r), (.75f - accentColor.g), (.75f - accentColor.b), 1f);
        rend.materials[1].SetColor("_weaponRandColor", weaponRandColor);

        //clothes metal color
        Color clothesMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
        rend.materials[1].SetColor("_clothesMetalColor", clothesMetalColor);

        //weapon metal color
        Color weaponMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
        rend.materials[1].SetColor("_weaponMetal", weaponMetalColor);

    }

}
