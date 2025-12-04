using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class OWQuitMenu : MonoBehaviour, ISelectionMenu
{
    [SerializeField] CompetenceMenuItem[] menuItems;

    int selectedIndex = 0;

    public void Show() {
        gameObject.SetActive(true);
        SelectItem(0);
    }

    public int SelectItem(int index) {
        int newIndex = (index + menuItems.Length) % menuItems.Length;

        menuItems[selectedIndex].DeselectItem();
        menuItems[newIndex].SelectItem();
        selectedIndex = newIndex;

        return selectedIndex;
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
