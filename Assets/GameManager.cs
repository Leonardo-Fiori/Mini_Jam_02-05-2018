using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    #region"singleton"
    private GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public GameObject world;
    public NavMeshSurface surface;

    public float spawnIntervalX = 0f;
    public float spawnOffsetY = 0f;
    public float spawnIntervalZ = 0f;

    public GameObject[] rockPrefabs;
    public int rocksNumber = 10;

    public GameObject sheepPrefab;
    public int sheepsNumber = 10;

    private float timer = 0f;
    public Text testo;

    /* NON UTILIZZATA: MAPPA FATTA A MANO */
    private void SpawnRocks()
    {
        for(int i = 0; i < rocksNumber; i++)
        {
            GameObject rock = Instantiate(rockPrefabs[Random.Range(0, rockPrefabs.Length - 1)]);
            rock.transform.position = new Vector3(Random.Range(-spawnIntervalX, spawnIntervalX), spawnOffsetY, Random.Range(-spawnIntervalZ, spawnIntervalZ));
            while(Physics.OverlapSphere(rock.transform.position,2f,1 << 8).Length > 0)
            {
                rock.transform.position = new Vector3(Random.Range(-spawnIntervalX, spawnIntervalX), spawnOffsetY, Random.Range(-spawnIntervalZ, spawnIntervalZ));
            }
            rock.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-45f, 45f), Random.Range(0f, 360f), Random.Range(-45f, 45f)));
            rock.transform.localScale *= Random.Range(0.9f, 1.1f);
        }
    }

    private void SpawnSheeps()
    {
        for (int i = 0; i < sheepsNumber; i++)
        {
            GameObject spawnedSheep = Instantiate(sheepPrefab, world.transform, false);
            spawnedSheep.SetActive(false);

            float x = Random.Range(-spawnIntervalX, spawnIntervalX);
            float y = Random.Range(-spawnOffsetY, spawnOffsetY);
            float z = Random.Range(-spawnIntervalZ, spawnIntervalZ);
            spawnedSheep.transform.position = new Vector3(x, y, z);

            while (Physics.OverlapSphere(spawnedSheep.transform.position, 1f, 1 << 9).Length > 0)
            {
                x = Random.Range(-spawnIntervalX, spawnIntervalX);
                y = Random.Range(-spawnOffsetY, spawnOffsetY);
                z = Random.Range(-spawnIntervalZ, spawnIntervalZ);
                spawnedSheep.transform.position = new Vector3(x, y, z);
            }

            spawnedSheep.SetActive(true);
        }
    }

    void Start () {
        SpawnRocks(); // <- LA MAPPA PER ORA E' FATTA A MANO
        surface.BuildNavMesh();
        SpawnSheeps();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnSheeps();
        }

        testo.text = "Pecore: " + sheepsNumber + "   Tempo: " + Mathf.RoundToInt(timer);
    }

    public void PecoraMangiata()
    {
        sheepsNumber--;

        if (sheepsNumber <= 0)
        {
            SceneManager.LoadScene("Main");
        }
    }

}
