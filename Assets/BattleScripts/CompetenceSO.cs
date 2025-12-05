using UnityEngine;

public enum Status { Lethargic, Muted, Poisoned, Hasty, Stasis, Incompetent, Strengthened, WideAngle, Revive, Defending}
public enum TargetType { Enemy, Ally, AllEnemy}
public enum RequirementType { None, NumEnemiesLessThan, NumEnemiesMoreThan, TurnsSinceUse}

[CreateAssetMenu(fileName = "Competence", menuName = "ScriptableObjects/CompetenceSO", order = 1)]
public class CompetenceSO : ScriptableObject
{
    public string description;
    public TargetType targetType;
    public int ccCost;
    public int hpChange;
    public int CPChange;
    public float ATKInfluence;
    public float ESPInfluence;
    public float DEFInfluence;
    public float AGIInfluence;
    public float variance;
    public float hitChance = 1;

    [Header("Statuses")]
    public Status[] statuses;
    public bool cures = false;
    public int statusDuration;
    public float inflictChance=1;

    [Header("Enemy Competence")]
    public bool isEnemy = false;
    public int weight;
    public RequirementType requirementType = RequirementType.None;
    public int requirementNum;
    public Entity summonPrefab;
    [HideInInspector] public int turnsSinceUse = 100;

    [Header("Animation")]
    public float moveAmount;
    public GameObject effect;
}
