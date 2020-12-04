using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private SpriteRenderer playerSr;

    private Queue<GameObject> afterImages = new Queue<GameObject>();
    
    public static AfterImagePool AfterImagePoolInstance { get; private set; }

    private void Awake()
    {
        AfterImagePoolInstance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instantiated = Instantiate(afterImagePrefab);
            instantiated.transform.SetParent(this.transform);
            instantiated.SetActive(false);
            afterImages.Enqueue(instantiated);
        }
    }

    public GameObject GetFromPool()
    {
        return afterImages.Dequeue();
    }

    public void AddToPool(GameObject toBeAdded)
    {
        toBeAdded.SetActive(false);
        afterImages.Enqueue(toBeAdded);
    }
    
}
