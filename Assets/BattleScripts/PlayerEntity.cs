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
}
