using UnityEngine;

public class zoomOut : MonoBehaviour
{

    public Camera cam;
    public GameObject uiGrow;
    public float finalSize = 1f;
    public float currentSize = 0.1f;

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
            /*
            if (uiGrow.transform.localScale != new Vector3(finalSize, finalSize, 1f))
            {
                currentSize += 0.1f;
            }
            */

            if (currentSize < finalSize)
            {
                currentSize += 0.1f;
            }
            else if (currentSize >= finalSize)
            {
                currentSize = 1f;
            }

            uiGrow.transform.localScale = new Vector3(currentSize, currentSize, 1f);

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
