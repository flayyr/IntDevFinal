using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class zoomIn : MonoBehaviour
{

    public Camera cam;
    public GameObject white;
    public string sceneToGo;

    public Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0f);

    public GameObject uiGrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.orthographicSize >= 0)
        {
            cam.orthographicSize -= 0.1f;
            uiGrow.transform.localScale -= scaleChange;
        }

        if(cam.orthographicSize < 0)
        {
            //SceneManager.LoadScene(sceneToGo);

            if (!white.activeInHierarchy)
            {
                white.SetActive(true);
            }

        }
    }
}
