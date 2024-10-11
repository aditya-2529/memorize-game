using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform arent;
    
    void Start(){
        int k = 1;
        for(int i = 0;i< 100;i++){
            GameObject gg = Instantiate(prefab,arent,false);
            gg.name = "Level "+k;
            Button []btns = gg.GetComponentsInChildren<Button>();
            for(int j = 0;j<btns.Length;j++){
                // btns[j].name = "Button Level"+(j+1);
                TextMeshProUGUI tt = btns[j].GetComponentInChildren<TextMeshProUGUI>();
                tt.text = ""+(k);
                k++;
            }
        }
    }
}
