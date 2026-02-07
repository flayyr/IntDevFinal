using UnityEngine;

public class CritTimerAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    float timer = 0f;
    int spriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (timer < 0.15f)
        {
            timer += Time.deltaTime;
            return;
        }

        timer -= 0.15f;
        spriteRenderer.sprite = sprites[spriteIndex];
        spriteIndex++;
        spriteIndex %= sprites.Length;
    }
}
