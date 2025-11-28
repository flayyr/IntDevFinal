using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Pointer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Vector3 textOffSet;

    public void SetSelection(Entity entity)
    {
        transform.position = entity.transform.position;
        nameText.transform.position = transform.position + textOffSet;
        nameText.text = entity.entityName;
        gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        Destroy(gameObject);
    }

    public void SetSelection(MenuItem item) {
        transform.position = item.transform.position;
        nameText.transform.position = new Vector3(-100, -100, 0);
    }

    public void Hide()
    {
        transform.position = new Vector3(-100, -100, 0);
        nameText.transform.position = transform.position + textOffSet;
    }

}
