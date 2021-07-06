using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] public int LevelIndex;
    [Space(20)]
    [SerializeField] string objectiveMolecule;
    [Space(5)]
    [SerializeField] string secondaryMolecule;
    [SerializeField] float objectiveTime;
    [SerializeField] int objectiveMoves;
    [Space(20)]

    [SerializeField] GameObject objectiveText;
    [SerializeField] GameObject winScreen;
    [SerializeField] List<Image> stars;
    [SerializeField] List<TextMeshProUGUI> objectivesText;

    public static GameController main;

    public bool ongoing = true;
    public int moves = 0;
    List<Particle> particles;

    public List<Objective> objectives;
    GameUI gameUI;

    void Start()
    {
        main = this;

        particles = Particle.getParticlesFromTag();

        objectives = new List<Objective>();


        if (objectiveMolecule != "")
        {
            objectives.Add(new Objective(objectiveMolecule));
            objectives.Add(new Objective(objectiveTime));
        }
        else
        {
            objectives.Add(new Objective());
            objectives.Add(new Objective(secondaryMolecule));
        }

        objectives.Add(new Objective(objectiveMoves));

        gameUI = new GameUI(objectiveText, winScreen, stars, objectivesText);

    }

    void Update()
    {
        if (ongoing)
        {
            if (objectives[0].ObjectiveComplete())
            {
                EndGame();
                gameUI.PrepareScreenUI();
                ongoing = false;
            }
        } else
        {
            gameUI.LiftWinScreen();
        }
    }

    public int EndGame() {
        int score = 0;
        foreach (Objective objective in objectives) { 
            if (objective.ObjectiveComplete())
            {
                score += 1;
            }
        }

        if (score > PlayerPrefs.GetInt(LevelIndex.ToString(), 0))
        {
            PlayerPrefs.SetInt(LevelIndex.ToString(), score);
        }

        return score;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level"+ (LevelIndex+1).ToString());
    }
    public void Menu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
    public void Retry()
    {
        SceneManager.LoadScene("Level" + LevelIndex.ToString());
    }

    public float GetTimeSinceLevelStart()
    {
        return Time.timeSinceLevelLoad;
    }

    public List<Molecule> GetMolecules()
    {
        List<Molecule> molecules = new List<Molecule>();
        foreach (Particle particle in particles)
        {
            molecules.Add(particle.element.molecule);
        }
        return molecules;
    }
}
