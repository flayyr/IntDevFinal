using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Entity : MonoBehaviour
{
    static float moveSpeed = 5f;
    static float waitTimeAfterMove = 0.5f;
    static float defendingMultiplier = 3f;

    [SerializeField] public int maxHP;
    [SerializeField] public int maxCC;
    [SerializeField] int attackPower;
    [SerializeField] float baseDefense;
    [SerializeField, Range(0, 1)] float baseAgility = 0.2f;
    [SerializeField] public CompetenceSO[] competences;

    [HideInInspector] public int hp;
    [HideInInspector] public int cc;
    [HideInInspector] public float progress;
    [HideInInspector] public bool[] statuses;
    protected float agility;
    protected float defense;
    
    

    private void Awake()
    {
        hp = maxHP;
        cc = maxCC;
        agility = baseAgility;
        statuses = new bool[4];
    }

    protected virtual void Update() {
        if (BattleManager.Instance.state == BattleState.idle) {
            progress += agility * Time.deltaTime;
        }
    }

    public void ChangeHP(int change)
    {
        int hpChange = change;
        if (hp + change > maxHP) {
            hpChange = maxHP - hp;
            hp = maxHP;
        } else {
            hp += change;
        }
        GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
        damageText.GetComponent<ScoreTextScript>().SetDamage(hpChange);
        damageText.transform.position = transform.position;

        defense = baseDefense;
    }

    public void ApplyStatus(CompetenceSO competence) {
        for (int i = 0; i < competence.statuses.Length; i++) {
            //Set Defending (not included in statuses array to avoid showing description)
            if (competence.statuses[i] == Status.Defending) {
                defense = baseDefense * defendingMultiplier;

            } else if (statuses[(int)competence.statuses[i]] == competence.cures) {
                if (competence.cures) {
                    Debug.Log(name + " is no longer "+ competence.statuses[i].ToString());
                    statuses[(int)competence.statuses[i]] = false;
                } else {
                    Debug.Log(name + " is inflicted with " + competence.statuses[i].ToString());
                    statuses[(int)competence.statuses[i]] = true;
                }
            }
        }

        //Set Agility
        if (statuses[(int)Status.Lethargic] && statuses[(int)Status.Hasty]) {
            agility = baseAgility;
        } else if (statuses[(int)Status.Lethargic]) {
            agility = 0.5f * baseAgility;
        } else if (statuses[(int)Status.Hasty]) {
            agility = 2f * baseAgility;
        }
    }

    public void UseCompetence(CompetenceSO competence, Entity target)
    {
        cc -= competence.ccCost;
        StartCoroutine(AnimationCoroutine(target, competence.moveAmount, competence));
    }

    public void UseMultiTargetCompetence(CompetenceSO competence, List<Entity> targets)
    {
        cc -= competence.ccCost;
        StartCoroutine(MultiTargetAnimationCoroutine(targets, competence.moveAmount, competence));
    }

    void HitTarget(CompetenceSO competence, Entity target)
    {
        if(competence.effect != null)
        {
            Instantiate(competence.effect, target.transform);
        }
        target.ChangeHP(competence.hpChange);
        target.ApplyStatus(competence);
    }

    protected void Attack(Entity target)
    {
        target.ChangeHP(-attackPower);
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

        HitTarget(competence, targetEntity);
        yield return new WaitForSeconds(waitTimeAfterMove);

        while (Mathf.Abs((originalPos - transform.position).magnitude) > 0.1f)
        {
            transform.position -= dir * Time.deltaTime * moveSpeed * (originalPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = originalPos;

        BattleManager.Instance.SwitchState(BattleState.idle);
        yield return null;
    }

    IEnumerator MultiTargetAnimationCoroutine(List<Entity> targetEntities, float moveAmount, CompetenceSO competence)
    {
        Vector3 dir = (Vector3.zero - transform.position).normalized;
        Vector3 targetPos = transform.position + dir * Mathf.Abs(moveAmount);//No negative moveamount for multitarget
        Vector3 originalPos = transform.position;

        while (Mathf.Abs((targetPos - transform.position).magnitude) > 0.5f)
        {
            transform.position += dir * Time.deltaTime * moveSpeed * (targetPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foreach (Entity entity in targetEntities)
        {
            HitTarget(competence, entity);
        }

        yield return new WaitForSeconds(waitTimeAfterMove);

        while (Mathf.Abs((originalPos - transform.position).magnitude) > 0.1f)
        {
            transform.position -= dir * Time.deltaTime * moveSpeed * (originalPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = originalPos;

        BattleManager.Instance.SwitchState(BattleState.idle);
        yield return null;
    }

}
