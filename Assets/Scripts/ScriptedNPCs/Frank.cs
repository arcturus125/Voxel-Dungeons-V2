using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frank : MonoBehaviour
{
    public static Dialogue hiFrank;
    public bool isWaving = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        hiFrank = new Dialogue("Hi, I'm Frank");
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
    }

    public void Use()
    {
        anim.SetBool("isWaving", true);
        hiFrank.ShowDialogue();
    }

    public void NoLongerNearby()
    {
        hiFrank.HideDialogue();
        anim.SetBool("isWaving", false);
    }
}
