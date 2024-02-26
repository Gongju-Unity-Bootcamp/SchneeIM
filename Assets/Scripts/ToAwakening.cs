using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAwakening : MonoBehaviour
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
            SwitchToEndingScene();
        }
    }

    // Method to switch to the "Intro" scene
    void SwitchToEndingScene()
    {
        SceneManager.LoadScene("7_Awakening");
    }
}