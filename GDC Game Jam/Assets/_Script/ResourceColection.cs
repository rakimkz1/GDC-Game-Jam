using System.Collections;
using TMPro;
using UnityEngine;
public class ResourceColection : MonoBehaviour
{
    public int resourceNumber;
    public static ResourceColection instance;
    public TextMeshProUGUI txt_Score;
    private void Awake()
    {
        instance = this;
    }
    public void AddResource(int number)
    {
        resourceNumber += number;
        txt_Score.text = resourceNumber.ToString();
    }
    public void RemoveResource(int number)
    {
        resourceNumber -= number;
        txt_Score.text = resourceNumber.ToString();
    }
}