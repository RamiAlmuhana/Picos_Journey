using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void MyGamePlay()
    {
        SceneManager.LoadScene("MyGameLevel1");
    }
    
    public void AllLevels()
    {
        SceneManager.LoadScene("AllLevels");
    }
    
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
    
    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }
    
    public void Level6()
    {
        SceneManager.LoadScene("Level6");
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    
}