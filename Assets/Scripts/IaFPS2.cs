using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

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

    //ParametrosJOgo
    public int hp = 10;
    public int bonushp = 0;
    public float speed = 20;
    public float bonusspeed = 0;
    public float visao = 20;
    public float mira = 15;
    public float seguir = 10;
    public float atirar = 10;



    //Animacao
    public Animator animator;

    //Acessa a Visão
    public Visao minhaVisao;

    //Deve Acessar a Arma
    public Atirar minhaArma;

    public void Start()
    {
        
        DefinirParametros();
        MeuSoldado = GetComponent<NavMeshAgent>();
        MeuSoldado.speed = speed;
        int sorteioDestino = Random.Range(0, Destinos.Count);
        DestinoReal = Destinos[sorteioDestino];
    }


    void DefinirParametros()
    {
        int sorteioNascimento = Random.Range(0, Destinos.Count);
        transform.position = Destinos[sorteioNascimento].transform.position;
        minhaArma.alcance += mira;
        minhaVisao.alcance += visao;
        hp += bonushp;
        speed += bonusspeed;
    }

    void Update()
    {

        if (DestinoReal == null)
        {
            maquinaEstados = MeusEstados.ronda;
            int sorteioDestino = Random.Range(0, Destinos.Count);
            DestinoReal = Destinos[sorteioDestino];
        }

        //Calcula a Distancia entre esse Objeto e o Objeto Destino
        float DistanciaFinal = Vector3.Distance(
                transform.position, DestinoReal.transform.position);
        //Construir Maquina De Estado
        if(maquinaEstados == MeusEstados.ronda)
        {
            animator.SetBool("Tiro", false);
            FazerRonda(DistanciaFinal);
            MeuSoldado.speed = speed;
            tempoTroca += Time.deltaTime;
            if (tempoTroca > 30)
            {
                maquinaEstados = MeusEstados.esperar;
                tempoTroca = 0;
            }
        }
        if(maquinaEstados == MeusEstados.esperar)
        {
            animator.SetBool("Tiro", false);
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
            MeuSoldado.speed = speed / 2;
            SeguirInimigo();
            animator.SetBool("Tiro", true);
            var dir = (DestinoReal.transform.position - transform.position).normalized;
            var rot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 8f * Time.deltaTime);
            if (DistanciaFinal >seguir)
            {
                maquinaEstados = MeusEstados.perseguir;
            }
        }
        if(maquinaEstados == MeusEstados.perseguir)
        {
            MeuSoldado.speed = speed;
            animator.SetBool("Tiro", false);
            SeguirInimigo();
            if(DistanciaFinal < atirar)
            {
                maquinaEstados = MeusEstados.atacar;
            }


        }


    }
    

    public void AvistarInimigo(GameObject InimigoAvistado)
    {
        if(maquinaEstados == MeusEstados.ronda || maquinaEstados == MeusEstados.esperar)
        {
            float DistanciaFinal = Vector3.Distance(
               transform.position, DestinoReal.transform.position);
            if(DistanciaFinal < atirar)
            {
                maquinaEstados = MeusEstados.atacar;
            }else
            {
                maquinaEstados = MeusEstados.perseguir;
            }
                
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

    public void MeuTiro()
    {
        minhaArma.Atirando();
    }
}
