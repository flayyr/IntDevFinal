using UnityEngine;

public class ClueSheetBehavior : MonoBehaviour
{
    public GameObject sheet;
    public GameObject player;
    PlayerMovement plscript;
    AudioSource audio;

    void Start()
    {
        plscript = player.GetComponent<PlayerMovement>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(sheet.activeSelf&&Input.GetKeyDown(KeyCode.Escape)){
            audio.Play(0);
            sheet.SetActive(false);
            plscript.moveSpeed=8.0f;
            

        }
    }
}
