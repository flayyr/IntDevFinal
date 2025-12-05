using UnityEngine;
using UnityEngine.SceneManagement;

public class FightEnd : MonoBehaviour
{
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;

    [SerializeField] float targetYPos;
    [SerializeField] float moveSpeed;

    bool win = false;

    SpriteRenderer spriteRenderer;

    private void Start() {
        gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EndFight(bool win) {
        this.win = win;
        if (win) {
            spriteRenderer.sprite = winSprite;
        } else {
            spriteRenderer.sprite = loseSprite;
        }
        gameObject.SetActive(true);
    }

    private void Update() {
        if (transform.position.y > targetYPos) {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            if (win) {
                SceneManager.LoadScene("BossRoom");
            } else {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
