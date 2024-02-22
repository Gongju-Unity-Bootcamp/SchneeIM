using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // start ��ư ã�Ƽ� Ŭ�� �����ʿ� �޼��带 �߰��մϴ�.
        GameObject start = GameObject.Find("Start");
        if (start != null)
        {
            Button startComponent = start.GetComponent<Button>();
            if (startComponent != null)
            {
                startComponent.onClick.AddListener(ChangeScene);
            }
        }

        // ��ư B�� ã�Ƽ� Ŭ�� �����ʿ� �޼��带 �߰��մϴ�.
        GameObject Quit = GameObject.Find("Quit");
        if (Quit != null)
        {
            Button QuitComponent = Quit.GetComponent<Button>();
            if (QuitComponent != null)
            {
                QuitComponent.onClick.AddListener(QuitGame);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ��ȯ�ϴ� �޼����Դϴ�.
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Hospital�� Ŭ���߽��ϴ�. ���� ��ȯ�մϴ�.");
    }

    // ������ �����ϴ� �޼����Դϴ�.
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        Debug.Log("Quit�� Ŭ���߽��ϴ�. ������ �����մϴ�.");
    }
}
