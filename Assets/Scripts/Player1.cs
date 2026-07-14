using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    public float aceleracao = 10f;       // Taxa de aceleracao
    public float velocidadeMaxima = 15f; // Velocidade limite para a frente
    public float velocidadeVertical = 8f; // Velocidade para subir e descer

    public GameObject player2; // Referência ao Player2

    private Rigidbody2D rb; // Referência ao Rigidbody2D do Player1
    private float velocidadeAtualX = 0f; // Velocidade atual no eixo X

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém a referência ao Rigidbody2D do Player1

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Impede a rotação do Rigidbody2D
    }

    void FixedUpdate()
    {
        // Acelera gradualmente
        velocidadeAtualX += aceleracao * Time.fixedDeltaTime;

        // Limita a velocidade maxima no eixo X
        velocidadeAtualX = Mathf.Clamp(velocidadeAtualX, 0f, velocidadeMaxima);


        // 2Logica de subir e descer (SETA PRA CIMA E BAIXO)
        float inputVertical = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVertical = 1f; // Sobe
        }
        else if (Input.GetKey(KeyCode.DownArrow))
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
        if (collision.gameObject.CompareTag("Player2"))
        {
            Destroy(gameObject); // Destrói o Player1
            ChangeScene("GameOver"); // Chama a função para mudar de cena
        }
        if (collision.gameObject.CompareTag("Lixo"))
        {
            velocidadeAtualX = 0f; // Reduz a velocidade para 0
            Destroy(collision.gameObject); // Destroi o objeto lixo
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
            velocidadeAtualX -= 10f; // Diminui a velocidade para frente
            Destroy(collision.gameObject); // Destroi o objeto Nitro
        }
        if (collision.gameObject.CompareTag("NitroVermelho"))
        {
            velocidadeAtualX += 10f; // Aumenta a velocidade para frente
            Destroy(collision.gameObject); // Destroi o objeto Nitro
        }
    }

    // Função para mudar de cena
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Chama a função para mudar de cena
    }
}