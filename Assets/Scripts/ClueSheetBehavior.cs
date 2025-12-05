using UnityEngine;

public class ClueSheetBehavior : MonoBehaviour
{
    public GameObject sheet;
    public GameObject player;
    PlayerMovement plscript;

    void Start()
    {
        plscript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(sheet.activeSelf&&Input.GetKeyDown(KeyCode.Escape)){
            sheet.SetActive(false);
            plscript.moveSpeed=8.0f;

        }
    }
}
