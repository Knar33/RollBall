using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AsciiRender : MonoBehaviour
{
    public CameraController renderCam;
    public RenderTexture renderTexture;
    public Text prefabText;
    private Text[,] asciiArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        asciiArray = new Text[96,54];
        for (int x = 0; x < 96; x++)
        {
            for (int y = 0; y < 54; y++)
            {
                var newText = Instantiate(prefabText, new Vector3(transform.position.x + (20 * x), transform.position.y + (20 * y), transform.position.z), Quaternion.identity);
                newText.transform.parent = gameObject.transform;
                asciiArray[x,y] = newText;
            }
        }
    }

    void Update()
    {
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        //Color[] renderGrid = tex2d.GetPixels(0, 0, 96, 54);
        
        for (int x = 0; x < 96; x++)
        {
            for (int y = 0; y < 54; y++)
            {
                asciiArray[x,y].text = getGreyscaleChar(Convert.ToDouble(tex2d.GetPixel(x, y).grayscale)).ToString();
            }
        }
    }

    public char getGreyscaleChar(double hue)
    {
        int charPos = Convert.ToInt32(Math.Ceiling(hue * 70));
        return greyscaleAscii[charPos];
    }
}
