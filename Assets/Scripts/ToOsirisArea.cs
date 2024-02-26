using UnityEngine;
using UnityEngine.SceneManagement;

public class ToOsirisArea : MonoBehaviour
{
    private float timer = 0f;
    private float switchTime = 10f;

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
        SceneManager.LoadScene("4_OsirisArea");
    }
}