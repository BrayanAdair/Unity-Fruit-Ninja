using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPos = new Vector3(cursorPos.x, cursorPos.y);
            transform.position = cursorPos;
        }
    }
}
