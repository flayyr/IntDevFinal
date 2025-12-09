using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]SpriteRenderer console;
    [SerializeField]Sprite conti;
    [SerializeField]Sprite quit;
    enum Selection{Continue, Quit};
    Selection currentSelect = Selection.Continue;

    void Update()
    {

        switch(currentSelect){
            case Selection.Continue:
                if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.UpArrow)){
                    currentSelect = Selection.Quit;
                    console.sprite = quit;
                    break;
                }

                if(Input.GetKeyDown(KeyCode.Z)){
                    SceneManager.LoadScene("MainScene");
                    break;
                }
                break;

            case Selection.Quit:
                if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.UpArrow)){
                    currentSelect = Selection.Continue;
                    console.sprite = conti;
                    break;
                }

                if(Input.GetKeyDown(KeyCode.Z)){
                    //Debug.Log("quit game");
                    Application.Quit();
                }
                break;
        }
    }
}
