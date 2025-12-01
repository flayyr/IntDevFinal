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

    private Animator anim;

    //this will hold the values of however long the animation clips are
    public float animTime = 0;
    public float lifeTimer = 0;

    private GameObject self;

    private void OnEnable()
    {

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
        //Debug.Log(animTime);

        Destroy(gameObject, animTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
