using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int level;

    void Start()
    {
        Button button = GetComponent<Button>();

        Transform TStars = gameObject.transform.Find("Stars");
        List<Image> stars = new List<Image>();

        for (int i = 0; i < TStars.childCount; i++)
        {
            stars.Add(TStars.GetChild(i).gameObject.GetComponent<Image>());
        }

        stars.Reverse(1,2);

        prepareButton(button, transform.Find("Text").gameObject, transform.Find("Image").gameObject, stars, PlayerPrefs.GetInt((level - 1).ToString(), 0) > 0, PlayerPrefs.GetInt(level.ToString(), 0));

    }

    public void prepareButton(Button button, GameObject textObject, GameObject lockObject, List<Image> stars, bool readyToPlay, int objectivesComplete)
    {
        textObject.GetComponent<TextMeshProUGUI>().text = level.ToString();
        button.onClick.AddListener(loadLevel);

        if (!readyToPlay && level != 1)
        {
            button.interactable = false;
            textObject.SetActive(false);
            lockObject.SetActive(true);
        }

        for (int starIndex = 0; starIndex < stars.Count; starIndex++)
        {
            if (starIndex < objectivesComplete )
            {
                stars[starIndex].sprite = UIResources.FullStar;
            }
            else
            {
                stars[starIndex].sprite = UIResources.EmptyStar;
            }
        }
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }

}
