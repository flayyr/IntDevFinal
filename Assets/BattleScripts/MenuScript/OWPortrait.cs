using UnityEngine;
using UnityEngine.UI;

public class OWPortrait : MonoBehaviour
{
    Sprite originalSprite;
    Image image;
    Animator animator;

    private void Awake() {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        originalSprite = image.sprite;
    }

    public void StopAnim() {
        animator.enabled = false;
        image.sprite = originalSprite;
    }

    public void StartAnim() {
        animator.enabled = true;
    }
}
