using UnityEngine;
using UnityEngine.SceneManagement;

public class OsirisAreaQuizController : MonoBehaviour
{
    // Ű �Է��� Ȯ���ϴ� �Լ�
    void Update()
    {
        // Ű���� ���� Ű 1, 2, 3�� ������ ���� ����
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

    // ���� ������ �̵��ϴ� �Լ�
    void MoveToCorrectScene()
    {
        SceneManager.LoadScene("OsirisArea_2");
    }

    // ���� ������ �̵��ϴ� �Լ�
    void MoveToWrongScene()
    {
        SceneManager.LoadScene("OsirisArea_3");
    }
}
