using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frank : MonoBehaviour
{
    public static Dialogue hiFrank;
    // Start is called before the first frame update
    void Start()
    {
        hiFrank = new Dialogue("Hi, I'm Frank");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        hiFrank.ShowDialogue();
    }

    public void NoLongerNearby()
    {
        hiFrank.HideDialogue();
    }
}
