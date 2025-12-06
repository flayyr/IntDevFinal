using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class grandientSwitch : MonoBehaviour
{
    Color lerpedColor = Color.white;
    SpriteRenderer sprender;

    public float transTime = 1.5f;
    public float currentTime = 0f;

    public string sceneGoTo;

    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        currentTime += Time.time;

        lerpedColor = Color.Lerp(Color.white, new Color(66f/255f, 59f/255f, 48f/255f), currentTime/transTime);
        sprender.color = lerpedColor;

        if(sprender.color == new Color(66f / 255f, 59f / 255f, 48f / 255f))
        {
            SceneManager.LoadScene(sceneGoTo);
        }
    }
}