using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneSwitcher : MonoBehaviour
{
    public float delayInSeconds = 20f; // �� ��ȯ������ ��� �ð� (��)

    private void Start()
    {
        // delayInSeconds ���Ŀ� ���� ������ ��ȯ�մϴ�.
        Invoke("LoadNextScene", delayInSeconds);
    }

    private void LoadNextScene()
    {
        // ���� ���� �ε��մϴ�. �� �̸��� �����ϼ���.
        SceneManager.LoadScene(2);
    }
}
