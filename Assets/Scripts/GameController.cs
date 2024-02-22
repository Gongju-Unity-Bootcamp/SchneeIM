using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ���� ���� ���θ� ��Ÿ���� ����
    private bool isGameOver = false;

    // ���� ���� ���¸� �����ϴ� �Լ�
    public void SetGameOver()
    {
        isGameOver = true;
        Invoke("LoadIntroScene", 3f); // 3�� �Ŀ� LoadIntroScene �Լ� ȣ��
    }

    // ��Ʈ�� ������ ���ư��� �Լ�
    void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro");
    }

    // ���� ���� ������ üũ�ϴ� �Լ� (���Ƿ� ����)
    bool GameOverConditionMet()
    {
        // ���� ���� ������ ���⿡ ����
        // ���� ���, �÷��̾��� ü���� 0�� �Ǹ� ���� ����
        return false;
    }

    // ���� ���� ���¿��� Ű �Է��� �����ϴ� �Լ�
    void Update()
    {
        // ���� ���� ������ ���� Ű �Է��� ����
        if (isGameOver)
            return;

        // ���� ���� ������ üũ�ϰ� ���� ���� ���¸� ����
        if (GameOverConditionMet())
        {
            SetGameOver();
        }
    }
}