using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStairsToFinal: MonoBehaviour
{
    private float timer = 0f;
    private float switchTime = 9f;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if 8 seconds have passed
        if (timer >= switchTime)
        {
            SwitchToAnubisArea();
        }
    }

    // Method to switch to the "Intro" scene
    void SwitchToAnubisArea()
    {
        SceneManager.LoadScene("5_StairsToFinal");
    }
}