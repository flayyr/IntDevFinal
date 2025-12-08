using UnityEngine;

public class LayerManagement : MonoBehaviour
{

    private SpriteRenderer sprender;

    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // int yLayer = Mathf.FloorToInt(transform.position.y);

        // Clamp to valid Unity layer range (0–31)
        // yLayer = Mathf.Clamp(yLayer, 0, 31);

        // Set the object's layer
        // sprender.sortingOrder = yLayer;
    }
}
