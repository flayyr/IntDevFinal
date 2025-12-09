using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] NameTag[] fifthNameTags;
    [SerializeField] NameTag[] erasedNameTags;


    public void SetSelection(EnemyEntity entity)
    {
        transform.position = entity.transform.position;
        //nameTag.transform.position = transform.position + textOffSet;
        gameObject.SetActive(true);

        HideNameTags();

        int statusCount = 0;

        for (int i = 0; i < entity.statuses.Length; i++) {
            if (entity.statuses[i]) {
                statusCount++;
            }
        }

        SwitchNameTag(entity, statusCount);
    }

    void SwitchNameTag(EnemyEntity entity, int countStatus) {
        if (entity.entityName == "Fifth") {
            if(countStatus >= fifthNameTags.Length) { countStatus = fifthNameTags.Length - 1; }
            fifthNameTags[countStatus].ShowNameTag(entity);
        } else if (entity.entityName == "Erased") {
            if (countStatus >= erasedNameTags.Length) { countStatus = erasedNameTags.Length - 1; }
            erasedNameTags[countStatus].ShowNameTag(entity);
        }
    }

    public void Deselect()
    {
        Destroy(gameObject);
    }

    void HideNameTags() {
        foreach (NameTag tag in fifthNameTags) {
            tag.HideNameTag();
        }
        foreach (NameTag tag in erasedNameTags) {
            tag.HideNameTag();
        }
    }

    public void SetSelection(MenuItem item) {
        transform.position = item.transform.position;
        HideNameTags();
    }

    public void Hide()
    {
        transform.position = new Vector3(-100, -100, 0);
    }

}
