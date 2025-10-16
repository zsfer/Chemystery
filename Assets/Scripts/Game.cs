using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Character
{
    MALE = 0,
    FEMALE = 1
}

public class Game : MonoBehaviour
{
    public Character SelectedCharacter { get; private set; }

    public static Game Instance { get; private set; }
    void Awake()
    {
        DontDestroyOnLoad( this );

        if ( Instance != null ) {
            Destroy( Instance.gameObject );
            Instance = this;
        } else {
            Instance = this;
        }
    }

    public void SelectCharacter( int c )
    {
        SelectedCharacter = (Character)c;
        StartGame();
    }

    public void StartGame()
    {
        var loadTask = SceneManager.LoadSceneAsync( "SCN_Game" );
        loadTask.completed += (o) => {
            PlayerController.Instance.SwitchCharacterSprite( SelectedCharacter );
        };
    }
}
