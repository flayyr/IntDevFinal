using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CompetenceMenuItem : MenuItem
{
    [Space(20)]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI maxCCText;
    [SerializeField] GameObject bgbox;
    [SerializeField] GameObject pointer;

    public CompetenceSO competence;
    float alpha = 1f;

    public void SetUp(PlayerEntity entity, int index, float alpha) {
        competence = entity.competences[index];
        SetText(competence.name);
        SetCostText(competence.ccCost);
        SetMaxCCText(entity.maxCC);

        if(entity.cc < competence.ccCost || (entity.statuses.Length>0 && entity.statuses[(int)Status.Muted])) {
            menuText.alpha = alpha;
            costText.alpha = alpha;
            this.alpha = alpha;
        } else {
            menuText.alpha = 1f;
            costText.alpha = 1f;
            this.alpha = 1f;
        }
    }

    public override void SelectItem() {
        base.SelectItem();
        menuText.alpha = alpha;
        costText.alpha = alpha;
        if (bgbox != null) {
            bgbox.SetActive(true);
            pointer.SetActive(true);
        }
    }

    public override void DeselectItem() {
        base.DeselectItem();
        menuText.alpha = alpha;
        costText.alpha = alpha;
        if (bgbox != null) {
            bgbox.SetActive(false);
            pointer.SetActive(false);
        }
    }

    public void SetCostText(int cost) {
        if (cost == -1) {
            costText.text = "";
        } else {
            costText.text = cost+"";
        }
    }

    public void SetMaxCCText(int maxCC) {
        if (maxCCText != null) {
            if (maxCC > 0) {
                maxCCText.text = "/" + maxCC;
            } else {
                maxCCText.text = "";
            }
        }
    }
}
