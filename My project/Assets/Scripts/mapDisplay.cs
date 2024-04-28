using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapDisplay : MonoBehaviour
{
    public RawImage rawImage;
    
    public void drawTexture(Texture2D texture){
        rawImage.texture = texture;
        rawImage.rectTransform.localScale = new Vector2(texture.width, texture.height);
    }


    public void drawRenderer(Texture2D texture)
    {
        rawImage.texture = texture;
        rawImage.rectTransform.localScale = new Vector3(texture.width, texture.height, 0);
    }
    
}