using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject objContainer = new GameObject(pool.tag);

            objContainer.transform.SetParent(transform);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, objContainer.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn;
        int times;
        times = 0;
        do
        {
            objectToSpawn = poolDictionary[tag].Dequeue();
            poolDictionary[tag].Enqueue(objectToSpawn);
            times++;
            if (times >= poolDictionary[tag].Count)
            {
                return null;
            }
        } while (objectToSpawn.activeSelf);

        IPoolObject pooledObject = objectToSpawn.GetComponent<IPoolObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawnBefore();
        }

        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.SetActive(true);

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawnAfter();
        }

        return objectToSpawn;
    }
}
