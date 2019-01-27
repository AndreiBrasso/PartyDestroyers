using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReset : MonoBehaviour {

    private void Start()
    {
        GameManager.self.OnRestartGame += RestartGame;
    }

    private void OnDestroy()
    {
          GameManager.self.OnRestartGame -= RestartGame;
    }

    void RestartGame()
    {
        Destroy(this.gameObject);
    }
}
