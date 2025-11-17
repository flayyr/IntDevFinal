using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : Entity
{
    private void Start()
    {
        //UseCompetence(competences[0], enemy);
    }

    private void Update()
    {
        if (BattleManager.Instance.state == BattleState.idle)
        {
            progress += agility * Time.deltaTime;
            if (progress >= 1f)
            {
                progress = 0f;
                BattleManager.Instance.PlayerTurn(this);
            }
        }
    }
}
