using UnityEngine;
using UnityEngine.SceneManagement;

public class RaAreaQuizController : MonoBehaviour
{
    private float timer = 0f;
    private float maxTime = 10f;

    // 고른 선지에 따른 씬 넘기기
    void Update()
    {
        // Check for key inputs
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            MoveToCorrectScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            MoveToWrongScene();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            MoveToWrongScene();
        }

        // Increment the timer
        timer += Time.deltaTime;

        // Check if 10 seconds have passed
        if (timer >= maxTime)
        {
            MoveToRaAreaScene();
        }
    }

    // 정답을 골랐을 때 동작
    void MoveToCorrectScene()
    {
        SceneManager.LoadScene("RaArea_2");
    }

    // 오답을 골랐을 때 동작
    void MoveToWrongScene()
    {
        SceneManager.LoadScene("RaArea_3");
    }

    // 타이머가 만료되었을 때 동작
    void MoveToRaAreaScene()
    {
        SceneManager.LoadScene("RaArea_3");
    }
}