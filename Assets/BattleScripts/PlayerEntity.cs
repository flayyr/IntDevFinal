using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : Entity
{
    private void Start()
    {
        //UseCompetence(competences[0], enemy);
    }

    protected override void Update()
    {
        base.Update();
        if (progress >= 1f) {
            progress = 0f;
            BattleManager.Instance.PlayerTurn(this);
        }
    }
}
