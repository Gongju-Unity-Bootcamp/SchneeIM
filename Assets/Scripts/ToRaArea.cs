using UnityEngine;
using UnityEngine.SceneManagement;

public class ToRaArea : MonoBehaviour
{
    // 전환할 씬의 이름
    public string nextSceneName = "3_RaArea";
    // 전환까지의 대기 시간 (초)
    public float delayInSeconds = 39f;

    // Start 함수에서 지연 후 씬 전환을 호출합니다.
    void Start()
    {
        Invoke("TransitionToNextScene", delayInSeconds);
    }

    // 다음 씬으로 전환하는 함수
    void TransitionToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
