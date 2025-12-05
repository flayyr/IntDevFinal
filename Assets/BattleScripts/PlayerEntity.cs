using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : Entity
{
    [SerializeField] public Sprite portraitSprite;
    [SerializeField] Sprite aliveSprite;
    [SerializeField] Sprite deadSprite;

    [SerializeField] float idleAnimationTiming = 0.5f;
    [SerializeField] float idleAnimationOffset=0.01f;
    
    float timer = 0;
    int animDir = 1;

    private void Start()
    {
        timer = Random.Range(0f, idleAnimationTiming);
        animDir = (Random.Range(0, 2) == 0) ? -1 : 1;
    }

    protected override void Update()
    {
        base.Update();
        if (progress >= 1f) {
            progress = 0f;
            BattleManager.Instance.PlayerTurn(this);
        }

        //if(BattleManager.Instance.state == BattleState.idle)
            timer += Time.deltaTime;

        if (timer > idleAnimationTiming)
        {
            timer = 0;
            animDir *= -1;
            transform.position = new Vector3(transform.position.x+idleAnimationOffset*animDir, transform.position.y, transform.position.z);
        }
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        BattleManager.Instance.CheckWin();
    }

    protected override void Revive()
    {
        base.Revive();
        GetComponent<SpriteRenderer>().sprite = aliveSprite;
    }
}
