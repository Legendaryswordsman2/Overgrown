using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    Sprite hoverOverImage, normalImage;

    [SerializeField]
    string sceneName;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnMouseEnter()
    {
        image.sprite = hoverOverImage;
    }
    private void OnMouseExit()
    {
        image.sprite = normalImage;
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
