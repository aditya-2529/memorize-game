using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Button> btns = new List<Button>();
    [SerializeField]
    private Sprite bgImage;
    [SerializeField]
    private GameObject gameArea;
    [SerializeField]
    private RectTransform canvasRect;

    public Sprite[] pu;
    public List<Sprite> gPu = new List<Sprite>();



    private bool fGuess, sGuess;
    private int cGuess,ccGuess,gGuess,fGuessIndex,sGuessIndex;
    private string fGuessName, sGuessName;


    void Awake(){
        pu = Resources.LoadAll<Sprite>("Sprites/Mem");
    }

    void Start(){
        createGameArea();
        GetButtons();
        AddListeners();
        AddGameP();
        shuffle(gPu);
        gGuess = gPu.Count / 2;
    }
    void createGameArea(){
        gameArea.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRect.rect.width,canvasRect.rect.height);
        RectTransform gamePanel = gameArea.GetComponentInChildren<RectTransform>();
        Debug.Log(gamePanel.position);
    }

    void GetButtons(){
        GameObject []ob = GameObject.FindGameObjectsWithTag("PBtn");
        for(int i = 0; i < ob.Length;i++){
            btns.Add(ob[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }
    void AddGameP(){
        int l = btns.Count, i = UnityEngine.Random.Range(0,pu.Length-4), fi = i;
        for(int j = 0; j < l;j++){
            if(j == l/2) i = fi;
            gPu.Add(pu[i]);
            i++;
        }
    }

    void AddListeners(){
        foreach(Button b in btns){
            b.onClick.AddListener(() => Pick());
        }
    }

    void Pick(){
        string anme = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        // Debug.Log();
        int index = int.Parse(anme.Substring(anme.Length-1));
        if(!fGuess){
            fGuess = true;
            fGuessIndex = index;
            fGuessName = gPu[fGuessIndex].name;
            // btns[fGuessIndex].image.sprite = gPu[fGuessIndex];
            StartCoroutine(RotateCard(fGuessIndex, false));
        }else if(!sGuess){
            sGuess = true;
            sGuessIndex = index;
            sGuessName = gPu[sGuessIndex].name;
            // btns[sGuessIndex].image.sprite = gPu[sGuessIndex];
            StartCoroutine(RotateCard(sGuessIndex, false));
            cGuess++;
            StartCoroutine(CheckIfMatch());
        }
    }
    IEnumerator CheckIfMatch(){
        yield return new WaitForSeconds(1f);
        if(fGuessName == sGuessName){
            yield return new WaitForSeconds(0.5f);
            btns[fGuessIndex].interactable = false;
            btns[sGuessIndex].interactable = false;
            
            btns[fGuessIndex].image.color = new Color(0,0,0,0);
            btns[sGuessIndex].image.color = new Color(0,0,0,0);
            CheckIfFinish();
        } else{
            yield return new WaitForSeconds(0.5f);
            // btns[fGuessIndex].image.sprite = bgImage;
            // btns[sGuessIndex].image.sprite = bgImage;
            StartCoroutine(RotateCard(fGuessIndex, true));
            StartCoroutine(RotateCard(sGuessIndex, true));
        }
        yield return new WaitForSeconds(0.5f);
        fGuess = sGuess = false;
    }
    private IEnumerator RotateCard(int ind,bool rev){
        if(rev){
            for(float i = 180f;i >= 0f;i-=10f){
                btns[ind].transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f){
                    btns[ind].image.sprite = bgImage;
                }
                yield return new WaitForSeconds(0.02f);
            }
        } else{
            for(float i = 0f;i < 180f;i+=10f){
                btns[ind].transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f){
                    btns[ind].image.sprite = gPu[ind];
                }
                yield return new WaitForSeconds(0.02f);
            }
        }

    }
    void CheckIfFinish(){
        ccGuess++;
        if(ccGuess == gGuess){
            Debug.Log("Guess: "+cGuess);
        }
    }
    void shuffle(List<Sprite> l){
        for(int i = 0;i<l.Count;i++){
            Sprite s = l[i];
            int r = UnityEngine.Random.Range(i,l.Count);
            l[i] = l[r];
            l[r] = s;
        }
    }
}
