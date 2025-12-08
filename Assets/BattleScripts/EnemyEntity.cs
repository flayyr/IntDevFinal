using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [Space(20)]
    [HideInInspector] public Pointer pointer;
    [SerializeField] EnemyTimerAnimation enemyTimer;
    [SerializeField] float TimerShowAfterPercentage = 0.2f;
    [SerializeField] Vector2 timerPositionOffset;

    public EnemySpawn spawnPosition;

    protected override void Awake() {
        base.Awake();
        enemyTimer.transform.parent = null;
    }

    private void Start()
    {
        enemyTimer.Hide();
    }

    protected override void Update()
    {
        base.Update();
        enemyTimer.UpdateSprite((progress-TimerShowAfterPercentage)/(1-TimerShowAfterPercentage));
        if (progress >= 1f)
        {
            progress = 0f;
            CompetenceSO selectedCompetence = SelectCompetence();
            BattleManager.Instance.EnemyTurn(this, selectedCompetence);
            enemyTimer.Hide();
        }

        if (BattleManager.Instance.state == BattleState.idle) {
            if (progress >= TimerShowAfterPercentage) {
                enemyTimer.Show();
            }
        }
    }

    public override void ChangeHP(int change) {
        base.ChangeHP(change);
    }

    protected override void Die() {
        base.Die();
        BattleManager.Instance.RemoveEnemy(this);
        Destroy(enemyTimer.gameObject);
        Destroy(gameObject);
    }

    CompetenceSO SelectCompetence()
    {
        Queue<CompetenceSO> usableCompetences = new Queue<CompetenceSO>();
        int totalWeight = 0;
        foreach (CompetenceSO competence in competences)
        {
            if (CheckRequirement(competence))
            {
                usableCompetences.Enqueue(competence);
                totalWeight += competence.weight;
            }
        }
        int randomWeight = Random.Range(0, totalWeight);
        while (usableCompetences.Count > 0)
        {
            CompetenceSO current = usableCompetences.Dequeue();
            randomWeight -= current.weight;
            if (randomWeight <= 0)
            {
                return current;
            }
        }
        Debug.Log("competence out of bounds!");
        return null;
    }

    bool CheckRequirement(CompetenceSO competence)
    {
        foreach (Requirement req in competence.requirements)
        {
            switch (req.requirementType)
            {
                case RequirementType.TurnsSinceUse:
                    if (competence.turnsSinceUse >= req.requirementNum)
                    {
                        //competence.turnsSinceUse = competence.requirementNum; i forgot why this line exists
                        continue;
                    }
                    return false;
                case RequirementType.NumEnemiesLessThan:
                    if( BattleManager.Instance.enemies.Count < req.requirementNum)
                    {
                        continue;
                    }
                    return false;
                case RequirementType.NumEnemiesMoreThan:
                    if( BattleManager.Instance.enemies.Count > req.requirementNum)
                    {
                        continue;
                    }
                    return false;
                case RequirementType.CantBeMuted:
                    if (!statuses[(int)Status.Muted])
                    {
                        continue;
                    }
                    return false;
            }
        }
        return true;
    }

    public void SetTimerPosition()
    {
        enemyTimer.transform.position = transform.position + (Vector3)timerPositionOffset;
    }
}
