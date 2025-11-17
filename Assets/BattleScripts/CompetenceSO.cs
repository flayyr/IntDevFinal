using UnityEngine;

public enum Status { Lethargic, Muted, Poisoned, Hasty, Defending}

[CreateAssetMenu(fileName = "Competence", menuName = "ScriptableObjects/CompetenceSO", order = 1)]
public class CompetenceSO : ScriptableObject
{
    public string competenceName;
    public string description;
    public bool targetEnemy = true;
    public int ccCost;
    public int hpChange;

    public Status[] statuses;
    public bool cures = false;

    [Header("Animation")]
    public float moveAmount;
    public GameObject effect;
}
