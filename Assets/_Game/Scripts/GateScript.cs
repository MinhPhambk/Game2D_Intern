using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.tag == "Player")
        {
            Invoke(nameof(LoadLevel2), 2f);
        }
    }

    private void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
