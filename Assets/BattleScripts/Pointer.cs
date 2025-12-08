using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] RectTransform nameTag;
    [SerializeField] Vector3 textOffSet;
    [SerializeField] Slider hpBar;

    public void SetSelection(Entity entity)
    {
        transform.position = entity.transform.position;
        nameTag.transform.position = transform.position + textOffSet;
        //nameText.text = entity.entityName;
        gameObject.SetActive(true);
        //nameText.gameObject.SetActive(true);
        if(entity.wideAngled && hpBar != null) {
            hpBar.gameObject.SetActive(true);
            hpBar.value = (float)entity.hp / entity.maxHP;
        } else if(hpBar!=null){
            hpBar.gameObject.SetActive(false);
        }
    }

    public void Deselect()
    {
        Destroy(gameObject);
    }

    public void SetSelection(MenuItem item) {
        transform.position = item.transform.position;
        nameTag.transform.position = new Vector3(-100, -100, 0);
    }

    public void Hide()
    {
        transform.position = new Vector3(-100, -100, 0);
        nameTag.transform.position = transform.position + textOffSet;
    }

}
