using UnityEngine;



public class Chronomancy : MonoBehaviour
{
    
    [SerializeField] public float chronotest=360.0f;
    [SerializeField] public float chronoGoal=0.0f;
    [SerializeField] public bool testingInProgress=false;
    [SerializeField]private Transform chronoTransform;
    [SerializeField] bool handMoving=false;
    [SerializeField] float timeSpeed=5.0f;
    Rigidbody2D clockHour;

    float timer = 0f;

    void Start()
    {
        chronoTransform = GetComponent<Transform>();
        clockHour = GetComponent<Rigidbody2D>();
        chronoTransform.transform.position = new Vector3(0,12,1);
        //transform.position.y = 8;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);
        if(Input.GetKeyDown(KeyCode.R)) {
            ChronomancyStart();
        }
        if(testingInProgress) {
            timer+= Time.deltaTime;
            //if(oneMoment <= 0) {
            //    ChronomancyEnd();
            //}
            //Debug.Log(chronoTransform.transform.eulerAngles.z);
            if(Input.GetKeyDown(KeyCode.C)) {
                ChronomancyEnd();
            }
            if(chronoTransform.transform.eulerAngles.z < 5f && chronoTransform.transform.eulerAngles.z >0f){
                ChronomancyEnd();
            }
            if(!handMoving && timer>0.3f){
                clockHour.AddTorque(-timeSpeed,ForceMode2D.Impulse);
                handMoving=true;
            }
        }
    }



    void ChronomancyStart() {
        if(!testingInProgress) {
            testingInProgress = true;
            chronoGoal = Random.Range(1,12)*30f;
            //Debug.Log(chronoGoal);
            //oneMoment = 8.0f;
            chronoTransform.transform.position = new Vector3(7,2,1);
            chronoTransform.transform.rotation = new Quaternion(0,0,0,0);
            // timererr???
            timer = 0f;
            
        }
    }

    void ChronomancyEnd() {
        chronotest = Mathf.Abs(chronoTransform.transform.eulerAngles.z-(360-chronoGoal))/180;
        Debug.Log(chronotest);
        chronoTransform.transform.position = new Vector3(0,12,1);
        testingInProgress = false;
        clockHour.angularVelocity=0f;
        timer=0f;
        handMoving=false;
    }

    
}
