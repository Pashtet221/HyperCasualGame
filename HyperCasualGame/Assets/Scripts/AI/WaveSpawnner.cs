using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Wave
{
   // public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawnner : MonoBehaviour
{
    public static WaveSpawnner instance {get; private set;}

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    //[SerializeField] private Text waveName;

    [SerializeField] private Button nextQuestionButton;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;

    public static UnityEvent DestroyGameObjectEvent = new UnityEvent();


    private GameObject[] totalEnemies;
    [SerializeField] TMP_Text[] aiAmoutUIText;


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        nextQuestionButton.onClick.AddListener(SpawnNextWave);
    }

    private void OnDisable()
    {
        nextQuestionButton.onClick.RemoveListener(SpawnNextWave);
    }


    public void UpdateAiUIText()
	{
		for (int i = 0; i < aiAmoutUIText.Length; i++) {
			SetAiText (aiAmoutUIText[i], totalEnemies.Length);
		}
	}

    void SetAiText (TMP_Text textMesh, int amount)
	{
        textMesh.text = amount.ToString();
	}

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        UpdateAiUIText();
        
        if (totalEnemies.Length == 0  )
        {
            Debug.Log("GameFinish");
        }
    }

    public void SpawnNextWave()
    {
        DestroyGameObjectEvent.Invoke();
        currentWaveNumber++;
        canSpawn = true;
    }


    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }

}
