using TMPro;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI menuText;
    [SerializeField] TMP_FontAsset unselectedFont;
    [SerializeField] TMP_FontAsset selectedFont;
    [SerializeField] Color selectedColor;

    public virtual void SelectItem() {
        //menuText.font = selectedFont;
        menuText.color = selectedColor;
        menuText.fontStyle = FontStyles.Bold;
    }

    public virtual void DeselectItem() {
        //menuText.font = unselectedFont;
        menuText.color = Color.white;
        menuText.fontStyle = FontStyles.Normal;
    }

    public void SetText(string text) {
        if(menuText !=null)
        menuText.text = text;
    }
}
