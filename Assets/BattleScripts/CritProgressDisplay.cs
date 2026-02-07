using UnityEngine;
using UnityEngine.UI;

public class CritProgressDisplay : MonoBehaviour
{
    enum State {deactivated, starting, playing }

    [SerializeField] Sprite[] startSprites;
    [SerializeField] Sprite[] movingSprites;
    [SerializeField] float tickRate = 0.1f;
    bool active = false;
    int spriteIndex;
    float timer = 0f;

    Image image;
    State state = State.deactivated;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateSprite()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        if (timer>0f)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (state == State.deactivated)
        {
            state = State.starting;
            image.sprite = startSprites[0];
            spriteIndex = 0;
        }
        else if (state == State.starting)
        {
            spriteIndex++;
            if (spriteIndex >= startSprites.Length)
            {
                state = State.playing;
                spriteIndex = 0;
            }
            else
            {
                image.sprite = startSprites[spriteIndex];
            }
            
        }
        else if (state==State.playing)
        {
            spriteIndex++;
            spriteIndex %= movingSprites.Length;
            image.sprite = movingSprites[spriteIndex];
        }
        timer += tickRate;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        spriteIndex = 0;
    }
}
