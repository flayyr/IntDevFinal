using TMPro;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI menuText;
    [SerializeField] TMP_FontAsset unselectedFont;
    [SerializeField] TMP_FontAsset selectedFont;
    [SerializeField] float unselectedSpacing;
    [SerializeField] float selectedSpacing;
    [SerializeField] Color selectedColor;
    [SerializeField] RectTransform textTransform;
    [SerializeField] float selectedShift;
    
    Animator animator;
    Vector3 originalTextPosition;
    Vector3 selectedTextPosition;

    private void Awake()
    {
        if (textTransform != null)
        {
            originalTextPosition = textTransform.localPosition;
            selectedTextPosition = textTransform.localPosition + Vector3.right * selectedShift;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void SelectItem() {
        menuText.font = selectedFont;
        menuText.color = selectedColor;
        menuText.fontStyle = FontStyles.Bold;
        menuText.characterSpacing = selectedSpacing;
        if (animator != null)
        {
            animator.enabled = true;
        }
        if (textTransform != null)
        {
            textTransform.localPosition = selectedTextPosition;
        }
    }

    public virtual void DeselectItem() {
        menuText.font = unselectedFont;
        menuText.color = Color.white;
        menuText.fontStyle = FontStyles.Normal;
        menuText.characterSpacing = unselectedSpacing;
        if (animator != null)
        {
            animator.enabled = false;
        }
        if (textTransform != null)
        {
            textTransform.localPosition = originalTextPosition;
        }
    }

    public void SetText(string text) {
        if(menuText !=null)
        menuText.text = text;
    }
}
