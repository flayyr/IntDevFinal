using UnityEngine;

public class CharacterSelectionmenu : MonoBehaviour
{
    [SerializeField]
    CharacterMenuItem[] characterMenuItems;
    [SerializeField]
    Pointer pointer;

    int selectedIndex = 0;

    private void Start() {
        for (int i = 0; i < BattleManager.Instance.playerEntities.Length; i++) {
            characterMenuItems[i].SetEntity( BattleManager.Instance.playerEntities[i]);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
        SelectItem(0);
    }

    public int SelectItem(int index) {
        int newIndex = (index + characterMenuItems.Length) % characterMenuItems.Length;

        characterMenuItems[selectedIndex].DeselectItem();
        characterMenuItems[newIndex].SelectItem();
        pointer.SetSelection(characterMenuItems[newIndex]);
        selectedIndex = newIndex;

        return selectedIndex;
    }
    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Default() {
        gameObject.SetActive(true);
        pointer.Hide();
        characterMenuItems[selectedIndex].DeselectItem();
    }
}
