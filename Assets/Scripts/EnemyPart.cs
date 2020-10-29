using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPart : MonoBehaviour
{
    [Header("When Hit:")]
    public bool destroy = false;
    public bool removeHp = false;
    public int hpToRemove = 0;
    public bool Animation = false;
    public string AnimationBoolName = "insert string here";

    private bool animationRun = false;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            Debug.Log("Running hit");
            hit();
        }
        else
        {

            Debug.Log("Something hit me but it was not a weapon");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("Running hit");
            hit();
        }
        else
        {

            Debug.Log("Something hit me but it was not a weapon: " + other.gameObject.name);
        }
    }

    private void hit()
    {
        Debug.Log("Enemy Part hit");
        // hide ("destroy") object when it is hit with a weapon
        if(destroy)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        // damage the obejct when it is hit with a weapon, if HP hits 0, then "destroy" it
        if(removeHp)
        {
            hpToRemove--;
            if(hpToRemove < 0)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        // play an animation when the object is hit with a weapon
        if(animationRun)
        {
            this.GetComponentInParent<Animator>().SetBool(AnimationBoolName, false);
            animationRun = false;
        }
        if(Animation)
        {
            if (AnimationBoolName != "insert string here")
            {
                this.GetComponentInParent<Animator>().SetBool(AnimationBoolName, true);
                animationRun = true;
            }
        }
    }
}
