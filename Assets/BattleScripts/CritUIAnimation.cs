using UnityEngine;
using UnityEngine.UI;

public class CritUIAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    Image image;

    float timer = 0f;
    int spriteIndex = 0;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (timer < 0.15f)
        {
            timer += Time.deltaTime;
            return;
        }

        timer -= 0.15f;
        image.sprite = sprites[spriteIndex];
        spriteIndex++;
        spriteIndex %= sprites.Length;
    }
}
