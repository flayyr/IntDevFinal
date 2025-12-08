using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldMenuManager : MonoBehaviour
{
    public static OverworldMenuManager Instance;

    [SerializeField] OWCharacterSelectionMenu characterMenu;
    [SerializeField] CompetenceSelectionMenu competenceMenu;
    [SerializeField] OWPortraitSwitcher portraitSwitcher;
    [SerializeField] OWQuitMenu quitMenu;
    [SerializeField] public PlayerEntity[] playerEntities;

    enum MenuState {Closed, Character, Competence, Exit}

    ISelectionMenu currSelectionMenu;
    MenuState state = MenuState.Closed;
    int index = 0;
    Entity selectedEntity;
    private void Awake() {
        Instance = this;
        state = MenuState.Closed;
    }


    private void Start() {
        currSelectionMenu = characterMenu;
    }

    void Update()
    {
        if (state != MenuState.Closed) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                ChangeSelection(index - 1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                ChangeSelection(index + 1);
            }
        }

        if (state == MenuState.Closed) {
            if (Input.GetKeyDown(KeyCode.C)) {
                state = MenuState.Character;
                index = 0;
                characterMenu.Show();
                currSelectionMenu = characterMenu;
                currSelectionMenu.SelectItem(0);
            }
        } else if (state == MenuState.Character) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                state = MenuState.Competence;
                competenceMenu.Show(playerEntities[index]);
                portraitSwitcher.ShowPortraitAtIndex(index);

                currSelectionMenu = competenceMenu;
                characterMenu.Hide();
                index = 0;
            } else if (Input.GetKeyDown(KeyCode.X)||Input.GetKeyDown(KeyCode.X)) {
                state = MenuState.Closed;
                characterMenu.Hide();
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                state = MenuState.Exit;
                characterMenu.Hide();
                quitMenu.Show();
                currSelectionMenu = quitMenu;
                index = 0;
            }
        } else if (state == MenuState.Competence) {
            if (Input.GetKeyDown(KeyCode.X)) {
                state = MenuState.Character;
                index = 0;
                characterMenu.Show();
                currSelectionMenu = characterMenu;
                competenceMenu.Hide();
            }
        } else if (state == MenuState.Exit) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                state = MenuState.Character;
                index = 0;
                characterMenu.Show();
                currSelectionMenu = characterMenu;
                quitMenu.Hide();
            } else if (Input.GetKeyDown(KeyCode.Z)) {
                if (index == 0) {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
                } else {
                    Application.Quit();
                }
            }
        }
    }

    void ChangeSelection(int i) {
        index = currSelectionMenu.SelectItem(i);
    }
}
