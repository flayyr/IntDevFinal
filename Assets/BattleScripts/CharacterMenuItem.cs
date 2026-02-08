using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuItem : MenuItem
{
    [Space(20)]
    [SerializeField] bool switchPortrait = false;
    [Space(20)]
    [SerializeField] Slider progressBar;
    [SerializeField] CritProgressDisplay critProgressSprite;
    [SerializeField] CritUIAnimation critFullAnimation;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider CCBar;
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI ccText;
    [SerializeField, Range(0f, 1f)] float unselectedAlpha = 0.3f;
    [Space(20)]
    [SerializeField] public GameObject statusIconPrefab;
    [SerializeField] public Sprite[] iconSprites;
    [SerializeField] Transform iconPosition;
    [SerializeField] float iconWidth;
    [SerializeField] bool showMaxHp;

    static int CCBarSize = 16;
    static int HPBarSize = 21;

    Image[] iconImages;
    bool[] iconsSet;
    int iconCount = 0;

    PlayerEntity entity;

    CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        iconImages = new Image[12];
        iconsSet = new bool[12];
    }

    private void Update() {
        if (critProgressSprite != null)
        {
            if (entity.critting && entity.progress>0.15f)
            {
                critProgressSprite.UpdateSprite();
            }
            else
            {
                critProgressSprite.Deactivate();
            }
            if (entity.selectingMove)
            {
                critProgressSprite.Deactivate();
            }
        }

        if(critFullAnimation != null)
        {
            critFullAnimation.gameObject.SetActive(entity.critting && entity.selectingMove);
        }

        if (progressBar != null) {
            progressBar.value = entity.progress;
        }
        healthBar.value = Mathf.Floor(((float)entity.hp / entity.maxHP)*HPBarSize)/CCBarSize;
        if (hpText != null)
        {
            if (showMaxHp)
            {
                hpText.text = entity.hp + "/" + entity.maxHP;
                ccText.text = entity.cc + "/" + entity.maxCC;
            } else
            {
                hpText.text = entity.hp+"";
                ccText.text = entity.cc+"";
            }
        }
        CCBar.value = Mathf.Floor(((float)entity.cc / entity.maxCC)*CCBarSize)/CCBarSize;


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
        if (switchPortrait)
        {
            portrait.sprite = entity.portraitSprite;
        }
    }

    public void SetEntity() {
        SetText(entity.entityName);

    }

    void AddIcon(int status) {
        GameObject prefab = Instantiate(statusIconPrefab, iconPosition);
        prefab.transform.position = iconPosition.position + Vector3.right * iconWidth * (iconCount%4);
        prefab.transform.position += Vector3.up * iconWidth * Mathf.Floor(iconCount / 4);
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
        UpdateIcons();
    }

    void UpdateIcons() {
        int curr = 0;
        foreach (Image icon in iconImages) {
            if (icon != null) {
                icon.transform.position = iconPosition.position + Vector3.right * iconWidth * (curr % 4);
                icon.transform.position += Vector3.up * iconWidth * Mathf.Floor(curr / 4);
                curr++;
            }
        }
    }

}
