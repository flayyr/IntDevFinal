using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxSummon : MonoBehaviour
{

    [SerializeField] public GameObject textBoxPrefab;
    //[SerializeField] public GameObject imagePrefab;

    
    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(!textBoxPrefab.activeInHierarchy)
            {
                textBoxPrefab.SetActive(true);
            }
        }
    }
    */
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("YO");

        if (!textBoxPrefab.activeInHierarchy)
        {
            textBoxPrefab.SetActive(true);
        }
    }

}
