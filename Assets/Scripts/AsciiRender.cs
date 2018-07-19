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
    private char[] asciiCharArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        asciiCharArray = new char[58257];
    }

    void Update()
    {
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        Color[] renderGrid = tex2d.GetPixels(0, 0, 320, 180);

        asciiCharArray[0] = '<';
        asciiCharArray[1] = 'm';
        asciiCharArray[2] = 's';
        asciiCharArray[3] = 'p';
        asciiCharArray[4] = 'a';
        asciiCharArray[5] = 'c';
        asciiCharArray[6] = 'e';
        asciiCharArray[7] = '=';
        asciiCharArray[8] = '6';
        asciiCharArray[9] = '>';
        asciiCharArray[58248] = '<';
        asciiCharArray[58249] = '/';
        asciiCharArray[58250] = 'm';
        asciiCharArray[58251] = 's';
        asciiCharArray[58252] = 'p';
        asciiCharArray[58253] = 'a';
        asciiCharArray[58254] = 'c';
        asciiCharArray[58255] = 'e';
        asciiCharArray[58256] = '>';
        //TODO: try as char array

        for (int y = 0; y < 180; y++)
        {
            for (int x = 0; x < 320; x++)
            {
                int charSpace = 10 + (2 * y) + x + (320 * y);
                asciiCharArray[charSpace] = getGreyscaleChar(Convert.ToDouble(renderGrid[x + (320 * y)].grayscale));
            }
            asciiCharArray[10 + (2 * y) + 320 + (320 * y)] = '\\';
            asciiCharArray[10 + (2 * y) + 320 + (320 * y) + 1] = 'n';
        }
        string output = new string(asciiCharArray);
        renderText.text = output;
    }

    public char getGreyscaleChar(double hue)
    {
        int charPos = Convert.ToInt32(Math.Ceiling(hue * 69));
        return greyscaleAscii[69 - charPos];
    }
}
