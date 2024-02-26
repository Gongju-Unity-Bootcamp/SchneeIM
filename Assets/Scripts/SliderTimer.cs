using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    public Slider timerSlider;
    public float maxTime = 10f;
    public float currentTime;
    public bool timerActive = false;

    void Start()
    {
        currentTime = maxTime;
        timerSlider.maxValue = maxTime;
        timerSlider.value = maxTime;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            timerSlider.value = currentTime;

            if (currentTime <= 0)
            {
                timerActive = false;
                timerSlider.interactable = false; // 슬라이더 비활성화
                Debug.Log("타이머 종료");
            }
        }
    }

    public void StartTimer()
    {
        timerActive = true;
        InvokeRepeating("DecreaseTime", 1f, 1f);
    }

    void DecreaseTime()
    {
        currentTime -= 1f;
        timerSlider.value = currentTime;

        if (currentTime <= 0)
        {
            timerActive = false;
            timerSlider.interactable = false; // 슬라이더 비활성화
            Debug.Log("타이머 종료");
            CancelInvoke("DecreaseTime"); // Invoke 반복 종료
        }
    }
}