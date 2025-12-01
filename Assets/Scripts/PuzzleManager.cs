using UnityEngine;
using System.Collections;
public class PuzzleManager : MonoBehaviour
{
    public int[] numInput = new int[8];
    public int[] passCode = new int[] {1,6,2,4,3,5,8,0};
    public bool solved = false;

    public int currentSpot = 0;
    void Start()
    {
        
    }

    void Update()
    {

    }
}
