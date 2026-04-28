using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public GameObject[] slides;

    int currentSlide = 0;

    void Start()
    {
        ShowSlide(0);
    }

    public void NextSlide()
    {
        currentSlide++;

        if (currentSlide >= slides.Length)
            currentSlide = slides.Length - 1;

        ShowSlide(currentSlide);
    }

    public void PreviousSlide()
    {
        currentSlide--;

        if (currentSlide < 0)
            currentSlide = 0;

        ShowSlide(currentSlide);
    }

    void ShowSlide(int index)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == index);
        }
    }
}