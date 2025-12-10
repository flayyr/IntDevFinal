using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{

    [SerializeField] string levelGoTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(levelGoTo);
            //SceneManager.LoadScene("TitleScene");
        }
    }
}
