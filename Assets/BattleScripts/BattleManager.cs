using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    idle, selectAction, selectCompetence, selectTarget, Acting
}

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;
    public BattleState state = BattleState.idle;
    [Space(20)]
    [SerializeField] Pointer pointer;
    [SerializeField] CompetenceSO baseAttack;
    [SerializeField] CompetenceSO baseDefend;

    [Space(20)]
    [SerializeField]
    List<Entity> enemies = new List<Entity>();

    Entity currEntity;
    Entity targetEntity;
    CompetenceSO selectedCompetence;

    int selectionIndex;

    delegate void SelectFunction (int index);
    enum Action { Attack, Defend, Competence }
    enum SelectMode { Vertical, Horizontal}

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (state)
        {
            case BattleState.idle:
                break;

            case BattleState.selectAction:
                AlterSelection(SelectAction, SelectMode.Vertical);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Action selected = (Action)selectionIndex;
                    if (selected == Action.Attack)
                    {
                        selectedCompetence = baseAttack;
                        state = BattleState.selectTarget;
                    } else if (selected == Action.Defend)
                    {
                        Act(currEntity, targetEntity, baseDefend);
                    } else if (selected == Action.Competence)
                    {
                        state = BattleState.selectCompetence;
                    }
                }
                break;

            case BattleState.selectCompetence:
                //competence selection is four directional, so I won't use the AlterSelection function here
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SelectCompetence(selectionIndex - 2);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SelectCompetence(selectionIndex + 2);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (selectionIndex % 2 == 0)
                        SelectCompetence(selectionIndex + 1);
                    else
                        SelectCompetence(selectionIndex - 1);
                } else if (Input.GetKeyDown(KeyCode.Z))
                {
                    selectedCompetence = currEntity.competences[selectionIndex];
                    if (currEntity.cc >= selectedCompetence.ccCost)
                    {
                        state = BattleState.selectTarget;
                    }
                    else
                    {
                        //NOT ENOUGH CC
                    }
                } else if (Input.GetKeyDown(KeyCode.X))
                {
                    state = BattleState.selectAction;
                }
                break;

            case BattleState.selectTarget:
                if (selectedCompetence.targetEnemy)
                    AlterSelection(SelectEnemy, SelectMode.Horizontal);
                else
                    AlterSelection(SelectAlly, SelectMode.Horizontal);

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Act(currEntity, targetEntity, selectedCompetence);
                } else if (Input.GetKeyDown(KeyCode.X))
                {
                    if (selectedCompetence == baseAttack)
                    {
                        state = BattleState.selectAction;
                    }
                    else
                    {
                        state = BattleState.selectCompetence;
                    }

                }
                break;

            case BattleState.Acting:
                break;
        }
    }

    void Act(Entity user, Entity target, CompetenceSO competence)
    {
        user.UseCompetence(competence, target);
        state = BattleState.Acting;
    }

    public void PlayerTurn(PlayerEntity entity)
    {
        state = BattleState.selectAction;
        currEntity = entity;
        SelectAction(0);
    }

    void AlterSelection(SelectFunction function, SelectMode mode)
    {
        KeyCode dec = (mode == SelectMode.Vertical) ? KeyCode.UpArrow : KeyCode.LeftArrow;
        KeyCode inc = (mode == SelectMode.Vertical) ? KeyCode.DownArrow : KeyCode.RightArrow;

        if (Input.GetKeyDown(dec))
        {
            function(selectionIndex - 1);
        }
        else if (Input.GetKeyDown(inc))
        {
            function(selectionIndex + 1);
        }
    }

    void SelectAction(int index)
    {

    }

    void SelectCompetence(int index)
    {

    }

    void SelectEnemy(int index)
    {
        int i = (index+enemies.Count) % enemies.Count;
        pointer.SetSelection(enemies[i]);
        targetEntity = enemies[i];
        selectionIndex = i;
    }

    void SelectAlly(int index)
    {

    }


}
