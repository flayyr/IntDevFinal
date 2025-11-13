using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxSummon : MonoBehaviour
{

    [SerializeField] public GameObject textBoxPrefab;
    //public string[] testText = new string[] {"yo!", "i'm testing this out dude", "lets see how it goes"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(textBoxPrefab.activeInHierarchy);
        if (Input.GetKeyDown("space"))
        {
            //int numOfTextBoxes = GameObject.FindGameObjectsWithTag("TextBox").Length;
            //if (numOfTextBoxes < 1)
            if(!textBoxPrefab.activeInHierarchy)
            {

                //behold my mess of comments

                //Instantiate(textBoxPrefab);
                //find game object and make it active bro

                //Debug.Log("Happening!");

                //var textBoxPrefabToSummon = GameObject.Find("TextBox");
                //Debug.Log(textBoxPrefabToSummon);

                textBoxPrefab.SetActive(true);
            }
        }
    }
}
