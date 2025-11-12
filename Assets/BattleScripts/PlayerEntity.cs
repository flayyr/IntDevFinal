using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : Entity
{
    [SerializeField] Entity enemy;
    [SerializeField] Slider progressBar;
    private void Start()
    {
        //UseCompetence(competences[0], enemy);
    }

    private void Update()
    {
        progress += agility * Time.deltaTime;
        progressBar.value = progress;
        if(progress >= 1f)
        {
            progress = 0f;
            UseCompetence(competences[0], enemy);
        }
    }
}
