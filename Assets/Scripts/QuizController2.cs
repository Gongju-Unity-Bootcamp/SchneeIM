using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController2 : MonoBehaviour
{
    // 키 입력을 확인하는 함수
    void Update()
    {
        // 키보드 숫자 키 1, 2, 3을 눌렀을 때만 동작
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
    }

    // 정답 씬으로 이동하는 함수
    void MoveToCorrectScene()
    {
        SceneManager.LoadScene("RaArea_2");
    }

    // 오답 씬으로 이동하는 함수
    void MoveToWrongScene()
    {
        SceneManager.LoadScene("RaArea_3");
    }
}