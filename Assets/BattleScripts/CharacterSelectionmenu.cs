using UnityEngine;

public class CharacterSelectionmenu : MonoBehaviour, ISelectionMenu
{
    [SerializeField]
    CharacterMenuItem[] characterMenuItems;
    [SerializeField]
    Pointer pointer;

    int selectedIndex = 0;

    private void Start() {
        if (BattleManager.Instance != null) {
            for (int i = 0; i < BattleManager.Instance.playerEntities.Length; i++) {
                characterMenuItems[i].SetEntity(BattleManager.Instance.playerEntities[i]);
            }
        } else {
            for (int i = 0; i < characterMenuItems.Length; i++) {
                characterMenuItems[i].SetEntity(OverworldMenuManager.Instance.playerEntities[i]);
            }
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

        if (pointer != null) {
            pointer.SetSelection(characterMenuItems[newIndex]);
        }
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
