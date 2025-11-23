using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    [HideInInspector] public Pointer pointer;
    [SerializeField] int numMinions;
    [SerializeField] EnemyTimerAnimation enemyTimer;

    protected override void Update()
    {
        base.Update();
        enemyTimer.UpdateSprite(progress);
        if (progress >= 1f)
        {
            progress = 0f;
            CompetenceSO selectedCompetence = SelectCompetence();
            BattleManager.Instance.EnemyTurn(this, selectedCompetence);
        }
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
            case RequirementType.NumMinionsLessThan:
                return numMinions < competence.requirementNum;
            case RequirementType.NumMinionsMoreThan:
                return numMinions > competence.requirementNum;
            case RequirementType.None:
                return true;
            default:
                return false;
        }
    }
}
