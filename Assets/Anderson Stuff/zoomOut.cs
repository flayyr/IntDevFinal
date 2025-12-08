using UnityEngine;

public class zoomOut : MonoBehaviour
{

    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        cam.orthographicSize = 0f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.orthographicSize < 5f)
        {
            cam.orthographicSize += 0.2f;
        }
        else if (cam.orthographicSize >= 5f)
        {
            cam.orthographicSize = 5f;
            gameObject.SetActive(false);
        }
    }
}
