using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class TextBox : MonoBehaviour
{

    //use these variables to determine whether the text box is spawned on the top of the screen or the bottom
    [SerializeField] bool top;
    [SerializeField] bool bottom;

    //[SerializeField] sprites (figure out how to implement that)

    //keeps track of page
    [SerializeField] private int index;

    //how fast the text types
    [SerializeField] private float typeSpeed = 0.005f;

    //actual dialogue
    [SerializeField] private string[] testText = new string[] { "yo!", "i'm testing this out dude", "lets see how it goes", "this is the last line" };

    private GameObject self;

    public TextMeshProUGUI textBoxDisplay;

    private void Awake()
    {
        self = this.gameObject;
    }

    void Start()
    {

    }

    void nextSentence()
    {
        if(index < testText.Length)
        {
            textBoxDisplay.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            index = 0;
            textBoxDisplay.text = "";
            self.SetActive(false);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char Character in testText[index].ToCharArray()){
            textBoxDisplay.text += Character;
            yield return new WaitForSeconds(typeSpeed);
        }
        index++;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            nextSentence();
        }
    }
}
