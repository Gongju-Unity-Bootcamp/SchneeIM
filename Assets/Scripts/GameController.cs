using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // 게임 오버 여부를 나타내는 변수
    private bool isGameOver = false;

    // 게임 오버 상태를 설정하는 함수
    public void SetGameOver()
    {
        isGameOver = true;
        Invoke("LoadIntroScene", 3f); // 3초 후에 LoadIntroScene 함수 호출
    }

    // 인트로 씬으로 돌아가는 함수
    void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro");
    }

    // 게임 오버 조건을 체크하는 함수 (임의로 구현)
    bool GameOverConditionMet()
    {
        // 게임 오버 조건을 여기에 구현
        // 예를 들어, 플레이어의 체력이 0이 되면 게임 오버
        return false;
    }

    // 게임 오버 상태에서 키 입력을 무시하는 함수
    void Update()
    {
        // 게임 오버 상태일 때는 키 입력을 무시
        if (isGameOver)
            return;

        // 게임 오버 조건을 체크하고 게임 오버 상태를 설정
        if (GameOverConditionMet())
        {
            SetGameOver();
        }
    }
}