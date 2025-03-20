using UnityEngine;
using UnityEngine.SceneManagement;

public class RingSpawner : MonoBehaviour
{
    public GameObject player;
    public Transform parentCanvas;
    public GameObject[] ringPool;

    private int ringIndex;    
    private Vector3 startPos;

    private AudioSource music;
    void Start()
    {
        
        ringIndex = 0;
        startPos = new Vector3(1070, 215, 0);
        
        music = GetComponent<AudioSource>();
        music.Play();
        music.Pause();
    }

    public void SpawnRing()
    {
        //Activate the rings in the pool for the rhythm sequence
        ringPool[ringIndex].SetActive(true);
        ringIndex++;
        if (ringIndex >= ringPool.Length-1)
        {
            ringIndex = 0;
        }
    }

    public void StartMusic()
    {
        music.UnPause();
    }

    public void EndLevel()
    {        
        SceneManager.LoadScene("MainMenu");
    }
}
