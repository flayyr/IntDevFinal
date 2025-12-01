using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PuzzleManager : MonoBehaviour
{
    public List<int> numInput = new List<int>();
    public int[] passCode = new int[] {1,6,2,4,3,5,8,0};
    public bool solved = false;

    GameObject[] cubes;
    GameObject[] barriers;

    public int currentSpot = 0;
    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        barriers = GameObject.FindGameObjectsWithTag("Barrier");
    }

    void Update()
    {
        if(numInput.Count == 8){

            string passString = string.Join("", passCode);
            string inputString = string.Join("", numInput);

            if(inputString==passString){
                Debug.Log("u did it");
                for(int i = 0; i<cubes.Length;i++){
                    var p = cubes[i].GetComponent<PuzzleCube>();
                    p.pressed = true;
                }
                for(int i = 0; i<barriers.Length;i++){
                    var col = barriers[i].GetComponent<BoxCollider2D>();
                    Destroy(col);
                }
                solved = true;
            }
            else{
                for(int i = 0; i<cubes.Length;i++){
                    var p = cubes[i].GetComponent<PuzzleCube>();
                    p.pressed = false;
                }
                Debug.Log("u didnt it");
            }
        }
    }
}
