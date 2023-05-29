using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

   public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

   public void DeleteSave()
    {
        File.Delete(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json");
        File.Delete(Application.dataPath + Path.AltDirectorySeparatorChar + "SavePopulation.json");
        File.Delete(Application.dataPath + Path.AltDirectorySeparatorChar + "Population.json");
    }
}
