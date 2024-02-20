using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneSwitcher : MonoBehaviour
{
    public float delayInSeconds = 20f; // 씬 전환까지의 대기 시간 (초)

    private void Start()
    {
        // delayInSeconds 이후에 다음 씬으로 전환합니다.
        Invoke("LoadNextScene", delayInSeconds);
    }

    private void LoadNextScene()
    {
        // 다음 씬을 로드합니다. 씬 이름을 수정하세요.
        SceneManager.LoadScene(2);
    }
}
