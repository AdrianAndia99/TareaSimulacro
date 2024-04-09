using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3; 

    private Vector3 targetPosition;

    // M�todo para establecer la posici�n objetivo de la bala
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }

}
