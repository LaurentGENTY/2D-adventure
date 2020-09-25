using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool isInRange = false;

    public Animator animator;

    private Text interactUI;

    public int coinsToAdd;
    public AudioClip audioClip;

    void Awake()
    {

        /* Récupérer le texte permettant de conseiller le joueur */
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
            OpenChest();

    }

    private void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        Inventory.instance.AddCoins(coinsToAdd);

        AudioManager.instance.PlayClipAt(audioClip, transform.position);

        /* Une fois ouvert, le coffre ne doit plus pouvoir être trigger : on desactive le boxcollider */
        GetComponent<BoxCollider2D>().enabled = false;

        interactUI.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}
