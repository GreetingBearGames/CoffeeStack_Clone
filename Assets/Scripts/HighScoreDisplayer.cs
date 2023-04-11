using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;


public class HighScoreDisplayer : MonoBehaviour
{
    [SerializeField] private float score, highScore;
    [SerializeField] private float[] scoreTowerValues;
    [SerializeField] private Transform scoreTower;


    private void Start()
    {
        scoreTowerValues = new float[scoreTower.childCount];

        for (int i = 0; i < scoreTower.childCount; i++)
        {
            var textValue = scoreTower.GetChild(i).GetChild(0).GetComponent<TextMeshPro>().text;
            var isNumeric = int.TryParse(textValue, out int n);
            if (isNumeric)
            {
                scoreTowerValues[i] = int.Parse(textValue);
            }
        }
    }

    public void HighScoreItemFinder()
    {
        var remainHighScore = highScore % 5;
        if (score < highScore - remainHighScore)
        {
            for (int i = scoreTower.childCount - 1; i > 0; i--)
            {
                if (scoreTowerValues[i] != 0 && highScore >= scoreTowerValues[i])
                {
                    HighScoreItemDisplayer(scoreTower.GetChild(i).gameObject);
                    break;
                }
            }
        }
    }

    private void HighScoreItemDisplayer(GameObject highScoreItem)
    {
        highScoreItem.GetComponent<Image>().color = new Color32(254, 231, 12, 255);
        var value = highScoreItem.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        highScoreItem.transform.GetChild(0).GetComponent<TextMeshPro>().text = "HIGHSCORE " + value;

        CameraManager.Instance.finish3Cam.Follow = highScoreItem.transform;
        CameraManager.Instance.finish3Cam.LookAt = highScoreItem.transform;
        CameraManager.Instance.SwitchCamera(CameraManager.Instance.finish3Cam);

    }
}
