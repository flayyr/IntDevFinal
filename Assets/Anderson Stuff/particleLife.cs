using UnityEngine;
using UnityEngine.UI;

public class particleLife : MonoBehaviour
{

    /*
    [SerializeField] Sprite particleSprite;
    public SpriteRenderer sprender;
    */

    [SerializeField] RuntimeAnimatorController particleAnim;
    [SerializeField] AnimationClip currentAnim;



    [SerializeField] AudioSource audioPlayer;

    private Animator anim;

    //this will hold the values of however long the animation clips are
    public float animTime = 0;
    public float lifeTimer = 0;

    public float spriteOpacity = 1f;

    public bool fade = false;

    private GameObject self;

    private SpriteRenderer sprender;

    private void Awake()
    {
        sprender = GetComponent<SpriteRenderer>(); 
    }

    private void OnEnable()
    {
        if (fade)
        {
            sprender.color = new Color(1f, 1f, 1f, spriteOpacity);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        self = this.gameObject;
        animTime = currentAnim.length;
        anim = GetComponent<Animator>();
        anim.Play(currentAnim.ToString());

        this.GetComponent<Animator>().runtimeAnimatorController = particleAnim as RuntimeAnimatorController;

        Destroy(gameObject, animTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {               
            spriteOpacity -= 0.002f;
            sprender.color = new Color(1f, 1f, 1f, spriteOpacity);
            Debug.Log(spriteOpacity);
        }
    }
}
