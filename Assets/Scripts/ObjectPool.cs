using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool me;

    private Dictionary<string, Queue<GameObject>> pool;

    private int maxCount = int.MaxValue;
    public int MaxCount
    {
        get { return maxCount; }
        set
        {
            maxCount = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    void Awake()
    {
        me = this;
        pool = new Dictionary<string, Queue<GameObject>>();

    }

    public GameObject GetObject(GameObject go,Vector3 position,Quaternion rotation)
    {
        if(!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        if(pool[go.name].Count == 0)
        {
            GameObject newObject = Instantiate(go, position, rotation);
            newObject.name = go.name;

            return newObject;
        }

        GameObject nextObject=pool[go.name].Dequeue();
        nextObject.SetActive(true);
        nextObject.transform.position = position;
        nextObject.transform.rotation = rotation;
        return nextObject;
    }

    public void PutObject(GameObject go,float t)
    {
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        if (pool[go.name].Count >= MaxCount)
            Destroy(go,t);
        else
            StartCoroutine(ExecutePut(go,t));
    }

    private IEnumerator ExecutePut(GameObject go, float t)
    {
        yield return new WaitForSeconds(t);
        go.SetActive(false);
        pool[go.name].Enqueue(go);
    }

    public void Preload(GameObject go,int number)
    {
        if (!pool.ContainsKey(go.name))
        {
            pool.Add(go.name, new Queue<GameObject>());
        }
        for (int i = 0; i < number; i++)
        {
            GameObject newObject = Instantiate(go);
            newObject.name = go.name;
            newObject.SetActive(false);
            pool[go.name].Enqueue(newObject);
        }
    }

}