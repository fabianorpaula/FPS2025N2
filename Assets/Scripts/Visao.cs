using UnityEngine;

public class Visao : MonoBehaviour
{
    //Alcance do meu raio
    public float alcance = 20f;
    public IaFPS2 Soldado;

   
    void Update()
    {
        RaycastHit hit;
        Vector3 direcao = transform.
            TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position,
            direcao, out hit, alcance)){

            if(hit.collider.tag == "Soldado")
            {
                if(hit.collider.gameObject != Soldado.gameObject)
                {
                    Debug.Log(Soldado.gameObject.name +
                    " Avistou: " + hit.collider.gameObject.name);
                    //Bateu em soldado
                    Soldado.AvistarInimigo(hit.collider.gameObject);
                    Debug.DrawRay(transform.position,
                            direcao * alcance, Color.yellow);
                }
                
            }
            else
            {
                //Bateu em algo
                Debug.DrawRay(transform.position,
                        direcao * alcance, Color.blue);
            }
        }
        else
        {
            //não bateu em nada
            Debug.DrawRay(transform.position,
                direcao * alcance, Color.red);
        }


    }
}
