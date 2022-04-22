using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureShowView : MonoBehaviour
{
    [SerializeField] Button _playerDeleteButton;
    [SerializeField] Button _opponentDeleteButton;

    [SerializeField] List<Button> _characterButtons;
    [SerializeField] List<Sprite> _characterImages;

    [SerializeField] Image _playerCreatureImage;
    [SerializeField] Image _opponentCreatureImage;

    public void SetCreatureShow()
    {
        _characterButtons[(int)CreatureConstant.Creature.Elephant].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Elephant));
        _characterButtons[(int)CreatureConstant.Creature.Lion].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Lion));
        _characterButtons[(int)CreatureConstant.Creature.Zebra].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Zebra));
        _characterButtons[(int)CreatureConstant.Creature.Dolphin].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Dolphin));
        _characterButtons[(int)CreatureConstant.Creature.Orca].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Orca));
        _characterButtons[(int)CreatureConstant.Creature.Human].onClick.AddListener(()=> CreatureShow((int)CreatureConstant.Creature.Human));

        _playerDeleteButton.onClick.AddListener(()=> PlayerCreatureHide());
        _opponentDeleteButton.onClick.AddListener(()=> OpponentCreatureHide());

        _playerDeleteButton.gameObject.SetActive(false);;
        _opponentDeleteButton.gameObject.SetActive(false);
    }

    void CreatureShow(int number)
    {
        if ((_playerCreatureImage.enabled == false && _opponentCreatureImage.enabled == false) ||
                (_playerCreatureImage.enabled == false && _opponentCreatureImage.enabled == true))
        {
            _playerCreatureImage.sprite = _characterImages[number];
            _playerCreatureImage.enabled = true;
            _playerDeleteButton.gameObject.SetActive(true);
        }
        else if (_playerCreatureImage.enabled == true && _opponentCreatureImage.enabled == false)
        {
            _opponentCreatureImage.sprite = _characterImages[number];
            _opponentCreatureImage.enabled = true;
            _opponentDeleteButton.gameObject.SetActive(true);
        }
    }
    void PlayerCreatureHide()
    {
        _playerDeleteButton.gameObject.SetActive(false);
        _playerCreatureImage.enabled = false;
    }

    void OpponentCreatureHide()
    {
        _opponentDeleteButton.gameObject.SetActive(false);
        _opponentCreatureImage.enabled = false;
    }
}
