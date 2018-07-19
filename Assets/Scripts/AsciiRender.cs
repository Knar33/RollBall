using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class AsciiRender : MonoBehaviour
{
    public CameraController renderCam;
    public RenderTexture renderTexture;
    public TextMeshProUGUI renderText;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };
    
    void Update()
    {
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        Color[] renderGrid = tex2d.GetPixels(0, 0, 320, 180);

        StringBuilder sb = new StringBuilder("</mspace>");
        //TODO: try as char array
        
        for (int y = 0; y < 180; y++)
        {
            for (int x = 319; x > 0; x--)
            {
                sb.Append(getGreyscaleChar(Convert.ToDouble(renderGrid[x + (320 * y)].grayscale)).ToString());
            }
            sb.Append("\n");
        }
        sb.Insert(0, "<mspace=6>");
        renderText.text = sb.ToString();
    }

    public char getGreyscaleChar(double hue)
    {
        int charPos = Convert.ToInt32(Math.Ceiling(hue * 69));
        return greyscaleAscii[69 - charPos];
    }
}
