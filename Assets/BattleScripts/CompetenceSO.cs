using UnityEngine;

public enum Status { Lethargic, Muted, Poisoned, Hasty, WideAngle, Revive, Stasis, Incompetent, Defending}
public enum TargetType { Enemy, Ally, AllEnemy}
public enum RequirementType { None, NumEnemiesLessThan, NumEnemiesMoreThan, TurnsSinceUse}

[CreateAssetMenu(fileName = "Competence", menuName = "ScriptableObjects/CompetenceSO", order = 1)]
public class CompetenceSO : ScriptableObject
{
    public string description;
    public TargetType targetType;
    public int ccCost;
    public int hpChange;

    public Status[] statuses;
    public bool cures = false;
    public int statusDuration;

    [Header("Enemy Competence")]
    public bool isEnemy = false;
    public int weight;
    public RequirementType requirementType = RequirementType.None;
    public int requirementNum;
    //public GameObject summonPrefab;
    [HideInInspector] public int turnsSinceUse = 100;

    [Header("Animation")]
    public float moveAmount;
    public GameObject effect;
}
