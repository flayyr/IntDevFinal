using TMPro;
using UnityEngine;

public class CompetenceMenuItem : MenuItem
{
    [SerializeField] TextMeshProUGUI costText;

    PlayerEntity entity;
    float alpha = 1f;

    public void SetUp(PlayerEntity entity, int index, float alpha) {
        this.entity = entity;
        SetText(entity.competences[index].competenceName);
        SetCostText(entity.competences[index].ccCost);

        if(entity.cc < entity.competences[index].ccCost) {
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
    }

    public override void DeselectItem() {
        base.DeselectItem();
        menuText.alpha = alpha;
        costText.alpha = alpha;
    }

    public void SetCostText(int cost) {
        if (cost == -1) {
            costText.text = "";
        } else {
            costText.text = cost+"";
        }
    }
}
