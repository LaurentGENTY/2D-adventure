using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMouvement playerMouvement;
    public BoxCollider2D topCollider;

    public Text interactUI;

    // Start is called before the first frame update
    void Awake()
    {
        /* Récupérer le script de player mouvement pour dire que le joueur est en train d'escalader */
        playerMouvement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();

        /* Récupérer le texte permettant de conseiller le joueur */
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && playerMouvement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            /* Descendre de l'échelle */
            playerMouvement.isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMouvement.isClimbing = true;
            topCollider.isTrigger = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMouvement.isClimbing = false;
            topCollider.isTrigger = false;

            interactUI.enabled = false;
        }

    }
}
