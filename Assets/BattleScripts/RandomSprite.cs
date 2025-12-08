using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite[] sprites;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
