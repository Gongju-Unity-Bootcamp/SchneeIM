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
        // start 버튼 찾아서 클릭 리스너에 메서드를 추가합니다.
        GameObject start = GameObject.Find("Start");
        if (start != null)
        {
            Button startComponent = start.GetComponent<Button>();
            if (startComponent != null)
            {
                startComponent.onClick.AddListener(ChangeScene);
            }
        }

        // 버튼 B를 찾아서 클릭 리스너에 메서드를 추가합니다.
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

    // 씬을 전환하는 메서드입니다.
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Hospital를 클릭했습니다. 씬을 전환합니다.");
    }

    // 게임을 종료하는 메서드입니다.
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        Debug.Log("Quit를 클릭했습니다. 게임을 종료합니다.");
    }
}
