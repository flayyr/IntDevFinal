using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class Entity : MonoBehaviour
{
    static float moveSpeed = 15f;
    static float waitTimeAfterMove = 0.3f;
    static int defendingCompetenceRestore = 10;
    static float hitAnimationFlashRate = 10f;
    static float multihitDelay = 0.2f;
    static float critSpeedMult = 1.3f;
    static float critValueMult = 1.5f;
    static float critRate = 0.05f;

    [SerializeField] public string entityName;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxCC;
    [SerializeField] int ATK;
    [SerializeField] int DEF;
    [SerializeField] int ESP;
    [SerializeField] int baseAgility = 200;
    [SerializeField] public CompetenceSO[] competences;

    public int hp;
    public int cc;
    [HideInInspector] public float progress;
    [HideInInspector] public bool[] statuses;
    [HideInInspector] public int[] statusDurations;
    [HideInInspector] public bool wideAngled = false;
    protected int agility;
    protected int defence;
    protected int attack;
    protected int espirit;

    protected bool defending;
    public bool dead;

    public bool critting;

    [SerializeField]SpriteRenderer spriteRenderer;
    

    protected virtual void Awake()
    {
        hp = maxHP;
        cc = maxCC;
        agility = baseAgility;
        statuses = new bool[12];
        statusDurations = new int[12];
        attack = ATK;
        espirit = ESP;
        defence = DEF;
        foreach(CompetenceSO competence in competences) {
            competence.turnsSinceUse = 100;
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    protected virtual void Update() {
        if (BattleManager.Instance.state == BattleState.idle && !dead) {
            if (critting)
            {
                progress += (agility * critSpeedMult) / 1000f * Time.deltaTime;
            }
            else
            {
                progress += (agility / 1000f) * Time.deltaTime;
            }
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
                ChangeCC(defendingCompetenceRestore);
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
            } else if(!competence.cures){
                if (Random.value < competence.inflictChance)
                {
                    DescriptionText.Instance.QueueText(entityName + " is " + competence.statuses[i].ToString() + " again!");
                    statuses[(int)competence.statuses[i]] = true;
                    statusDurations[(int)competence.statuses[i]] = competence.statusDuration;
                }
            }
        }

        UpdateStats();
    }

    void UpdateStats() {
        //Set Agility
        if (statuses[(int)Status.Lethargic] && statuses[(int)Status.Hasty]) {
            agility = baseAgility;
        } else if (statuses[(int)Status.Lethargic]) {
            agility = Mathf.CeilToInt(0.6f * baseAgility);
        } else if (statuses[(int)Status.Hasty]) {
            agility = Mathf.CeilToInt(1.5f * baseAgility);
        } else {
            agility = baseAgility;
        }

        //Set Defence
        if (statuses[(int)Status.Toughened]) {
            defence = Mathf.CeilToInt(DEF * 1.3f);
        } else {
            defence = DEF;
        }

        //Set Attack
        if (statuses[(int)Status.Strengthened]) {
            attack = Mathf.CeilToInt(ATK * 1.3f);
        } else {
            attack = ATK;
        }

        //Set Espirit
        if (statuses[(int)Status.Clever]) {
            espirit = Mathf.CeilToInt(ESP * 1.3f);
        } else {
            espirit = ESP;
        }
    }

    public void UseCompetence(CompetenceSO competence, Entity target)
    {
        cc -= competence.ccCost;
        DescriptionText.Instance.QueueText(entityName + " used "+ competence.name + "!");
        if (competence.soundEffect != null)
            SFXManager.Instance.PlaySound(competence.soundEffect);

        StartCoroutine(AnimationCoroutine(target, competence.moveAmount, competence));
        
    }

    public void UseMultiTargetCompetence(CompetenceSO competence, List<Entity> targets)
    {
        if (competence.soundEffect != null)
            SFXManager.Instance.PlaySound(competence.soundEffect);
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
                    DescriptionText.Instance.QueueText(entityName + " is no longer " + ((Status)i).ToString() + "!");
                    UpdateStats();
                }
            }
            
        }

        if (statuses[(int)Status.Poisoned]) {
            ChangeHP(Mathf.CeilToInt(-maxHP * (maxHP>300 ? 0.03f:0.07f)));
            SFXManager.Instance.PlaySound(SFXManager.Instance.statusHurt);
        }
        if (statuses[(int)Status.Incompetent])
        {
            ChangeCC(-Mathf.CeilToInt(maxCC * 0.12f));
        }

        if (critting)
        {
            RemoveCrit();
        }else if (Random.value < critRate)
        {
            Crit();
        }
    }

    protected virtual void Crit()
    {
        critting = true;
        SFXManager.Instance.PlaySound(SFXManager.Instance.crit);
    }

    protected virtual void RemoveCrit()
    {
        critting = false;
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
        if (Random.value > competence.hitChance || competence.statusDuration==0) {
            GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
            damageText.GetComponent<ScoreTextScript>().SetText("Miss");
            damageText.transform.position = target.transform.position;
            return;
        }
        for(int i = 0; i<competence.effect.Length; i++) {
            if (competence.effect[i] != null) {
                Instantiate(competence.effect[i], target.transform.position, Quaternion.identity);
            }
        }
        

        target.ApplyStatus(competence);
        target.Hit(competence, this);
    }

    public void Hit(CompetenceSO competence, Entity source) {
        int hpChange = 0;
        if (competence.hpChange < 0) {
            hpChange = competence.hpChange - Mathf.CeilToInt(source.attack * competence.ATKInfluence + source.espirit * competence.ESPInfluence - defence * competence.DEFInfluence);
            if (hpChange >= 0) {
                hpChange = 0;
            }
            if (defending) {
                hpChange /= 2;
                defending = false;
            }

            StartCoroutine(HitAnimation());

        }else
        {
            hpChange = competence.hpChange + Mathf.CeilToInt(source.attack * competence.ATKInfluence + source.espirit * competence.ESPInfluence + defence * competence.DEFInfluence);
        }
        hpChange = Mathf.CeilToInt(hpChange * Random.Range(1f - competence.variance, 1f + competence.variance));
        if (source.critting)
        {
            hpChange = Mathf.CeilToInt(hpChange * critValueMult);
        }
        ChangeHP(hpChange);

        if (competence.CPChange != 0) {
            ChangeCC(Mathf.CeilToInt( competence.CPChange + agility * competence.AGIInfluence));
        }
    }

    IEnumerator HitAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;

        float time = 0f;
        while (time < 0.5f*hitAnimationFlashRate)
        {
            time += Time.deltaTime* hitAnimationFlashRate;
            spriteRenderer.color = new Color(1,1,1, Mathf.Floor(time) % 2);
            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator AnimationCoroutine(Entity targetEntity, float moveAmount, CompetenceSO competence)
    {
        Vector3 dir = (targetEntity.transform.position - transform.position).normalized;
        Vector3 targetPos = transform.position + dir * moveAmount;
        Vector3 originalPos = transform.position;

        if (statuses[(int)Status.Static]) {
            Chronomancy.Instance.ChronomancyStart();
            yield return new WaitUntil(() => !Chronomancy.Instance.testingInProgress);
            float val = Chronomancy.Instance.chronotest;
            if (val > 0.2f) {
                GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
                damageText.GetComponent<ScoreTextScript>().SetText("Stasis");
                damageText.transform.position = transform.position;
                goto AfterHitLabel;
            }
        }

        if (moveAmount == -1)
        {
            targetPos = targetEntity.transform.position;
        }
        
        while(Mathf.Abs((targetPos - transform.position).magnitude) > 0.5f)
        {
            transform.position += dir * Time.deltaTime * moveSpeed * (targetPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (competence.chronomancy) {
            Chronomancy.Instance.ChronomancyStart();
            yield return new WaitUntil(()=>!Chronomancy.Instance.testingInProgress);
            float val = Chronomancy.Instance.chronotest;
            if (val < 0.05f) {
                competence.statusDuration = 4;
            } else if (val < 0.1f) {
                competence.statusDuration = 3;
            } else if (val < 0.2f) {
                competence.statusDuration = 2;
            } else {
                competence.statusDuration = 0;
            }
        }

        HitTarget(competence, targetEntity);

        AfterHitLabel:
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

        if (statuses[(int)Status.Static]) {
            Chronomancy.Instance.ChronomancyStart();
            yield return new WaitUntil(() => !Chronomancy.Instance.testingInProgress);
            float val = Chronomancy.Instance.chronotest;
            if (val > 0.2f) {
                GameObject damageText = Instantiate(BattleManager.Instance.damageTextPrefab, BattleManager.Instance.worldSpaceCanvas.transform);
                damageText.GetComponent<ScoreTextScript>().SetText("Stasis");
                damageText.transform.position = transform.position;
                goto AfterHitLabel;
            }
        }

        while (Mathf.Abs((targetPos - transform.position).magnitude) > 0.5f)
        {
            transform.position += dir * Time.deltaTime * moveSpeed * (targetPos - transform.position).magnitude;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foreach (Entity entity in targetEntities)
        {
            HitTarget(competence, entity);
            yield return new WaitForSeconds(multihitDelay);
        }

        AfterHitLabel:
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
