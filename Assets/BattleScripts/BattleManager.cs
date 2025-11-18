using System;
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
    public GameObject damageTextPrefab;
    public Canvas worldSpaceCanvas;
    [Space(20)]
    [SerializeField] Pointer pointer;
    [SerializeField] CompetenceSO baseAttack;
    [SerializeField] CompetenceSO baseDefend;

    [Space(20)]
    [SerializeField] ActionSelectionMenu actionSelectionMenu;
    [SerializeField] CompetenceSelectionMenu competenceSelectionMenu;
    [SerializeField] CharacterSelectionmenu characterSelectionMenu;

    [Space(20)]
    [SerializeField] List<Entity> enemies = new List<Entity>();
    [SerializeField] public PlayerEntity[] playerEntities;

    Entity currEntity;
    Entity targetEntity;
    CompetenceSO selectedCompetence;

    public int selectionIndex;

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
                        SwitchState(BattleState.selectTarget);
                    } else if (selected == Action.Defend)
                    {
                        Act(currEntity, targetEntity, baseDefend);
                    } else if (selected == Action.Competence)
                    {
                        SwitchState(BattleState.selectCompetence);
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
                    if (selectionIndex < currEntity.competences.Length && currEntity.cc >= currEntity.competences[selectionIndex].ccCost)
                    {
                        selectedCompetence = currEntity.competences[selectionIndex];
                        SwitchState(BattleState.selectTarget);
                    }
                    else
                    {
                        //NOT ENOUGH CC or invalid selection
                    }
                } else if (Input.GetKeyDown(KeyCode.X))
                {
                    SwitchState(BattleState.selectAction);
                }
                break;

            case BattleState.selectTarget:
                if (selectedCompetence.targetEnemy)
                    AlterSelection(SelectEnemy, SelectMode.Horizontal);
                else
                    AlterSelection(SelectAlly, SelectMode.Horizontal);

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (!selectedCompetence.targetEnemy)
                        targetEntity = playerEntities[selectionIndex];
                    Act(currEntity, targetEntity, selectedCompetence);
                } else if (Input.GetKeyDown(KeyCode.X))
                {
                    if (selectedCompetence == baseAttack)
                    {
                        SwitchState(BattleState.selectAction);
                    }
                    else
                    {
                        SwitchState(BattleState.selectCompetence);
                    }

                }
                break;

            case BattleState.Acting:
                break;
        }
    }

    public void SwitchState(BattleState targetState) {
        if (state == BattleState.selectAction) {
            actionSelectionMenu.Hide();
            characterSelectionMenu.Default();
        } else if (state == BattleState.selectCompetence) {
            competenceSelectionMenu.Hide();
        } else if (state == BattleState.selectTarget) {
            characterSelectionMenu.Default();
            pointer.Hide();
        }

        if (targetState == BattleState.selectAction) {
            actionSelectionMenu.Show((PlayerEntity)currEntity);
            characterSelectionMenu.Hide();
        } else if (targetState == BattleState.selectCompetence) {
            competenceSelectionMenu.Show((PlayerEntity)currEntity);
        } else if (targetState == BattleState.selectTarget) {
            if (selectedCompetence.targetEnemy) {
                SelectEnemy(0);
            } else {
                characterSelectionMenu.Show();
                targetEntity = playerEntities[selectionIndex];
            }
        }
        selectionIndex = 0;

        state = targetState;
    }

    void Act(Entity user, Entity target, CompetenceSO competence)
    {
        user.UseCompetence(competence, target);
        SwitchState(BattleState.Acting);
    }

    public void PlayerTurn(PlayerEntity entity)
    {
        currEntity = entity;

        SwitchState(BattleState.selectAction);
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

    void SelectAction(int index) {
        selectionIndex = actionSelectionMenu.SelectItem(index);
    }

    void SelectCompetence(int index)
    {
        selectionIndex = competenceSelectionMenu.SelectItem(index);
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
        selectionIndex = characterSelectionMenu.SelectItem(index);
    }


}
