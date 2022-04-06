using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonScript : MonoBehaviour
{
    public Text fieldText;
    public Sprite TokyoImage;
    public Sprite SavannahImage;
    public Sprite PacificOceanImage;
    public Sprite SpaceImage;
    private Image fieldImage;

    // Start is called before the first frame update
    void Start()
    {
        fieldImage = GameObject.Find("FieldImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (fieldText.text == "����")
        {
            fieldText.text = "�F��";
            fieldImage.sprite = SpaceImage;
        }
        else if (fieldText.text == "�F��")
        {
            fieldText.text = "�����m";
            fieldImage.sprite = PacificOceanImage;
        }
        else if (fieldText.text == "�����m")
        {
            fieldText.text = "�T�o���i";
            fieldImage.sprite = SavannahImage;
        }
        else
        {
            fieldText.text = "����";
            fieldImage.sprite = TokyoImage;
        }
    }
}
