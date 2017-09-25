using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float playerSpeed;
    public Vector3 playerPosition;
    public GameObject startPoint;
    //private Rigidbody2D playerBody;
    private bool moveUp, moveDown, moveLeft, moveRight;
    private BoxCollider2D[] playerColliders;
    private GameObject[] walls;
    private GameObject[] floors;
    private GameObject[] teleports;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start() {
        playerPosition = transform.position;    //Posicion actual del jugador
       //playerBody = this.GetComponent<Rigidbody2D> ();
        playerColliders = this.GetComponents<BoxCollider2D>();
        walls = GameObject.FindGameObjectsWithTag("Wall");
        floors = GameObject.FindGameObjectsWithTag("Floor");
        teleports = GameObject.FindGameObjectsWithTag("Teleport");
        moveUp = true;
        moveDown = true;
        moveLeft = true;
        moveRight = true;
    }

    // Update is called once per frame
    void Update() {
        DetectWallCollision();
        DetectTeleport();
        Movement(); //Funcion de movimiento del jugador
        GoMainMenu();
        DetectFloorCollision();
    }

    void GoMainMenu() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("Main Menu");
        }
    }

    void DetectTeleport() {
        foreach (GameObject teleport in teleports) {
            if (playerColliders[0].IsTouching(teleport.GetComponent<BoxCollider2D>()) &&
                teleport.GetComponent<TeleScript>().isWorking) {
                //Debug.Log(teleport.GetComponent<TeleScript>().target.transform.position);
                targetPosition = teleport.GetComponent<TeleScript>().target.transform.position;
                targetPosition.z = -1.5f;
                this.transform.position = targetPosition;
                playerPosition = this.transform.position;
                return;
            }
        }
    }

    void DetectWallCollision() {
        
        foreach (GameObject wall in walls) {
            if (playerColliders[1].IsTouching(wall.GetComponent<BoxCollider2D>())) {
                moveUp = false;
            }
            if (playerColliders[2].IsTouching(wall.GetComponent<BoxCollider2D>())) {
                moveRight = false;
            }
            if (playerColliders[3].IsTouching(wall.GetComponent<BoxCollider2D>())) {
                moveLeft = false;
            }
            if (playerColliders[4].IsTouching(wall.GetComponent<BoxCollider2D>())) {
                moveDown = false;
            }
        }
    }

    void DetectFloorCollision() {
        
        foreach (GameObject floor in floors) {
            if (playerColliders[1].IsTouching(floor.GetComponent<BoxCollider2D>())) {
                moveUp = true;
            }
            if (playerColliders[2].IsTouching(floor.GetComponent<BoxCollider2D>())) {
                moveRight = true;
            }
            if (playerColliders[3].IsTouching(floor.GetComponent<BoxCollider2D>())) {
                moveLeft = true;
            }
            if (playerColliders[4].IsTouching(floor.GetComponent<BoxCollider2D>())) {
                moveDown = true;
            }
        }
    }


    void Movement() {
        if (Input.GetKey(KeyCode.UpArrow) && transform.position == playerPosition && moveUp) {
            playerPosition += Vector3.up / 2;
        } else if (Input.GetKey(KeyCode.DownArrow) && transform.position == playerPosition && moveDown) {
            playerPosition += Vector3.down / 2;
        } else if (Input.GetKey(KeyCode.LeftArrow) && transform.position == playerPosition && moveLeft) {
            playerPosition += Vector3.left / 2;
        } else if (Input.GetKey(KeyCode.RightArrow) && transform.position == playerPosition && moveRight) {
            playerPosition += Vector3.right / 2;
        }

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * playerSpeed);

    }

}