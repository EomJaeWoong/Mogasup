using UnityEngine;

public class GolfGameManager : MonoBehaviour
{
    public static GolfGameManager singleton;

    [HideInInspector]
    public int currentLevelIndex;
    [HideInInspector]
    public GameStatus gameStatus = GameStatus.None;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public enum GameStatus
{
    None,
    Playing,
    Failed,
    Complete
}