using UnityEngine;
using UnityEngine.SceneManagement;

public class AnubisQuizController : MonoBehaviour
{
    private float timer = 0f;
    private float maxTime = 10f;
    
    void Update()
    {
        // quiz time
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            MoveToWrongScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            MoveToWrongScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            MoveToCorrectScene();
        }

        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            MoveToGameOverScene();
        }
    }

    // load succeed Scene
    void MoveToCorrectScene()
    {
        SceneManager.LoadScene("6_StairsToFinal");
    }

    // load gameover scene
    void MoveToWrongScene()
    {
        SceneManager.LoadScene("6_2_StairsToFinal");
    }

    void MoveToGameOverScene()
    {
        SceneManager.LoadScene("6_2_StairsToFinal");
    }
}
