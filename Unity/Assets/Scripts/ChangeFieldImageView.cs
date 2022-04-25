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

    List<string> _fieldsName = new List<string>()
    { 
        FieldConstant.Tokyo,
        FieldConstant.Savannah,
        FieldConstant.PacificOcean,
        FieldConstant.Space,
    };

    public void OnClickFieldImageButton()
    {
        _nextFieldImageButton.onClick.AddListener(()=> OnClickNextFieldImageButton());
        _returnFieldImageButton.onClick.AddListener(()=> OnClickReturnFieldImageButton());
    }

    void OnClickNextFieldImageButton()
    {
        _fieldNumber++;
        if (_fieldNumber == _fieldsName.Count)
        {
            _fieldNumber = 0;
        }
        SetTextAndImage(_fieldsName[_fieldNumber], _fieldNumber);
    }

    void OnClickReturnFieldImageButton()
    {
        _fieldNumber--;
        if (_fieldNumber <= 0)
        {
            _fieldNumber = _fieldsName.Count;
        }
        SetTextAndImage(_fieldsName[_fieldNumber - 1], _fieldNumber - 1);
    }

    void SetTextAndImage(string fieldName, int fieldNumber)
    {
        _fieldText.text = fieldName;
        _fieldImage.sprite = _fieldImages[fieldNumber];
    }
}
