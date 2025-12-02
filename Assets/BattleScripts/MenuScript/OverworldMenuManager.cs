using UnityEngine;

public class OverworldMenuManager : MonoBehaviour
{
    public static OverworldMenuManager Instance;

    [SerializeField] CharacterSelectionmenu characterMenu;
    [SerializeField] CompetenceSelectionMenu competenceMenu;
    [SerializeField] public PlayerEntity[] playerEntities;

    enum MenuState {Closed, Character, Competence}

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
            if (Input.GetKeyDown(KeyCode.X)) {
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
                currSelectionMenu = competenceMenu;
                characterMenu.Hide();
                index = 0;
            } else if (Input.GetKeyDown(KeyCode.X)) {
                state = MenuState.Closed;
                characterMenu.Hide();
            }
        } else {
            if (Input.GetKeyDown(KeyCode.X)) {
                state = MenuState.Character;
                index = 0;
                characterMenu.Show();
                currSelectionMenu = characterMenu;
                competenceMenu.Hide();
            }
        }
    }

    void ChangeSelection(int i) {
        index = currSelectionMenu.SelectItem(i);
    }
}
