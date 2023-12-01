using UnityEngine;

public class InimigoEstatico : MonoBehaviour
{
    // Variáveis para controlar a vida do inimigo
    public int vidaMaxima = 1;
    private int vidaAtual;

    // Função de inicialização
    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    // Função para tratar o acerto no inimigo
    public void SofrerDano(int quantidadeDano)
    {
        vidaAtual -= quantidadeDano;

        // Verifica se a vida chegou a zero
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // Função chamada quando o inimigo morre
    void Morrer()
    {
        // Adicione aqui qualquer lógica que você deseja quando o inimigo morre
        Debug.Log("Inimigo morreu!");

        // Pode destruir o objeto ou desativá-lo, dependendo da sua necessidade
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}