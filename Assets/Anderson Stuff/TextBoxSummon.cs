using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxSummon : MonoBehaviour
{

    [SerializeField] public GameObject textBoxPrefab;

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
    }
}
