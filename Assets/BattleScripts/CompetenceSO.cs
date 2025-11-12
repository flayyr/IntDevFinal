using UnityEngine;

[CreateAssetMenu(fileName = "Competence", menuName = "ScriptableObjects/CompetenceSO", order = 1)]
public class CompetenceSO : ScriptableObject
{
    public int ccCost;
    public int hpChange;

    [Header("Animation")]
    public float moveAmount;
    public GameObject effect;
}
