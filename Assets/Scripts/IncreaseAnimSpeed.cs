using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAnimSpeed : MonoBehaviour
{
    public void IncreaseSpeed(float inc)
    {
        Animator myAnim = gameObject.GetComponent<Animator>();
        float curSpeed = myAnim.speed;
        myAnim.speed = curSpeed + inc;
    }
}
