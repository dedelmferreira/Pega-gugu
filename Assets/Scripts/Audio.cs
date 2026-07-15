using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("AudioSource encontrado? " + (audioSource != null));
        Debug.Log("AudioClip existe? " + (audioSource != null && audioSource.clip != null));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLIDIU COM: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);

        if (audioSource == null)
        {
            Debug.Log("ERRO: AudioSource é NULL!");
            return;
        }

        if (audioSource.clip == null)
        {
            Debug.Log("ERRO: AudioClip é NULL!");
            return;
        }

        if (collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tocando som!");
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
