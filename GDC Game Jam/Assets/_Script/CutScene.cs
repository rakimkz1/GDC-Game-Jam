using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public void EndCutscene()
    {
        SceneManager.LoadScene(2);
    }
}
