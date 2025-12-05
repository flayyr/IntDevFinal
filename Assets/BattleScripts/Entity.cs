using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Entity : MonoBehaviour
{
    static float moveSpeed = 15f;
    static float waitTimeAfterMove = 0.3f;
    static int statusDuration = 4;

    [SerializeField] public string entityName;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxCC;
    [SerializeField] int ATK;
    [SerializeField] int DEF;
    [SerializeField] int ESP;
    [SerializeField, Range(0, 1)] float baseAgility = 0.2f;
    [SerializeField] public CompetenceSO[] competences;

    public int hp;
    public int cc;
    [HideInInspector] public float progress;
    [HideInInspector] public bool[] statuses;
    [HideInInspector] public int[] statusDurations;
    [HideInInspector] public bool wideAngled = false;
    protected float agility;
    protected bool defending;
    public bool dead;
    

    protected virtual void Awake()
    {
        hp = maxHP;
        cc = maxCC;
        agility = baseAgility;
        statuses = new bool[10];
        statusDurations = new int[10];
        foreach(CompetenceSO competence in competences) {
            competence.turnsSinceUse = 100;
        }
    }

    protected virtual void Update() {
        if (BattleManager.Instance.state == BattleState.idle && !dead) {
            progress += agility * Time.deltaTime;
        }
    }

    public virtual void ChangeHP(int change)
    {
        if (dead)
        {
            return;
        }
        int hpChange = change;
        if (hp + change > maxHP) {
            hpChange = maxHP - hp;
            hp = maxHP;
        } else if (hp + change < 0)
        {
            hpChange = -hp;
            hp = 0;
        }
        else
        {
            hp += change;
        }
        GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
        damageText.GetComponent<ScoreTextScript>().SetDamage(hpChange);
        damageText.transform.position = transform.position;

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    void ChangeCC(int change) {
        int ccChange = change;
        if (cc + change > maxCC) {
            ccChange = maxCC - cc;
            cc = maxCC;
        } else if (cc + change < 0) {
            ccChange = -cc;
            cc = 0;
        } else {
            cc += change;
        }

        GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
        damageText.GetComponent<ScoreTextScript>().SetCompetence(ccChange);
        damageText.transform.position = transform.position;
    }

    protected virtual void Die()
    {
        dead = true;
    }

    protected virtual void Revive()
    {
        dead = false;
    }

    public void ApplyStatus(CompetenceSO competence) {
        for (int i = 0; i < competence.statuses.Length; i++) {
            //Set Defending (not included in statuses array to avoid showing description)
            if (competence.statuses[i] == Status.Defending) {
                defending = true;

            } else if (competence.statuses[i] == Status.WideAngle) {
                wideAngled = true;
            }
            else if (competence.statuses[i] == Status.Revive)
            {
                Revive();
            }
            else if (statuses[(int)competence.statuses[i]] == competence.cures) {
                if (competence.cures) {
                    DescriptionText.Instance.QueueText(entityName + " is no longer " + competence.statuses[i].ToString() + "!");
                    statuses[(int)competence.statuses[i]] = false;
                    statusDurations[(int)competence.statuses[i]] = 0;
                } else {
                    if (Random.value < competence.inflictChance) {
                        DescriptionText.Instance.QueueText(entityName + " is " + competence.statuses[i].ToString() + "!");
                        statuses[(int)competence.statuses[i]] = true;
                        statusDurations[(int)competence.statuses[i]] = competence.statusDuration;
                    }
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
        DescriptionText.Instance.QueueText(entityName + " used "+ competence.name + "!");
        StartCoroutine(AnimationCoroutine(target, competence.moveAmount, competence));
        
    }

    public void UseMultiTargetCompetence(CompetenceSO competence, List<Entity> targets)
    {
        cc -= competence.ccCost;
        DescriptionText.Instance.QueueText(entityName + " used " + competence.name + "!");
        StartCoroutine(MultiTargetAnimationCoroutine(targets, competence.moveAmount, competence));
        
    }

    void AfterMoveUpdate(CompetenceSO competence) {
        IncrementCompetenceTurnsSinceUse(competence);
        for (int i = 0; i<statusDurations.Length; i++) {
            if(statusDurations[i] > 0) {
                statusDurations[i]--;
                if (statusDurations[i] == 0) {
                    statuses[i] = false;
                    DescriptionText.Instance.QueueText(name + " is no longer " + ((Status)i).ToString() + "!");
                }
            }
            
        }

        if (statuses[(int)Status.Poisoned]) {
            ChangeHP(Mathf.CeilToInt(-maxHP * (maxHP>300 ? 0.03f:0.07f)));
        }
        if (statuses[(int)Status.Incompetent])
        {
            ChangeCC(-Mathf.CeilToInt(maxCC * 0.06f));
        }
    }

    void IncrementCompetenceTurnsSinceUse(CompetenceSO usingCompetence) {
        foreach (CompetenceSO competence in competences) {
            if (competence == usingCompetence) {
                usingCompetence.turnsSinceUse = 0;
            } else {
                competence.turnsSinceUse++;
            }
        }
    }

    void HitTarget(CompetenceSO competence, Entity target)
    {
        if (Random.value > competence.hitChance) {
            GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
            damageText.GetComponent<ScoreTextScript>().SetMiss();
            damageText.transform.position = target.transform.position;
            return;
        }
        if(competence.effect != null)
        {
            Instantiate(competence.effect, target.transform.position, Quaternion.identity);
        }

        target.ApplyStatus(competence);
        target.Hit(competence, this);
    }

    public void Hit(CompetenceSO competence, Entity source) {
        int hpChange = 0;
        if (competence.hpChange < 0) {
            hpChange = competence.hpChange - Mathf.CeilToInt(source.ATK * competence.ATKInfluence + source.ESP * competence.ESPInfluence - DEF * competence.DEFInfluence);
            if (hpChange >= 0) {
                hpChange = 0;
            }
            if (defending) {
                hpChange /= 2;
                defending = false;
            }
            if (source.statuses[(int)Status.Strengthened]) {
                hpChange = Mathf.CeilToInt( hpChange * 1.2f);
            }
        }else
        {
            hpChange = competence.hpChange + Mathf.CeilToInt(source.ATK * competence.ATKInfluence + source.ESP * competence.ESPInfluence + DEF * competence.DEFInfluence + agility * competence.AGIInfluence);
        }
        hpChange = Mathf.RoundToInt(hpChange * Random.Range(1f - competence.variance, 1f + competence.variance));
        ChangeHP(hpChange);

        if (competence.CPChange != 0) {
            

            
        }
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

        AfterMoveUpdate(competence);

        BattleManager.Instance.actionDone = true;
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

        AfterMoveUpdate(competence);

        BattleManager.Instance.actionDone = true;
        yield return null;
    }

}
