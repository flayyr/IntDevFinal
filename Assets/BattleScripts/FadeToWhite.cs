using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToWhite : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1;
    public bool fading = false;
    float alpha=0;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (fading)
        {
            alpha += fadeSpeed * Time.deltaTime;
            image.color = new Color(1,1,1,alpha );
            if(alpha >= 1)
            {
                SceneManager.LoadScene("BossDefeatRoom");
            }
        }
    }
}
