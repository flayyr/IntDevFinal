using UnityEngine;
using System.Collections;
public class PuzzleCube : MonoBehaviour
{
    public bool pressed = false;
    public int number;
    private SpriteRenderer cspriteRenderer;
    PuzzleManager puzzleManager;

    void Start()
    {
        cspriteRenderer = GetComponent<SpriteRenderer>();
        puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzzleManager>();
    }

    void Update()
    {
        if(pressed){
            cspriteRenderer.color = new Color(1f,1f,1f,.5f);
            puzzleManager.numInput[0] = 9;
            puzzleManager.currentSpot += 1;
        }

        if(!pressed){
            cspriteRenderer.color = new Color(1f,1f,1f,1f);
        }
    }
}
