using UnityEngine;

public class RingManager : MonoBehaviour
{
    public RollPlayerSkills playerSkills;
    public AccuracyDetector perfectAccuracyDetector;
    public AccuracyDetector goodAccuracyDetector;

    void Update()
    {
        if (perfectAccuracyDetector.inRings > 0)
        {
            playerSkills.perfectAccuracy = true;
            playerSkills.goodAccuracy = false;
        }
        else if ((perfectAccuracyDetector.inRings == 0) && (goodAccuracyDetector.inRings > 0))
        {
            playerSkills.perfectAccuracy = false;
            playerSkills.goodAccuracy = true;
        }
        else
        {
            playerSkills.perfectAccuracy = false;
            playerSkills.goodAccuracy = false;
        }
    }
}
