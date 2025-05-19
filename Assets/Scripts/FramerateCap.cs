using UnityEngine;

public class FramerateCap : MonoBehaviour
{
    [SerializeField]
    private int targetFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
    }

}
