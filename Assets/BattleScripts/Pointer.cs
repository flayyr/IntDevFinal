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
        nameText.text = entity.name;
    }

    public void Hide()
    {
        transform.position = new Vector3(-100, -100, 0);
        nameText.transform.position = transform.position + textOffSet;
    }

}
