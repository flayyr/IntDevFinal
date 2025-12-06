using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class zoomIn : MonoBehaviour
{

    public Camera cam;
    public GameObject white;
    public string sceneToGo;

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
