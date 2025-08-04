using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Best Score: {PlayerPrefs.GetInt("BestScore", 0).ToString()}";
    }
}
