using UnityEditor.Build;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        { 

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            Destroy(this.gameObject);
            playerMovement.coinScore++;
            playerMovement.CoinText.text = "Coins: " + playerMovement.coinScore.ToString();
        }
    }

}
