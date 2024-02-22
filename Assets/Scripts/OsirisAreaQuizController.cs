using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OsirisAreaQuizController : MonoBehaviour
{
    // 키 입력을 확인하는 함수
    void Update()
    {
        // 키보드 숫자 키 1, 2, 3을 눌렀을 때만 동작
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
    }

    // 정답 씬으로 이동하는 함수
    void MoveToCorrectScene()
    {
        SceneManager.LoadScene("OsirisArea_2");
    }

    // 오답 씬으로 이동하는 함수
    void MoveToWrongScene()
    {
        SceneManager.LoadScene("OsirisArea_3");
        StartCoroutine(GoToIntroAfterDelay());
    }

    // 인트로 씬으로 돌아가는 코루틴
    IEnumerator GoToIntroAfterDelay()
    {
        yield return new WaitForSeconds(3f); // 3초 대기
        SceneManager.LoadScene("Intro"); // 인트로 씬으로 이동
    }
}
