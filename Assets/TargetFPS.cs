using UnityEngine;
public class TargetFPS : MonoBehaviour
{
    public int Target = 60;

    private void Awake()
    {
        Application.targetFrameRate = Target;
    }
    
    private void Update()
    {
        if (Application.targetFrameRate != Target)
        {
            Application.targetFrameRate = Target;
        }
    }
}
