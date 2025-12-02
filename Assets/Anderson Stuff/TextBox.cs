using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    //keeps track of page
    [SerializeField] public int index = 0;

    //keeps track of whether or not text is currently being typed
    [SerializeField] private bool typing = true;

    [SerializeField] public AudioSource audioPlayer;

    //how fast the text types
    [SerializeField] private float typeSpeed;

    //actual dialogue
    [SerializeField] public string[] firstText = new string[] { "im sans", "i'm testing this out dude", "lets see how it goes", "this is the last line", "just kidding here's sans", "yo" };
    [SerializeField] public string[] secondText = new string[] { "RAHAHAHAHAHAH", "I took too long" };

    //you're gonna have to edit these in accordance to which image you want to show up, "B" for batter & "F" for Fifth
    [SerializeField] public string[] firstTextImages = new string[] { "B", "F", "F", "F", "F", "B"};
    [SerializeField] public string[] secondTextImages = new string[] { "F", "F" };

    //use this one to determine which string the textbox is using
    [SerializeField] private string[] usedText;

    //use this one to determine which image the textbox should be on
    [SerializeField] private string[] usedPortraits;

    //this determines whether or not you've talked to fifth already
    [SerializeField] bool talked = false;

    //holds the image
    [SerializeField] GameObject portrait;
    [SerializeField] string currentImg;

    //images
    [SerializeField] Sprite batterSprite;
    [SerializeField] Sprite fifthSprite;
    public Image portraitHolder;

    private GameObject self;
    
    public TextMeshProUGUI textBoxDisplay;

    private void Awake()
    {
        self = this.gameObject;
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        typing = false;

        //this set of code determines which dialogue and which portraits to use
        if (!talked)
        {
            //use first set
            usedPortraits = firstTextImages;
            usedText = firstText;

        }
        else
        {
            //use second set
            usedPortraits = secondTextImages;
            usedText = secondText;
        }
        //resets whichever portrait you're on
        currentImg = usedPortraits[0];
        nextSentence();
    }

    void nextSentence()
    {
        if(index < usedText.Length)
        {
            textBoxDisplay.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            index = 0;

            textBoxDisplay.text = "";
            if (!talked)
            {
                talked = true;
            }
            self.SetActive(false);
        }
    }

    IEnumerator WriteSentence()
    {

        textBoxDisplay.maxVisibleCharacters = 0;
        textBoxDisplay.text = usedText[index];

        foreach (char Character in usedText[index].ToCharArray())
        {
            //textBoxDisplay.text += Character;

            textBoxDisplay.maxVisibleCharacters++;

            yield return new WaitForSeconds(typeSpeed);
        }
        index++;
        typing = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (typing)
            {
                if (index < usedText.Length)
                {
                    currentImg = usedPortraits[index];
                    
                    if (index > 0 && currentImg == "F" && usedPortraits[index - 1] == "B")
                    {
                        audioPlayer.Play();
                    }
                    
                }
                typing = false;
                nextSentence();
            }
        }

        //handles portraits
        if (currentImg == "B")
        {
            portraitHolder.sprite = batterSprite;
        }
        else if (currentImg == "F")
        {
            portraitHolder.sprite = fifthSprite;
        }
    }
}
