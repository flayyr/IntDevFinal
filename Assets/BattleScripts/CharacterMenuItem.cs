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
    [Space(20)]
    [SerializeField] GameObject statusIconPrefab;
    [SerializeField] Sprite[] iconSprites;
    [SerializeField] Transform iconPosition;
    [SerializeField] float iconWidth;

    Image[] iconImages;
    bool[] iconsSet;
    int iconCount = 0;

    PlayerEntity entity;

    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        iconImages = new Image[10];
        iconsSet = new bool[10];
    }

    private void Update() {
        if (progressBar != null) {
            progressBar.value = entity.progress;
        }
        healthBar.value = (float)entity.hp / entity.maxHP;
        hpText.text = entity.hp+"/"+entity.maxHP;
        CCBar.value = (float)entity.cc / entity.maxCC;
        ccText.text = entity.cc + "/" + entity.maxCC;


        if (iconPosition != null) {
            for (int i = 0; i < entity.statuses.Length; i++) {
                if (iconsSet[i] != entity.statuses[i]) {
                    if (entity.statuses[i]) {
                        AddIcon(i);
                    } else {
                        RemoveIcon(i);
                    }
                }
            }
        }
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

    void AddIcon(int status) {
        GameObject prefab = Instantiate(statusIconPrefab, iconPosition);
        prefab.transform.position = iconPosition.position + Vector3.right * iconWidth * iconCount;
        Image prefabImage = prefab.GetComponent<Image>();
        if (prefabImage != null) {
            prefabImage.sprite = iconSprites[status];
            iconImages[status] = prefabImage;
            iconsSet[status] = true;
            iconCount++;
        }
    }

    void RemoveIcon(int status) {
        Destroy( iconImages[status].gameObject);
        iconImages[status] = null;
        iconsSet[status] = false;
        iconCount--;
    }

}
