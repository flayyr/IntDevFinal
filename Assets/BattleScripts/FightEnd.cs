using UnityEngine;
using UnityEngine.SceneManagement;

public class FightEnd : MonoBehaviour
{
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;

    [SerializeField] float targetYPos;
    [SerializeField] float moveSpeed;

    bool win = false;

    SpriteRenderer renderer;

    private void Start() {
        gameObject.SetActive(false);
        renderer = GetComponent<SpriteRenderer>();
    }

    public void EndFight(bool win) {
        this.win = win;
        if (win) {
            renderer.sprite = winSprite;
        } else {
            renderer.sprite = loseSprite;
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
