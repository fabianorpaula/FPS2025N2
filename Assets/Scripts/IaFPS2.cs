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

        }
        if(maquinaEstados == MeusEstados.esperar)
        {

        }
        if(maquinaEstados == MeusEstados.atacar)
        {

        }
        if(maquinaEstados == MeusEstados.perseguir)
        {

        }
        if (estadoMedo == false)
        {
            //Faz com que o Objeto o Alvo vá até o destino
            MeuSoldado.SetDestination(DestinoReal.transform.position);
            //Se o destino está perto ele entre no if
            if (DistanciaFinal < 7)
            {
                //Sorteia um Novo caminho
                int novocaminho = Random.Range(0, Destinos.Count);
                DestinoReal = Destinos[novocaminho];

            }
        }
        if (estadoMedo == true)
        {
            MeuSoldado.SetDestination(DestinoReal.transform.position);
            if (DistanciaFinal < 1)
            {
                MeuSoldado.speed = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider contato)
    {
        if (contato.gameObject.tag == "Grama")
        {
            Debug.Log("VI GRAMA" + contato.gameObject.name);
            if (estadoMedo == true)
            {
                DestinoReal = contato.gameObject;
                Debug.Log("ESSA É A GRAMA MAIS PERTO");
            }
        }

        if (contato.gameObject.tag == "Lobo")
        {
            estadoMedo = true;
            Debug.Log("Estou com Medo");
        }
    }
}
