using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public float aceleracao = 10f;       // Taxa de aceleracao
    public float velocidadeMaxima = 15f; // Velocidade limite para a frente
    public float velocidadeVertical = 8f; // Velocidade para subir e descer

    public GameObject player1; // Referência ao Player1

    private Rigidbody2D rb;
    private float velocidadeAtualX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Impede a rotação do Rigidbody2D
    }

    void FixedUpdate()
    {
         // Acelera gradualmente
        velocidadeAtualX += aceleracao * Time.fixedDeltaTime;

        // Limita a velocidade maxima no eixo X
        velocidadeAtualX = Mathf.Clamp(velocidadeAtualX, 0f, velocidadeMaxima);


        // Logica de subir e descer (W/S)
        float inputVertical = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            inputVertical = 1f; // Sobe
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVertical = -1f; // Desce
        }

        float velocidadeAtualY = inputVertical * velocidadeVertical;

        // Aplicacao do movimento no Rigidbody2D
        // O carro vai para a frente (X) e se move para cima/baixo (Y) ao mesmo tempo
        rb.linearVelocity = new Vector2(velocidadeAtualX, velocidadeAtualY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            Destroy(gameObject); // Destrói o Player2
        }
        if (collision.gameObject.CompareTag("Lixo"))
        {
            velocidadeAtualX = 0f; // Reduz a velocidade para 0
            Destroy(collision.gameObject); // Destrói o objeto lixo
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            ChangeScene("Win"); // Chama a função para mudar de cena
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NitroAzul"))
        {
            velocidadeAtualX += 10f; // Aumenta a velocidade para frente
            Destroy(collision.gameObject); // Destroi o objeto Nitro
        }
        if (collision.gameObject.CompareTag("NitroVermelho"))
        {
            velocidadeAtualX -= 10f; // Diminui a velocidade para frente
            Destroy(collision.gameObject); // Destroi o objeto Nitro
        }
    }

    // Função para mudar de cena
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Chama a função para mudar de cena
    }
}
