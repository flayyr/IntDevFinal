using UnityEngine;
using UnityEngine.UI;

public class CritUIAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float animationDelay = 0.15f;
    Image image;

    float timer = 0f;
    int spriteIndex = 0;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (timer < animationDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        timer -= animationDelay;
        image.sprite = sprites[spriteIndex];
        spriteIndex++;
        spriteIndex %= sprites.Length;
    }
}
