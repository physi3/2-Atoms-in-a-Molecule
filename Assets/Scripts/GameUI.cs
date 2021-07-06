using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI
{
    TextMeshProUGUI objectiveText;
    RectTransform winScreen;

    List<Image> stars;
    List<TextMeshProUGUI> objectivesText;
    Sprite fullStar;
    Sprite emptyStar;
    GameController gameController;

    public GameUI(GameObject passedObjectiveText, GameObject passedWinScreen, List<Image> passedstars, List<TextMeshProUGUI> objectives)
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        objectiveText = passedObjectiveText.GetComponent<TextMeshProUGUI>();

        objectiveText.text = GameController.main.objectives[0].readalise();

        winScreen = passedWinScreen.GetComponent<RectTransform>();

        stars = passedstars;
        fullStar = UIResources.FullStar;
        emptyStar = UIResources.EmptyStar;
        objectivesText = objectives;
    }

    public void LiftWinScreen()
    {
        winScreen.position = Vector2.Lerp(winScreen.position, new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), 8f * Time.deltaTime);
    }
    
    public void PrepareScreenUI()
    {

        for (int i = 0; i < 3; i++)
        {
            objectivesText[i].text = GameController.main.objectives[i].readalise();

            if (GameController.main.objectives[i].ObjectiveComplete())
            {
                stars[i].sprite = UIResources.FullStar;
                objectivesText[i].color = objectivesText[0].color;
            }
            else
            {
                stars[i].sprite = UIResources.EmptyStar;
            }
        }

    }

}
