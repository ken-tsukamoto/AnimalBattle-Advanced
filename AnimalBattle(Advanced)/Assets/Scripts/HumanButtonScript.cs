using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanButtonScript : MonoBehaviour
{
    public Image creatureImage1;
    public Image creatureImage2;
    public Sprite characterImage;
    public GameObject deleteButton1;
    public GameObject deleteButton2;

    // Start is called before the first frame update
    void Start()
    {
        creatureImage1 = GameObject.Find("CreatureImage1").GetComponent<Image>();
        creatureImage1.enabled = false;
        creatureImage2 = GameObject.Find("CreatureImage2").GetComponent<Image>();
        creatureImage2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        if ((creatureImage1.enabled == false && creatureImage2.enabled == false) ||
                (creatureImage1.enabled == false && creatureImage2.enabled == true))
        {
            creatureImage1.sprite = characterImage;
            creatureImage1.enabled = true;
            deleteButton1.SetActive(true);
        }
        else if (creatureImage1.enabled == true && creatureImage2.enabled == false)
        {
            creatureImage2.sprite = characterImage;
            creatureImage2.enabled = true;
            deleteButton2.SetActive(true);
        }
    }
}
