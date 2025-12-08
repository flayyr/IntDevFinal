using UnityEngine;


public class ClockFace : MonoBehaviour
{
    float clockGoal;
    public Sprite clockImage;
    public Sprite clockImage2;
    public Sprite clockImage3;
    public Sprite clockImage4;
    public Sprite clockImage5;
    public Sprite clockImage6;
    public Sprite clockImage7;
    public Sprite clockImage8;
    public Sprite clockImage9;
    public Sprite clockImage10;
    public Sprite clockImage11;
    public Sprite clockImage12;

    [SerializeField] private Transform clockTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = clockImage;
        clockTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("ChronoHand").GetComponent<Chronomancy>().testingInProgress==true){
            clockTransform.transform.position = new Vector3(0,2,1);
            float clockGoal = GameObject.Find("ChronoHand").GetComponent<Chronomancy>().chronoGoal/30;
            if(clockGoal==1){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage;}
            else if(clockGoal==2){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage2;}
            else if(clockGoal==3){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage3;}
            else if(clockGoal==4){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage4;}
            else if(clockGoal==5){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage5;}
            else if(clockGoal==6){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage6;}
            else if(clockGoal==7){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage7;}
            else if(clockGoal==8){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage8;}
            else if(clockGoal==9){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage9;}
            else if(clockGoal==10){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage10;}
            else if(clockGoal==11){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage11;}
            else if(clockGoal==12){gameObject.GetComponent<SpriteRenderer>().sprite = clockImage12;}
        }
        else {
            clockTransform.transform.position = new Vector3(0,12,1);
        }
    }
}
