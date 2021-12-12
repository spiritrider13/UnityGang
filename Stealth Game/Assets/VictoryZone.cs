using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerObject;
    GameObject enemyObject;
    public GameObject victoryText;
    public GameObject button;
    void Start()
    {
        this.playerObject = GameObject.Find("Player");
        this.enemyObject = GameObject.Find("Enemy");
        victoryText.SetActive(false);
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider) {
        var player = collider.GetComponent<PlayerMovement>();
        if (player != null) {
            playerObject.SendMessage("reset");
            enemyObject.SendMessage("reset");
            victoryText.SetActive(true);
            button.SetActive(true);
        }
    }
}
