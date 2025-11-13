using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class TextBox : MonoBehaviour
{

    //use these variables to determine whether the text box is spawned on the top of the screen or the bottom
    [SerializeField] bool top;
    [SerializeField] bool bottom;

    //[SerializeField] sprites (figure out how to implement that)

    //use these to keep how long a line of text is and which page of the text you're currently on
    [SerializeField] private int textLineLength;
    [SerializeField] private int currentPage = 0;
    [SerializeField] private int maxPages;

    //current letter the text is on, for typewriter style
    [SerializeField] private int currentLetter = 0;
    [SerializeField] private int maxLetter;
    [SerializeField] private int typeSpeed = 1;

    //[SerializeField] Array arrText;
    [SerializeField] private string[] testText = new string[] { "yo!", "i'm testing this out dude", "lets see how it goes", "this is the last line" };

    //[SerializeField] string text;

    private GameObject self;

    public TextMeshProUGUI textBoxDisplay;

    private void Awake()
    {
        self = this.gameObject;
        //textBoxDisplay = GetComponent();
        //textBoxDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "if this is showing up you fucked up";
    }

    void Start()
    {
        //textBoxDisplay = GetComponent<TMP_Text>();
        currentPage = 0;
        currentLetter = 0;

        textBoxDisplay.text = testText[currentPage].ToString();

        //textBoxDisplay.text = "if this is showing up you fucked up";

        //textBoxDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "if this is showing up you fucked up";

        maxPages = testText.Length;
        //Debug.Log(maxPages);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Current page is: " + currentPage);
        Debug.Log("the max pages are: " + maxPages);

        if (currentPage < maxPages)
        {
            maxLetter = testText[currentPage].ToString().Length;
            textBoxDisplay.text = testText[currentPage].ToString().Substring(0, currentLetter);

            if(currentLetter < maxLetter)
            {
                currentLetter++;
            }
            else
            {
                currentLetter = maxLetter;
            }
        }

        if (Input.GetKeyDown("space"))
        {

            currentPage++;

            //typewriter style
            if (currentLetter < maxLetter)
            {
                currentLetter = maxLetter;
            }
            else
            {
                currentLetter = 0;
            }

            //kills the box and resets everything
            if (currentPage >= maxPages)
            {
                currentPage = 0;
                currentLetter = 0;
                self.SetActive(false);
            }
        }
    }
}
