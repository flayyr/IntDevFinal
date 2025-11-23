using UnityEngine;

public class EnemyTimerAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(float value) {
        int spriteIndex = Mathf.FloorToInt(value * (sprites.Length-1));
        spriteIndex = Mathf.Clamp(spriteIndex, 0, sprites.Length-1);
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
