using System;
using UnityEngine;

public class BreakTile : MonoBehaviour
{

    public GameObject pickupItemPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            Instantiate(pickupItemPrefab, transform.position, Quaternion.identity);
            pickupItemPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            Destroy(this.gameObject);
            Console.WriteLine("Object destoryed");
        }
    }

}
