using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IaFPS2 : MonoBehaviour
{
    //Aqui é onde eu defino o componente de Ingeligencia
    public NavMeshAgent MeuSoldado;
    //Estados que O Soldado pode ter
    public enum MeusEstados { ronda, perseguir, atacar, esperar };
    public MeusEstados maquinaEstados;

    //Destinos que ele tem que fazer ronda
    public List<GameObject> Destinos;
    //Para onde vai no momento
    public GameObject DestinoReal;
    public float tempoTroca = 0;

    //Vida
    public int hp = 10;

    public void Start()
    {
        
        MeuSoldado = GetComponent<NavMeshAgent>();
        MeuSoldado.speed = 40;
        int sorteioDestino = Random.Range(0, Destinos.Count);
        DestinoReal = Destinos[sorteioDestino];
    }

    void Update()
    {
        //Calcula a Distancia entre esse Objeto e o Objeto Destino
        float DistanciaFinal = Vector3.Distance(
                transform.position, DestinoReal.transform.position);
        //Construir Maquina De Estado
        if(maquinaEstados == MeusEstados.ronda)
        {
            FazerRonda(DistanciaFinal);
            MeuSoldado.speed = 40;
            tempoTroca += Time.deltaTime;
            if (tempoTroca > 30)
            {
                maquinaEstados = MeusEstados.esperar;
                tempoTroca = 0;
            }
        }
        if(maquinaEstados == MeusEstados.esperar)
        {
            MeuSoldado.speed = 0;
            tempoTroca += Time.deltaTime;
            if(tempoTroca > 3)
            {
                maquinaEstados = MeusEstados.ronda;
                tempoTroca = 0;
            }
        }
        if(maquinaEstados == MeusEstados.atacar)
        {

        }
        if(maquinaEstados == MeusEstados.perseguir)
        {
            SeguirInimigo();
            if(DestinoReal == null)
            {
                maquinaEstados = MeusEstados.esperar;
                int sorteioDestino = Random.Range(0, Destinos.Count);
                DestinoReal = Destinos[sorteioDestino];
            }
        }

    }
    

    public void AvistarInimigo(GameObject InimigoAvistado)
    {
        if(maquinaEstados == MeusEstados.ronda || maquinaEstados == MeusEstados.esperar)
        {
            maquinaEstados = MeusEstados.perseguir;
            DestinoReal = InimigoAvistado;
        }
        
    }

    void SeguirInimigo()
    {
        MeuSoldado.SetDestination(DestinoReal.transform.position);
    }

    void FazerRonda(float DistanciaFinal)
    {
        //Faz com que o Objeto o Soldado vá até o destino
        MeuSoldado.SetDestination(DestinoReal.transform.position);
        //Se o destino está perto ele entre no if
        if (DistanciaFinal < 7)
        {
            //Sorteia um Novo caminho
            int novocaminho = Random.Range(0, Destinos.Count);
            DestinoReal = Destinos[novocaminho];

        }

    }
    public void TomarDano()
    {
        hp--;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider contato)
    {
        
    }
}
