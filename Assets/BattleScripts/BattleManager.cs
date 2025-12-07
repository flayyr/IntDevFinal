using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BattleState
{
    idle, selectAction, selectCompetence, selectTarget, Acting, Finished
}

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;
    public BattleState state = BattleState.idle;
    [Space(20)]
    public GameObject damageTextPrefab;
    [SerializeField] GameObject pointerPrefab;
    public Canvas worldSpaceCanvas;
    [SerializeField] FightEnd fightEndBanner;
    [Space(20)]
    [SerializeField] Pointer pointer;
    [SerializeField] CompetenceSO baseAttack;
    [SerializeField] CompetenceSO baseDefend;

    [Space(20)]
    [SerializeField] ActionSelectionMenu actionSelectionMenu;
    [SerializeField] CompetenceSelectionMenu competenceSelectionMenu;
    [SerializeField] CharacterSelectionmenu characterSelectionMenu;

    [Space(20)]
    [SerializeField] public List<EnemyEntity> enemies = new List<EnemyEntity>();
    [SerializeField] EnemySpawn[] enemySpawnPositions;
    [SerializeField] public PlayerEntity[] playerEntities;

    Entity currEntity;
    Entity targetEntity;
    List<Entity> targetEntities;
    CompetenceSO selectedCompetence;

    [HideInInspector] public bool actionDone = false;
    [HideInInspector] public bool descriptionDone = false;

    public int selectionIndex;

    delegate void SelectFunction (int index);
    enum Action { Attack, Defend, Competence }
    enum SelectMode { Vertical, Horizontal}

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemySpawnPositions[i].occupied = true;
            enemies[i].spawnPosition = enemySpawnPositions[i];
        }
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
                    SFXManager.Instance.PlaySound(SFXManager.Instance.selectClick);
                    Action selected = (Action)selectionIndex;
                    if (selected == Action.Attack)
                    {
                        selectedCompetence = baseAttack;
                        SwitchState(BattleState.selectTarget);
                    } else if (selected == Action.Defend)
                    {
                        Act(currEntity, currEntity, baseDefend);
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
                    if (selectionIndex < currEntity.competences.Length && currEntity.cc >= currEntity.competences[selectionIndex].ccCost && !currEntity.statuses[(int)Status.Muted])
                    {
                        SFXManager.Instance.PlaySound(SFXManager.Instance.selectClick);
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
                if (selectedCompetence.targetType == TargetType.Enemy)
                    AlterSelection(SelectEnemy, SelectMode.Horizontal);
                else if(selectedCompetence.targetType == TargetType.Ally)
                    AlterSelection(SelectAlly, SelectMode.Horizontal);

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SFXManager.Instance.PlaySound(SFXManager.Instance.selectClick);
                    if (selectedCompetence.targetType == TargetType.Ally)
                        targetEntity = playerEntities[selectionIndex];
                    if (selectedCompetence.targetType == TargetType.AllEnemy)
                    {
                        targetEntities = new List<Entity>();
                        foreach(EnemyEntity enemy in enemies)
                        {
                            enemy.pointer.Deselect();
                            targetEntities.Add(enemy);
                        }
                    }
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
                if(actionDone && descriptionDone)
                {
                    actionDone = false;
                    descriptionDone = false;
                    state = BattleState.idle;
                }
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
            if (selectedCompetence.targetType != TargetType.Ally) {
                SelectEnemy(0);
            } else
            {
                characterSelectionMenu.Show();
                targetEntity = playerEntities[0];
            }
        }
        selectionIndex = 0;

        state = targetState;
    }

    void Act(Entity user, Entity target, CompetenceSO competence)
    {
        if(competence.targetType != TargetType.AllEnemy)
            user.UseCompetence(competence, target);
        else
            user.UseMultiTargetCompetence(competence, targetEntities);
        SwitchState(BattleState.Acting);
    }

    public void PlayerTurn(PlayerEntity entity)
    {
        currEntity = entity;

        SwitchState(BattleState.selectAction);
    }

    public void EnemyTurn(EnemyEntity entity, CompetenceSO competence)
    {
        currEntity = entity;
        selectedCompetence = competence;

        if(competence.summonPrefab != null)
        {
            targetEntity = SpawnEnemy(competence.summonPrefab);
            if (targetEntity != null) {
                currEntity.UseCompetence(selectedCompetence, targetEntity);
            }
        }
        else if(competence.targetType == TargetType.Ally)
        {
            targetEntities = new List<Entity>();
            foreach (Entity enemy in enemies)
                targetEntities.Add(enemy);
            currEntity.UseMultiTargetCompetence(selectedCompetence, targetEntities);
        } else
        {
            targetEntities = AlivePlayers();
            if (competence.targetType == TargetType.Enemy)
            {
                targetEntity = targetEntities[Random.Range(0, targetEntities.Count)];
                currEntity.UseCompetence(selectedCompetence, targetEntity);
            } else
            {
                currEntity.UseMultiTargetCompetence(selectedCompetence, targetEntities);
            }
        }
        SwitchState(BattleState.Acting);
    }

    List<Entity> AlivePlayers()
    {
        List<Entity> alivePlayers = new List<Entity>();
        foreach (Entity player in playerEntities)
        {
            if (player.hp > 0)
            {
                alivePlayers.Add(player);
            }
        }
        return alivePlayers;
    }

    void AlterSelection(SelectFunction function, SelectMode mode)
    {
        KeyCode dec = (mode == SelectMode.Vertical) ? KeyCode.UpArrow : KeyCode.LeftArrow;
        KeyCode inc = (mode == SelectMode.Vertical) ? KeyCode.DownArrow : KeyCode.RightArrow;

        if (Input.GetKeyDown(dec))
        {
            function(selectionIndex - 1);
            SFXManager.Instance.PlaySound(SFXManager.Instance.cycleClick);
        }
        else if (Input.GetKeyDown(inc))
        {
            function(selectionIndex + 1);
            SFXManager.Instance.PlaySound(SFXManager.Instance.cycleClick);
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
        if (selectedCompetence.targetType == TargetType.Enemy)
        {
            int i = (index + enemies.Count) % enemies.Count;
            pointer.SetSelection(enemies[i]);
            targetEntity = enemies[i];
            selectionIndex = i;
        }
        else
        {
            foreach(EnemyEntity enemy in enemies){
                enemy.pointer = Instantiate(pointerPrefab, worldSpaceCanvas.transform).GetComponent<Pointer>();
                enemy.pointer.SetSelection(enemy);
            }
        }
    }

    void SelectAlly(int index)
    {
        selectionIndex = characterSelectionMenu.SelectItem(index);
    }

    public void CheckWin() {
        if(enemies.Count == 0) {
            fightEndBanner.EndFight(true);
            state = BattleState.Finished;
        }
        foreach (PlayerEntity player in playerEntities) {
            if (!player.dead) {
                return;
            }
        }
        fightEndBanner.EndFight(false);
        state = BattleState.Finished;
    }

    public void RemoveEnemy(EnemyEntity enemy)
    {
        enemy.spawnPosition.occupied = false;
        enemies.Remove(enemy);
        CheckWin();
    }

    public Entity SpawnEnemy(EnemyEntity enemy)
    {
        for (int i = 0; i < enemySpawnPositions.Length; i++)
        {
            if (!enemySpawnPositions[i].occupied)
            {
                EnemyEntity newEnemy = Instantiate(enemy);
                newEnemy.transform.position = enemySpawnPositions[i].transform.position;
                newEnemy.GetComponent<SpriteRenderer>().sortingOrder = enemySpawnPositions.Length - i;
                newEnemy.spawnPosition = enemySpawnPositions[i];
                enemySpawnPositions[i].occupied = true;
                newEnemy.SetTimerPosition();

                enemies.Insert(i, newEnemy);
                return newEnemy;
            }
        }
        Debug.Log("No space for new enemy!");
        return null;
    }


}
