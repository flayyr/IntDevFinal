using System.Collections;
using UnityEngine;


public class Entity : MonoBehaviour
{
    static float moveSpeed = 5f;
    static float waitTimeAfterMove = 0.5f;

    [SerializeField] int maxHP;
    [SerializeField] int maxCC;
    [SerializeField] int attackPower;
    [SerializeField] int baseDefense;
    [SerializeField, Range(0, 1)] float baseAgility = 0.2f;
    [SerializeField] public CompetenceSO[] competences;

    float hp;
    public float cc;
    protected float agility;
    protected int defense;
    protected float progress;

    private void Awake()
    {
        hp = maxHP;
        cc = maxCC;
        agility = baseAgility;
    }

    public void changeHP(int change)
    {
        hp += change;
        Debug.Log(gameObject.name +": "+ hp);
    }

    public void UseCompetence(CompetenceSO competence, Entity target)
    {
        cc -= competence.ccCost;
        StartCoroutine(AnimationCoroutine(target, competence.moveAmount, competence));
    }

    void ApplyEffect(CompetenceSO competence, Entity target)
    {
        if(competence.effect != null)
        {
            Instantiate(competence.effect, target.transform);
        }
        target.changeHP(competence.hpChange);
    }

    protected void Attack(Entity target)
    {
        target.changeHP(-attackPower);
    }

    IEnumerator AnimationCoroutine(Entity targetEntity, float moveAmount, CompetenceSO competence)
    {
        Vector3 dir = (targetEntity.transform.position - transform.position).normalized;
        Vector3 targetPos = transform.position + dir * moveAmount;
        Vector3 originalPos = transform.position;
        if (moveAmount == -1)
        {
            targetPos = targetEntity.transform.position;
        }
        
        while(Mathf.Abs((targetPos - transform.position).magnitude) > 0.5f)
        {
            transform.position += dir * Time.deltaTime * moveSpeed * (targetPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ApplyEffect(competence, targetEntity);
        yield return new WaitForSeconds(waitTimeAfterMove);

        while (Mathf.Abs((originalPos - transform.position).magnitude) > 0.1f)
        {
            transform.position -= dir * Time.deltaTime * moveSpeed * (originalPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = originalPos;
        yield return null;
    }

}
