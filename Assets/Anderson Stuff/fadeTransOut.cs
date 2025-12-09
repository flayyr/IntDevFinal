using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeTransOut : MonoBehaviour
{

    public Image square;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        square.CrossFadeAlpha(0f, 0.2f, false);
    }
}
