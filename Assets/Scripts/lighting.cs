using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class lighting : MonoBehaviour
{

    public float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, Speed, 0);
    }
}
