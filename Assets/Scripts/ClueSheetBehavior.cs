using UnityEngine;

public class ClueSheetBehavior : MonoBehaviour
{
    public GameObject sheet;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if(sheet.activeSelf&&Input.GetKeyDown(KeyCode.Escape)){
            sheet.SetActive(false);
        }
    }
}
