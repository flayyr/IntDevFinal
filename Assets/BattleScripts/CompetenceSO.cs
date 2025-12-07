using System;
using UnityEngine;

public enum Status { Lethargic, Muted, Poisoned, Hasty, Stasis, Incompetent, Strengthened, Toughened, Clever, WideAngle, Revive, Defending}
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
    [SerializeField, Range(0f,1f)]public float variance;
    [SerializeField, Range(0f, 1f)] public float hitChance = 1;

    [Header("Statuses")]
    public Status[] statuses;
    public bool cures = false;
    public int statusDuration = 1;
    [SerializeField, Range(0f, 1f)] public float inflictChance=1;
    public bool chronomancy = false;

    [Header("Enemy Competence")]
    public bool isEnemy = false;
    public int weight;
    public Requirement[] requirements;
    public EnemyEntity summonPrefab;
    [HideInInspector] public int turnsSinceUse = 100;

    [Header("Animation")]
    public float moveAmount;
    public GameObject effect;
    public AudioClip soundEffect;
}

[Serializable]
public class Requirement
{
    public RequirementType requirementType = RequirementType.None;
    public int requirementNum;
}
