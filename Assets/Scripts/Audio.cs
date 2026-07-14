using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            audioSource.Play(); // Play the audio when colliding with Player2
        }
        if (collision.gameObject.CompareTag("Player1"))
        {
            audioSource.Play(); // Play the audio when colliding with Player1
        }
    }
}
