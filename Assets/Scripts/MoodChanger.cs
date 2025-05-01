using UnityEngine;

/// <summary>
/// change mood gameobject of character
/// </summary>
public class MoodChanger : MonoBehaviour
{
    public enum Moods { None, Normal, Happy, Anger }

    [SerializeField] GameObject normal;
    [SerializeField] GameObject happy;
    [SerializeField] GameObject anger;

    private void Start()
    {
        ChangeMood(Moods.Normal);
    }

    public void ChangeMood(Moods mood)
    {
        if (normal == null || happy == null || anger == null)
            return;

        switch (mood)
        {
            case Moods.Normal:
                Normal();
                break;
            case Moods.Happy:
                Happy();
                break;
            case Moods.Anger:
                Anger();
                break;
            default:
                Normal();
                Debug.Log("you selected None mode");
                break;
        }
    }

    private void Normal()
    {
        normal.SetActive(true);
        happy.SetActive(false);
        anger.SetActive(false);
    }

    private void Happy()
    {
        normal.SetActive(false);
        happy.SetActive(true);
        anger.SetActive(false);
    }

    private void Anger()
    {
        normal.SetActive(false);
        happy.SetActive(false);
        anger.SetActive(true);
    }
}
