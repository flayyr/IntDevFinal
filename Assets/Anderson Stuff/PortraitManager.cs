using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{

    [SerializeField] Sprite portrait;
    public Image portraitHolder;

    private void OnEnable()
    {
        portraitHolder.sprite = portrait;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
