using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    Image[] iconImages = new Image[12];

    [SerializeField] CharacterMenuItem iconReference;
    [SerializeField] Transform iconPosition;
    [SerializeField] float iconWidth;
    [SerializeField] Slider hpBar;

    int iconCount = 0;
    public void ShowNameTag(Entity entity) {
        gameObject.SetActive(true);

        if (entity.wideAngled && hpBar != null) {
            hpBar.gameObject.SetActive(true);
            hpBar.value = (float)entity.hp / entity.maxHP;
        } else if (hpBar != null) {
            hpBar.gameObject.SetActive(false);
        }

        for (int i = 0; i < iconImages.Length; i++) {
            if (iconImages[i] != null) {
                RemoveIcon(i);
            }
        }

        for (int i = 0; i < entity.statuses.Length; i++) {
            if (entity.statuses[i]) {
                AddIcon(i);
            }
        }
    }

    public void HideNameTag() {
        gameObject.SetActive(false);
    }

    void AddIcon(int status) {
        GameObject prefab = Instantiate(iconReference.statusIconPrefab, iconPosition);
        prefab.transform.position = iconPosition.position + Vector3.right * iconWidth * iconCount;
        Image prefabImage = prefab.GetComponent<Image>();
        if (prefabImage != null) {
            prefabImage.sprite = iconReference.iconSprites[status];
            iconImages[status] = prefabImage;
            iconCount++;
        }
    }

    void RemoveIcon(int status) {
        Destroy(iconImages[status].gameObject);
        iconImages[status] = null;
        iconCount--;
    }
}
