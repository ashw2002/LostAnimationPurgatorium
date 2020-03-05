using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    //loads the game based on the currently saved file
    public void OnClick()
    {
        
        ItemInventory.II.onGameLoad();
    }
}
