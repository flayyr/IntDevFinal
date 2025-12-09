using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeTrans : MonoBehaviour
{

    //public RawImage border;
    public Image square;

    //public bool fade = false;

    public string scene;

    public GameObject textboxPrefab;

    public float timer = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        square.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!textboxPrefab.activeInHierarchy)
        {
            //border.CrossFadeAlpha(0f, 2f, false);
            square.CrossFadeAlpha(1f, 0.25f, false);
            timer -= Time.deltaTime;
            //fade = true;

            Debug.Log(timer);

            if(timer <= 0)
            {
               SceneManager.LoadScene(scene);
            }

        }
    }
}
