using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] float speed=1;
    [SerializeField] Color damageColor;
    [SerializeField] Color healColor;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int dmg)
    {
        text.text = Mathf.Abs(dmg)+"";
        if (dmg < 0)
        {
            text.color = damageColor;
        } else
        {
            text.color = healColor;
        }
    }
}
