using UnityEngine;

public class OWPortraitSwitcher : MonoBehaviour
{
    [SerializeField] OWPortrait[] portraits;

    public void ShowPortraitAtIndex(int index) {
        StopAll();
        portraits[index].StartAnim();
        
    }

    public void StopAll() {
        for (int i = 0; i < portraits.Length; i++) {
            if (portraits[i].gameObject.activeInHierarchy) {
                portraits[i].StopAnim();
            }
        }
    }
}
