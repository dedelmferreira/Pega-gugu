using UnityEngine;

public class Player1 : MonoBehaviour
{
    [Header("Movimento para Frente (Eixo X)")]
    public float aceleracao = 10f;       // Taxa de acelera��o
    public float velocidadeMaxima = 15f; // Velocidade limite para a frente
    public float forcaFreio = 5f;        // Taxa de desacelera��o ao soltar o D

    [Header("Movimento Vertical (Eixo Y)")]
    public float velocidadeVertical = 8f; // Velocidade para subir e descer

    private Rigidbody2D rb;
    private float velocidadeAtualX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 1. L�GICA DE IR PARA A FRENTE (TECLA D)
        if (Input.GetKey(KeyCode.D))
        {
            // Acelera gradualmente enquanto segura o D
            velocidadeAtualX += aceleracao * Time.fixedDeltaTime;
        }
        else
        {
            // Desacelera suavemente quando solta o D
            if (velocidadeAtualX > 0)
            {
                velocidadeAtualX -= forcaFreio * Time.fixedDeltaTime;
                if (velocidadeAtualX < 0) velocidadeAtualX = 0;
            }
        }

        // Limita a velocidade m�xima no eixo X
        velocidadeAtualX = Mathf.Clamp(velocidadeAtualX, 0f, velocidadeMaxima);


        // 2. L�GICA DE SUBIR E DESCER (SETAS OU W/S)
        float inputVertical = Input.GetAxisRaw("Vertical");
        float velocidadeAtualY = inputVertical * velocidadeVertical;


        // 3. APLICA��O DO MOVIMENTO NO RIGIDBODY2D
        // O carro vai para a frente (X) e se move para cima/baixo (Y) ao mesmo tempo
        rb.linearVelocity = new Vector2(velocidadeAtualX, velocidadeAtualY);
    }
}