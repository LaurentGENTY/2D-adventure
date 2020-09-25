using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private Animator fadeSystem;

    public AudioClip nextLevelSoundEffect;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Changer de scene */
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(nextLevelSoundEffect, transform.position);
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        /* Sauvegarder les données avant de passer à un autre niveau */
        LoadAndSaveData.instance.SaveData();

        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

    }
}
