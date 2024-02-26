using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private float timer = 0f;
    private float switchTime = 8f;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if 8 seconds have passed
        if (timer >= switchTime)
        {
            SwitchToIntroScene();
        }
    }

    // Method to switch to the "Intro" scene
    void SwitchToIntroScene()
    {
        SceneManager.LoadScene("0_Title");
    }
}