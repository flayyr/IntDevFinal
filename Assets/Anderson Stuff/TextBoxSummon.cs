using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxSummon : MonoBehaviour
{

    [SerializeField] public GameObject textBoxPrefab;
    [SerializeField] public GameObject imagePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(!textBoxPrefab.activeInHierarchy)
            {
                textBoxPrefab.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!imagePrefab.activeInHierarchy)
            {
                imagePrefab.SetActive(true);
            }
            else
            {
                imagePrefab.SetActive(false);
            }
        }
    }
}
