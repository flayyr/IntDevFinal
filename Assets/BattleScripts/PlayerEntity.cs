using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : Entity
{
    protected override void Update()
    {
        base.Update();
        if (progress >= 1f) {
            progress = 0f;
            BattleManager.Instance.PlayerTurn(this);
        }
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<SpriteRenderer>().color = Color.black;
        BattleManager.Instance.CheckWin();
    }

    protected override void Revive()
    {
        base.Revive();
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
