using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public GameObject player;

    public GameObject cheatPanel;
    // Start is called before the first frame update

    private void Awake()
    {
       
    }

    void Start()
    {
        /// FindObjectOfType<AudioManager>().Play("LevelTheme");
        GameObject.FindGameObjectWithTag("CinemachineCamera").transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        StartCoroutine(SpawnPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPlayer()
    {
        Debug.Log("Player Spawn Oldu");

        yield return new WaitForSeconds(1);

        GameObject newPlayer = Instantiate(player, transform.position, Quaternion.identity);
        newPlayer.GetComponent<PlayerController>().cheatPanel = cheatPanel;

        GameObject.FindGameObjectWithTag("CinemachineCamera").GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform;
    }
}
