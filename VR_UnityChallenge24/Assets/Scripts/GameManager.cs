using System;
using Meta.XR.MRUtilityKit;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [FormerlySerializedAs("spawner")] [SerializeField] private FindSpawnPositions Spawner;
    [SerializeField] private int BalloonCount;
    
    [SerializeField] private TMP_Text BalloonCountText;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TMP_Text GameOverText;

    private int _maxBalloons;
 
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _maxBalloons = BalloonCount;
        updateBalloonText();
    }

    public void SpawnObjects(GameObject obj, FindSpawnPositions.SpawnLocation spawnLocation, MRUKAnchor.SceneLabels label, int amount)
    {
        Spawner.SpawnAmount = amount;
        Spawner.Labels = label;
        Spawner.SpawnLocations = spawnLocation;
        Spawner.SpawnObject = obj;
        
        Spawner.StartSpawn();
    }
    
    public void SpawnObjects(SpawnConfig config)
    {
        SpawnObjects(config.objectToSpawn, config.spawnLocation, config.label, config.amount);
    }

    public void BalloonDestroyed()
    {
        BalloonCount -= 1;
        updateBalloonText();
        if(BalloonCount <= 0)
            GameOver(true);
    }
    
    public void Restart()
    {
        StartCoroutine(restartScene());
    }

    public void GameOver(bool victoryState)
    {
        BalloonCountText.text = "";
        GameOverUI.SetActive(true);

        if (victoryState)
            GameOverText.text = "Congratulations, you've successfully completed the exercise! \nPop the balloon to try again";
        else
            GameOverText.text = "Oops! You didn't complete the exercise this time. \nPop the balloon to try again";
    }
    
    private IEnumerator restartScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private void updateBalloonText()
    {
        BalloonCountText.text = $"{BalloonCount} / {_maxBalloons}";
    }
}
