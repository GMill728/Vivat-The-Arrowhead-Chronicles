using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingLevel : MonoBehaviour
{
    public void TestButton()
    {
        SceneManager.LoadScene("TestingEnemyLevel");
    }
}
