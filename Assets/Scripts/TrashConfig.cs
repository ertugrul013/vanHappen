using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashConfig : MonoBehaviour
{
    public Trash _trash = new Trash();

    public Guid GUID;

    public bool isGame2;

    private void Update()
    {
        if (!isGame2)
        {
            //billboard functions
            this.transform.forward = -Camera.main.transform.forward;
        }
    }

    public Guid Generateuid()
    {
        if (GUID != null)
        {
            GUID = Guid.NewGuid();
            _trash.GUID = GUID;
        }

        return GUID;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            var tag = other.gameObject.tag;
            if (other.gameObject.layer == 9)
            {
                if (tag == _trash.mytype.ToString())
                {
                    /*
                    if (_trash.mytype == Trash.Type.chemicals)
                    {
                        Gamemanager.instance.powerUpManager.PowerUpHandeler(this.gameObject.GetComponent<TrashConfig>());
                    }
                    */
                    Gamemanager.instance._score += _trash.amountOfScore;
                    UIController.instance.setScoreText(Gamemanager.instance._score);
                    Destroy(this.gameObject);
                }
                else
                {
                    Gamemanager.instance.CurrentLives--;
                    UIController.instance.SetLifeUI(Gamemanager.instance.CurrentLives);
                    Destroy(this.gameObject);
                }
            }

            if (other.gameObject.CompareTag("wall"))
            {
                Gamemanager.instance.CurrentLives--;
                UIController.instance.SetLifeUI(Gamemanager.instance.CurrentLives);
                Destroy(this.gameObject);
            }

        }

    }
}