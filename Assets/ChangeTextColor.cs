using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour {

    Image im;
	// Use this for initialization
	void Start () {
        im = GetComponent<Image>();
        Texture2D originalTexture = im.sprite.texture;
        Vector2 textureSize = new Vector2(im.sprite.texture.width, im.sprite.texture.height);
        //textureSize = new Vector2(128, 128);
        Texture2D texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, (int)textureSize.x, (int)textureSize.y), Vector2.zero);

        Color blank = new Color(0, 0, 0, 0);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++) //Goes through each pixel
            {
                Color pixelColour;
                if (originalTexture.GetPixel(x,y).a == 0) {
                    pixelColour = blank;
                }
                else
                {
                    pixelColour = Color.red;
                }
                
                texture.SetPixel(x, y, pixelColour);
            }
        }
        texture.Apply();
        im.sprite = sprite;
        
    }
	
    public static Texture2D ChangeColor(Texture2D t, Color color)
    {
        Texture2D tex_ = new Texture2D(t.width, t.height, TextureFormat.ARGB32, true);
        Color[] colors = t.GetPixels();
        Color pixel;

        for (int i = 0; i < colors.Length; i++)
        {
            Debug.Log(colors[i]);
            if (colors[i].a != 0)
            {
                colors[i] = color;
            }
        }
        tex_.SetPixels(colors);
        tex_.Apply();
        return tex_;
    }
}
