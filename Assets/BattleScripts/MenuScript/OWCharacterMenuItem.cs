using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OWCharacterMenuItem : MenuItem
{
    [Space(40)]
    [SerializeField] PlayerEntity entity;
    [Space(20)]
    [SerializeField] Image portrait;
    [SerializeField] Sprite SelectedSprite;
    [SerializeField] Sprite UnselectedSprite;
    [Space(20)]
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject bubble;
    [Space(20)]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI hpText;
    [Space(20)]
    [SerializeField] Slider CCBar;
    [SerializeField] TextMeshProUGUI ccText;

    private void Update() {
        healthBar.value = (float)entity.hp / entity.maxHP;
        if (hpText != null)
        {
                hpText.text = entity.hp + "/" + entity.maxHP;
                ccText.text = entity.cc + "/" + entity.maxCC;
        }
        CCBar.value = (float)entity.cc / entity.maxCC;
    }

    public override void SelectItem() {
        base.SelectItem();
        portrait.sprite = SelectedSprite;
        pointer.SetActive(true);
        bubble.SetActive(true);
    }

    public override void DeselectItem() {
        base.DeselectItem();
        portrait.sprite = UnselectedSprite;
        pointer.SetActive(false);
        bubble.SetActive(false);
    }

    public void SetEntity(PlayerEntity entity) {
        this.entity = entity;
        SetText(entity.entityName);
    }

}
