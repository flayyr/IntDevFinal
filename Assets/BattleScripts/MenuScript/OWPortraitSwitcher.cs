using UnityEngine;

public class OWPortraitSwitcher : MonoBehaviour
{
    [SerializeField] OWPortrait[] portraits;

    public void ShowPortraitAtIndex(int index) {
        for(int i = 0; i < portraits.Length; i++) {
            portraits[i].StopAnim();
        }
        portraits[index].StartAnim();
        
    }
}
