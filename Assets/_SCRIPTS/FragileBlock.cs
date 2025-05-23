using Unity.Android.Gradle;
using UnityEngine;

public class FragileBlock : MonoBehaviour
{

    private SpriteRenderer _renderer;
    public Color hitOne, hitTwo;

    private int hitCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _renderer.color = hitOne;
            hitCount++;

            if (hitCount == 2)
            {
                _renderer.color = hitTwo;
            }

            if (hitCount == 3)
            {
                Destroy(this.gameObject);
            }

        }
    }

}
