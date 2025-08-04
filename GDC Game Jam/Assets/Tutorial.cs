using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;

    private void OnEnable()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void Next()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void Back()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        gameObject.SetActive(false);
    }
}
