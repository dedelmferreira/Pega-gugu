using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    [Header("Movimento para Frente (Eixo X)")]
    public float aceleracao = 10f;       // Taxa de aceleracao
    public float velocidadeMaxima = 15f; // Velocidade limite para a frente
    public float forcaFreio = 5f;        // Taxa de desaceleracao ao soltar o D

    [Header("Movimento Vertical (Eixo Y)")]
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


        // 2. LOGICA DE SUBIR E DESCER (W/S)
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

        // 3. APLICACAO DO MOVIMENTO NO RIGIDBODY2D
        // O carro vai para a frente (X) e se move para cima/baixo (Y) ao mesmo tempo
        rb.linearVelocity = new Vector2(velocidadeAtualX, velocidadeAtualY);
    }

    //Destruindo ao colidir com o Player2
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            Destroy(gameObject); // Destrói o Player2
        }
        if (collision.gameObject.CompareTag("Lixo"))
        {
            velocidadeAtualX = 0f; // Reduz a velocidade para 0
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            ChangeScene("Win"); // Chama a função para mudar de cena)
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
