using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioClip ping;
    public AudioClip cycleClick;
    public AudioClip selectClick;
    public AudioClip statusHurt;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
