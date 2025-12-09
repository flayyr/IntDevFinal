using System.Collections.Generic;
using UnityEngine;

public class OWCharacterSelectionMenu : MonoBehaviour, ISelectionMenu
{
    [SerializeField]
    List< OWCharacterMenuItem> characterMenuItems;

    int selectedIndex = 0;

    private void Start() {
        if (BattleManager.Instance != null) {
            for (int i = 0; i < BattleManager.Instance.playerEntities.Length; i++) {
                characterMenuItems[i].SetEntity(BattleManager.Instance.playerEntities[i]);
            }
        } else {
            for (int i = 0; i < characterMenuItems.Count; i++) {
                characterMenuItems[i].SetEntity(OverworldMenuManager.Instance.playerEntities[i]);
            }
        }
    }

    public void AddMenuItem(OWCharacterMenuItem menuItem, PlayerEntity entity) {
        characterMenuItems.Add(menuItem);
        menuItem.SetEntity(entity);
        menuItem.gameObject.SetActive(true);
    }

    public void Show() {
        gameObject.SetActive(true);
        SelectItem(0);
    }

    public int SelectItem(int index) {
        int newIndex = (index + characterMenuItems.Count) % characterMenuItems.Count;

        characterMenuItems[selectedIndex].DeselectItem();
        characterMenuItems[newIndex].SelectItem();

        selectedIndex = newIndex;

        return selectedIndex;
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
