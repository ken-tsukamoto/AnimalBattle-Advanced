using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFieldImageView : MonoBehaviour
{
    [SerializeField] Button _nextFieldImageButton;
    [SerializeField] Button _returnFieldImageButton;

    [SerializeField] List<Sprite> _fieldImages;

    [SerializeField] Image _fieldImage;

    [SerializeField] Text _fieldText;

    int _fieldNumber = 0;

    public void OnClickFieldImageButton(){
        _nextFieldImageButton.onClick.AddListener(()=> ChangeFieldImage(true));
        _returnFieldImageButton.onClick.AddListener(()=> ChangeFieldImage(false));
    }

    void ChangeFieldImage(bool next){
        List<string> _changeFields = new List<string>()
        { 
            FieldConstant.Tokyo,
            FieldConstant.Savannah,
            FieldConstant.PacificOcean,
            FieldConstant.Space,
        };

        if (next == true)
        {
            _fieldNumber++;
            if (_fieldNumber == 4)
            {
                _fieldNumber = 0;
            }
            SetTextAndImage(_changeFields[_fieldNumber], _fieldNumber);
        }
        else
        {
            _fieldNumber--;
            if (_fieldNumber == -1)
            {
                _fieldNumber = 3;
            }
            SetTextAndImage(_changeFields[_fieldNumber], _fieldNumber);
        }
    }

    void SetTextAndImage(string fieldName, int fieldNumber){
        _fieldText.text = fieldName;
        _fieldImage.sprite = _fieldImages[fieldNumber];
    }
}
