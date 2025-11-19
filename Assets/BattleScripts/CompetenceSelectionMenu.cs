using TMPro;
using UnityEngine;

public class CompetenceSelectionMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField]
    CompetenceMenuItem[] competenceMenuItems;
    [SerializeField, Range(0f, 1f)] float unusableAlpha = 0.3f;

    int selectedIndex = 0;

    Entity entity;

    public void Show(PlayerEntity entity) {
        gameObject.SetActive(true);
        this.entity = entity;

        for (int i = 0; i < competenceMenuItems.Length; i++) {
            if (i < entity.competences.Length) {
                competenceMenuItems[i].SetUp(entity, i, unusableAlpha);
            } else {
                competenceMenuItems[i].SetText("");
                competenceMenuItems[i].SetCostText(-1);
            }
        }

        SelectItem(0);
    }

    public int SelectItem(int index) {
        int newIndex = (index + entity.competences.Length) % entity.competences.Length;

        competenceMenuItems[selectedIndex].DeselectItem();
        competenceMenuItems[newIndex].SelectItem();
        descriptionText.text = competenceMenuItems[newIndex].competence.description;
        selectedIndex = newIndex;

        return selectedIndex;
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
}
