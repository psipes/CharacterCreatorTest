using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayData : MonoBehaviour
{
    //----------------------------------------------------
    //
    // Holds all the UI display objects as well as functions
    // for displaying them.
    //
    //----------------------------------------------------
    [Header("UI Objects")]
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI HPNum;
    [SerializeField]
    private TextMeshProUGUI DefNum;
    [SerializeField]
    private TextMeshProUGUI AttNum;
    [SerializeField]
    private TextMeshProUGUI MagNum;


    // Start is called before the first frame update



    private void Start()
    {
    }
    // Late Update so it updates after all the other changes.
    void LateUpdate()
    {
        
    }
 
    //Updates all the text
    public void UpdateText(PlayerData playerData)
    {
        //title is comprised of the modifier/archetype and the display name of the job (like magician, sorcerer, or mage)
        title.text = playerData.jobMod + " " + playerData.displayJob;
        //Health display
        HPNum.text = playerData.GetHP().ToString();
        //Defense Display
        DefNum.text = playerData.GetDP().ToString();
        //Attack Display
        AttNum.text = playerData.GetAP().ToString();
        //Magic Display
        MagNum.text = playerData.GetMP().ToString();

    }

    public void UpdateHealth(PlayerData playerData)
    {
        //Health display
        HPNum.text = playerData.GetHP().ToString();
    }

    public void UpdateDef(PlayerData playerData)
    {
        //Defense Display
        DefNum.text = playerData.GetDP().ToString();
    }

    public void UpdateAtt(PlayerData playerData)
    {
        //Attack Display
        AttNum.text = playerData.GetAP().ToString();
    }

    public void UpdateMag(PlayerData playerData)
    {
        //Magic Display
        MagNum.text = playerData.GetMP().ToString();
    }
}
