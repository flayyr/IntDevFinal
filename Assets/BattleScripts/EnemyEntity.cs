using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [Space(20)]
    [HideInInspector] public Pointer pointer;
    [SerializeField] EnemyTimerAnimation enemyTimer;
    [SerializeField] float TimerShowAfterPercentage = 0.2f;

    float timer = 0;

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
            timer = 0f;
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
        BattleManager.Instance.enemies.Remove(this);
        BattleManager.Instance.CheckWin();
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
        switch (competence.requirementType)
        {
            case RequirementType.TurnsSinceUse:
                if(competence.turnsSinceUse >= competence.requirementNum)
                {
                    competence.turnsSinceUse = competence.requirementNum;
                    return true;
                }
                return false;
            case RequirementType.NumEnemiesLessThan:
                return BattleManager.Instance.enemies.Count < competence.requirementNum;
            case RequirementType.NumEnemiesMoreThan:
                return BattleManager.Instance.enemies.Count > competence.requirementNum;
            case RequirementType.None:
                return true;
            default:
                return false;
        }
    }
}
