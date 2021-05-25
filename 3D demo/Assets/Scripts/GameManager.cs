using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public Text timeText;
    public Text recordText;

    public GameObject level;
    public GameObject bulletSpawnerPrefab;
    public GameObject itemPrefab;
    int prevItemCheck;

    private List<Vector3> listbulletSpawners = new List<Vector3>();
    int spawnCounter = 0;

    public float surviveTime;
    private bool isGameover;

    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameover = false;

       
        listbulletSpawners.Add(new Vector3(-8f, 1f, 8f));
        listbulletSpawners.Add(new Vector3(8f, 1f, 8f));
        listbulletSpawners.Add(new Vector3(8f, 1f, -8f));
        listbulletSpawners.Add(new Vector3(-8f, 1f, -8f));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
        {
            surviveTime += Time.deltaTime;

            timeText.text = "Time: " + (int)surviveTime;

            if(surviveTime % 5f <= 0.01f && prevItemCheck == 4)
            {
                Vector3 randpos = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8, 8f));

                GameObject item = Instantiate(itemPrefab, randpos, Quaternion.identity);
                item.transform.parent = level.transform;
                item.transform.localPosition = randpos;
            }
            prevItemCheck = (int)(surviveTime % 5f);

            if(surviveTime <5f && spawnCounter == 0)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, listbulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = listbulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if(surviveTime >= 5f && surviveTime < 10f && spawnCounter == 1)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, listbulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = listbulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 10f && surviveTime < 15f && spawnCounter == 2)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, listbulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = listbulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            else if (surviveTime >= 15f && surviveTime < 20f && spawnCounter == 3)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, listbulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = listbulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
        }

        else
        {

            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("NewScene");
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        PlayerPrefs.DeleteKey("BestTime");
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time: " + (int)bestTime;
    }
}
