using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "GlitchData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    // private void Start()
    // {

    // }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            print("Load Success");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);

            SceneManager.LoadScene(gameData.sceneIndex);
            GameObject.FindGameObjectWithTag("Player").transform.position = gameData.playerPos;
            GameObject.FindGameObjectWithTag("Player").transform.localScale = gameData.playerScale;
            // GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>().inventoryArray[0, 0] = gameData.inventoryArray[0, 0];
        }

        else
        {
            print("New file");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        gameData.playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameData.playerScale = GameObject.FindGameObjectWithTag("Player").transform.localScale;
        gameData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // gameData.inventoryArray[0, 0] = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>().inventoryArray[0, 0];
        // //       gameData.quickAccessArray;

        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadGameData();
        }
    }


}
