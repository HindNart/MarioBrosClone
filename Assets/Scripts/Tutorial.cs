using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;

    private void Awake()
    {
        tutorial.SetActive(false);
    }

    public void Show()
    {
        tutorial.SetActive(true);
    }

    public void Hide()
    {
        tutorial.SetActive(false);
    }
}
