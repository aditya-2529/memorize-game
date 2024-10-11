using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button stBtn, exitBtn;
    void Awake(){
        ChangeHome();
        exitGame();
    }
    void ChangeHome(){
        stBtn.onClick.AddListener(()=>SceneManager.LoadScene(3));
    }
    void exitGame(){
        exitBtn.onClick.AddListener(()=>Application.Quit());
    }
}
