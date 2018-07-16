using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsciiRender : MonoBehaviour
{
    public CameraController renderCam;

    public int[,] asciiArray;

    private void Start()
    {
        asciiArray = new int[96, 54];
    }

    void OnPostRender()
    {

    }
}
