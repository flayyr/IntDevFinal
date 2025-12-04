using UnityEngine;

public class ClueSheetBehavior : MonoBehaviour
{
    public GameObject sheet;
    public GameObject player;
<<<<<<< Updated upstream
=======
    PlayerMovement plscript;
>>>>>>> Stashed changes

    void Start()
    {
        plscript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(sheet.activeSelf&&Input.GetKeyDown(KeyCode.Escape)){
            sheet.SetActive(false);
            plscript.moveSpeed=5.0f;

        }
    }
}
