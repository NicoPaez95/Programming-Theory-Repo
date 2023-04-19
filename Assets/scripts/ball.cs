using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ball : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject ballPrefab;
    public float initialSpeed = 10f;
    public int numPoints = 10; // N�mero de puntos en la l�nea
    private Vector3 targetPosition;

    public Slider mySlider;

    private void Start()
    {
        
    }
    void Update()
    {
        initialSpeed = mySlider.value;
        // Obtener la posici�n del mouse en el mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Actualizar la posici�n final de la curva
            targetPosition = hit.point;
        }

        // Actualizar la posici�n de los puntos de la curva
        Vector3[] positions = new Vector3[numPoints];
        positions[0] = transform.position;
        for (int i = 1; i < numPoints - 1; i++)
        {
            float t = (float)i / (numPoints - 1);
            positions[i] = Vector3.Lerp(transform.position, targetPosition, t) + Vector3.up * Mathf.Sin(t * Mathf.PI) * 2f; // Ajustar la posici�n de los puntos intermedios para crear una curva m�s suave
        }
        positions[numPoints - 1] = targetPosition;
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);

        if (Input.GetMouseButtonUp(0))
        {
            // Instanciar la pelota en la posici�n actual del objeto
            GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // Obtener la direcci�n de la curva
            Vector3 curveDirection = (targetPosition - transform.position).normalized;

            // Obtener la velocidad inicial de la pelota
            float speed = curveDirection.magnitude * initialSpeed;

            // Aplicar una fuerza a la pelota en la direcci�n de la curva
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            rb.AddForce(curveDirection * speed, ForceMode.Impulse);
        }
    }


}
