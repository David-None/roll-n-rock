using UnityEngine;

public class AccuracyDetector : MonoBehaviour
{

    public RollPlayerSkills playerSkills;
    public int inRings;
    private int perfectRings;

    void Start()
    {
        inRings = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bigScore"))
        {
            inRings++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bigScore"))
        {
            inRings--;
        }
    }
}
