using UnityEngine;

public class zoomOut : MonoBehaviour
{

    public Camera cam;
    public GameObject uiGrow;
    public Vector3 scaleChange = new Vector3(0.04f, 0.04f, 0.04f);

    public bool canvasOrNot;

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

        if (canvasOrNot)
        {
            if (uiGrow.transform.localScale != new Vector3(1f, 1f, 1f))
            {
                uiGrow.transform.localScale += scaleChange;
            }
        }

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
