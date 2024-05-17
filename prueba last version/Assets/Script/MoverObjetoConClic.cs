using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjetoConClic : MonoBehaviour
{
    public string collisionTag = "Colisionador";
    public float moveSpeed = 5.0f;

    private bool isDragging = false;
    private Vector3 offset;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePosition = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePosition.z));
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            // Reproducir la animación cuando se colisiona con el objeto etiquetado como "Colisionador"
            if (animator != null)
            {
                // Inserta aquí el código para reproducir la animación
                animator.SetTrigger("NombreDeTuTriggerDeAnimacion");
            }
        }
    }
}