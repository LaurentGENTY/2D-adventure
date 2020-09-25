using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    public static DialogueManager instance;

    public Animator animator;

    private bool isWriting = false;
    private string currentSentence;

    public AudioClip clickEffect;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un DialogueManager");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        /* lancer l'animation */
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        /* Stocke les phrases dans l'ordre */
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        AudioManager.instance.PlayClipAt(clickEffect, transform.position);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (isWriting)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isWriting = false;
            return;
        }

        /* On recupere la premiere phrase a afficher et on l'enleve */
        string sentence = sentences.Dequeue();
        currentSentence = sentence;

        /* Effet machine à ecrire */
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        isWriting = true;

        dialogueText.text = "";

        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        isWriting = false;
    }

    public void EndDialogue()
    {
        /* lancer l'animation */
        animator.SetBool("isOpen", false);
    }
}
