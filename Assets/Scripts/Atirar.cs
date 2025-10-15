using UnityEngine;

public class Atirar : MonoBehaviour
{
    //Alcance do meu raio
    public float alcance = 20f;
   

    void Update()
    {
        RaycastHit hit;
        Vector3 direcao = transform.
            TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position,
            direcao, out hit, alcance))
        {

            //Bateu em soldado
            hit.collider.gameObject.GetComponent<IaFPS2>().TomarDano();
            Debug.DrawRay(transform.position, direcao * alcance, Color.magenta); 
            }
            else
            {
                //Bateu em algo
                Debug.DrawRay(transform.position,
                        direcao * alcance, Color.blue);
            }
        }
     

}
