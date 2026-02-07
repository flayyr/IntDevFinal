using UnityEngine;

public class EnemyTimerAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] CritTimerAnimation critAnimation;

    SpriteRenderer spriteRenderer;


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        critAnimation.gameObject.SetActive(false);
    }

    public void UpdateSprite(float value) {
        
        int spriteIndex = Mathf.FloorToInt(value * (sprites.Length - 1));
        spriteIndex = Mathf.Clamp(spriteIndex, 0, sprites.Length-1);
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Crit()
    {
        critAnimation.gameObject.SetActive(true);
    }
    public void RemoveCrit()
    {
        critAnimation.gameObject.SetActive(false);
    }
}
