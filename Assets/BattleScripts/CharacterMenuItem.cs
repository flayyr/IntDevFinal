using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuItem : MenuItem
{
    [Space(20)]
    [SerializeField] Slider progressBar;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider CCBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI ccText;
    [SerializeField, Range(0f, 1f)] float unselectedAlpha = 0.3f;

    PlayerEntity entity;

    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (progressBar != null) {
            progressBar.value = entity.progress;
        }
        healthBar.value = (float)entity.hp / entity.maxHP;
        hpText.text = entity.hp+"/"+entity.maxHP;
        CCBar.value = (float)entity.cc / entity.maxCC;
        ccText.text = entity.cc + "/" + entity.maxCC;
    }

    public override void SelectItem() {
        base.SelectItem();
        if (canvasGroup != null) {
            canvasGroup.alpha = 1.0f;
        }
    }

    public override void DeselectItem() {
        base.DeselectItem();
        if (canvasGroup != null) {
            canvasGroup.alpha = unselectedAlpha;
        }
    }

    public void SetEntity(PlayerEntity entity) {
        this.entity = entity;
        SetText(entity.entityName);

    }

    public void SetEntity() {
        SetText(entity.entityName);

    }

}
