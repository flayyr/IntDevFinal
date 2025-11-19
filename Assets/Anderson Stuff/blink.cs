using UnityEngine;
using UnityEngine.UI;

public class blink : MonoBehaviour
{

    bool off = false;
    int offTimer = 240;
    public Image buttonSprite;
    public SpriteRenderer spriteRender;

    // Update is called once per frame
    void Update()
    {
        offTimer--;
        if(offTimer <= 0)
        {
            if (off)
            {
                buttonSprite.color = new Color32(255, 255, 225, 255);
                offTimer = 240;
                off = false;
            }
            else
            {
                buttonSprite.color = new Color32(0, 0, 0, 0);
                offTimer = 240;
                off = true;
            }
        }
    }
}
