using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]string thisScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(thisScene);
    }
}
