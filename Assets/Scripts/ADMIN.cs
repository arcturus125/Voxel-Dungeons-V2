using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ADMIN : MonoBehaviour
{
    public bool DebugMode = false;

    public static bool Debug_Mode;

    void Update()
    {
        Debug_Mode = DebugMode;
    }
}
