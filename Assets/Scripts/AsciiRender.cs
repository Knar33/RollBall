using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AsciiRender : MonoBehaviour
{
    public CameraController renderCam;
    public RenderTexture renderTexture;
    public TextMeshProUGUI prefabText;
    private TextMeshProUGUI[,] asciiArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        asciiArray = new TextMeshProUGUI[320, 180];
        for (int x = 0; x < 320; x++)
        {
            for (int y = 0; y < 180; y++)
            {
                var newText = Instantiate(prefabText, new Vector3(transform.position.x + (6 * x), transform.position.y + (6 * y), transform.position.z), Quaternion.identity);
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

        Color[] renderGrid = tex2d.GetPixels(0, 0, 320, 180);
        
        string asciiText = ""
        for (int x = 0; x < 320; x++)
        {
            for (int y = 0; y < 180; y++)
            {
                asciiArray[x,y].text = getGreyscaleChar(Convert.ToDouble(renderGrid[x + (320 * y)].grayscale)).ToString();
            }
        }
    }

    public char getGreyscaleChar(double hue)
    {
        int charPos = Convert.ToInt32(Math.Ceiling(hue * 69));
        return greyscaleAscii[69 - charPos];
    }
}
