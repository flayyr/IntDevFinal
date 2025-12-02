using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]string thisScene;

    void OnTriggerEnter2D(Collider other)
    {
        SceneManager.LoadScene(thisScene);
    }
}
