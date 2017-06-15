using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionFix : MonoBehaviour {

    void SetResolution()
    {
        Screen.SetResolution(360, 640, false);
    }

    void Start()
    {
        SetResolution();
    }
}
