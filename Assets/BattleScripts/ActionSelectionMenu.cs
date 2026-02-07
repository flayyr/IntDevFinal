using UnityEngine;

public class ActionSelectionMenu : MonoBehaviour
{
    [SerializeField] MenuItem AttackMenuItem;
    [SerializeField] MenuItem DefendMenuItem;
    [SerializeField] MenuItem CompetenceMenuItem;
    [SerializeField] CharacterMenuItem ActingCharacter;

    MenuItem[] menuItems = new MenuItem[3];
    int selectedIndex = 0;

    private void OnEnable() {
        menuItems[0] = AttackMenuItem;
        menuItems[1] = DefendMenuItem;
        menuItems[2] = CompetenceMenuItem;
    }

    public void Show(PlayerEntity entity) {
        gameObject.SetActive(true);
        ActingCharacter.SetEntity(entity);
        SetShowOnSelectObj(entity.critting);

        SelectItem(0);
    }

    void SetShowOnSelectObj(bool condition)
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].enableShowOnSelectObj = condition;
        }
    }

    public int SelectItem(int index) {
        int newIndex = (index+3) % 3;

        menuItems[selectedIndex].DeselectItem();
        menuItems[newIndex].SelectItem();
        selectedIndex = newIndex;
        return selectedIndex;
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
