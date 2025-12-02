using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    [SerializeField] GameObject descriptionTextBox;
    [SerializeField, Range(0f, 0.2f)] float waitTimePerCharacter;
    [SerializeField, Range(0f, 0.5f)] float waitTimePerLine;
    public static DescriptionText Instance;
    TextMeshProUGUI text;
    Queue<string> textsToDisplay;
    bool displaying;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        textsToDisplay = new Queue<string>();
        Instance = this;
    }

    private void Update()
    {
        if (textsToDisplay.Count > 0 && !displaying)
        {
            BattleManager.Instance.descriptionDone = false;
            displaying = true;
            StartCoroutine(DisplayText(textsToDisplay.Dequeue()));
        }

        if (textsToDisplay.Count == 0 && !displaying)
        {
            BattleManager.Instance.descriptionDone = true;
            descriptionTextBox.SetActive(false);
        }
    }

    public void QueueText(string textToDisplay)
    {
        descriptionTextBox.SetActive(true);
        text.maxVisibleCharacters = 0;
        textsToDisplay.Enqueue(textToDisplay);
    }

    IEnumerator DisplayText(string textToDisplay)
    {
        int numChar = 0;
        text.text = textToDisplay;
        while (numChar < textToDisplay.Length)
        {
            numChar++;
            text.maxVisibleCharacters = numChar;
            yield return new WaitForSeconds(waitTimePerCharacter);
        }
        yield return new WaitForSeconds(waitTimePerLine);
        displaying = false;
    }
}
