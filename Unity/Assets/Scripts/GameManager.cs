using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    const string _elephant = "ゾウ";
    const string _lion = "ライオン";
    const string _zebra = "シマウマ";
    const string _dolphin = "イルカ";
    const string _orca = "シャチ";
    const string _human = "人間";

    const int _elephantPower = 100;
    const int _lionPower = 80;
    const int _zebraPower = 30;
    const int _dolphinPower = 30;
    const int _orcaPower = 80;
    const int _humanPower = 10;

    const string _tokyo = "東京";
    const string _savannah = "サバンナ";
    const string _pacificOcean = "太平洋";
    const string _space = "宇宙";

    const string _elephantImage = "ゾウ2";
    const string _lionImage = "ライオン2";
    const string _zebraImage = "シマウマ2";
    const string _dolphinImage = "イルカ2";
    const string _orcaImage = "シャチ2";
    const string _humanImage = "人間2";

    const string _power = "攻撃力";
    const string _winner = "勝者";
    const string _draw = "Draw";

    [SerializeField] GameObject _mainGameObject;
    [SerializeField] GameObject _battleGameObject;

    [SerializeField] Button _nextFieldImageButton;
    [SerializeField] Button _returnFieldImageButton;
    [SerializeField] Button _playerDeleteButton;
    [SerializeField] Button _opponentDeleteButton;
    [SerializeField] Button _startGameButton;
    [SerializeField] Button _nextGameButton;
    [SerializeField] Button _finishGameButton;

    [SerializeField] Image _playerCreatureImage;
    [SerializeField] Image _opponentCreatureImage;
    [SerializeField] Image _playerCreatureBattleImage;
    [SerializeField] Image _opponentCreatureBattleImage;
    [SerializeField] Image _vsImage;
    [SerializeField] Image _battleTextImage;
    [SerializeField] Image _fieldImage;

    [SerializeField] List<Sprite> _characterImages;
    [SerializeField] List<Sprite> _characterBattleImages;
    [SerializeField] List<Sprite> _fieldImages;
    [SerializeField] List<Button> _characterButtons;

    [SerializeField] Text _fieldText;
    [SerializeField] Text _playerCreatureText;
    [SerializeField] Text _opponentCreatureText;
    [SerializeField] Text _battleText;

    int _playerCreatureNumber;
    int _opponentCreatureNumber;
    int _playerScaledPower;
    int _opponentScaledPower;

    enum Field {   
        Tokyo = 0,
        Savannah = 1,
        PacificOcean = 2,
        Space = 3,
    }
    enum Character
    {
        Elephant = 0,
        Lion = 1,
        Zebra = 2,
        Dolphin = 3,
        Orca = 4,
        Human = 5,
    }

    class Creature{
        public string name;
        public int power;
        public string field;

        public Creature(string name, int power, string field)
        {
            this.name = name;
            this.power = power;
            this.field = field;
        }
    }

    void Start()
    {
        _characterButtons[(int)Character.Elephant].onClick.AddListener(()=> SetImage((int)Character.Elephant));
        _characterButtons[(int)Character.Lion].onClick.AddListener(()=> SetImage((int)Character.Lion));
        _characterButtons[(int)Character.Zebra].onClick.AddListener(()=> SetImage((int)Character.Zebra));
        _characterButtons[(int)Character.Dolphin].onClick.AddListener(()=> SetImage((int)Character.Dolphin));
        _characterButtons[(int)Character.Orca].onClick.AddListener(()=> SetImage((int)Character.Orca));
        _characterButtons[(int)Character.Human].onClick.AddListener(()=> SetImage((int)Character.Human));

        _playerDeleteButton.onClick.AddListener(()=> SetPlayerDelete());
        _opponentDeleteButton.onClick.AddListener(()=> SetOpponentDelete());

        _nextFieldImageButton.onClick.AddListener(()=> ChangeNextFieldImage());
        _returnFieldImageButton.onClick.AddListener(()=> ChangeReturnFieldImage());

        _startGameButton.onClick.AddListener(()=> StartGame());
        _nextGameButton.onClick.AddListener(()=> NextGame());
        _finishGameButton.onClick.AddListener(()=> FinishGame());

        _battleGameObject.SetActive(false);

        _playerDeleteButton.gameObject.SetActive(false);;
        _opponentDeleteButton.gameObject.SetActive(false);
        _nextGameButton.gameObject.SetActive(false);
        
        _playerCreatureImage.enabled = false;
        _opponentCreatureImage.enabled = false;

        _battleText.enabled = false;
        _battleTextImage.enabled = false;
    }

    void SetImage(int number)
    {
        switch (number)
        {
            case 0:
                CharacterDisplay(number);
                break;
            case 1:
                CharacterDisplay(number);
                break;
            case 2:
                CharacterDisplay(number);
                break;
            case 3:
                CharacterDisplay(number);
                break;
            case 4:
                CharacterDisplay(number);
                break;
            case 5:
                CharacterDisplay(number);
                break;
        }
    }

    void SetPlayerDelete()
    {
        _playerDeleteButton.gameObject.SetActive(false);
        _playerCreatureImage.enabled = false;
    }

    void SetOpponentDelete()
    {
        _opponentDeleteButton.gameObject.SetActive(false);
        _opponentCreatureImage.enabled = false;
    }

    void CharacterDisplay(int number)
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

    void ChangeNextFieldImage()
    {
        if (_fieldText.text == _tokyo)
        {
            _fieldText.text = _savannah;
            _fieldImage.sprite = _fieldImages[(int)Field.Savannah];
        }
        else if (_fieldText.text == _savannah)
        {
            _fieldText.text = _pacificOcean;
            _fieldImage.sprite = _fieldImages[(int)Field.PacificOcean];
        }
        else if (_fieldText.text == _pacificOcean)
        {
            _fieldText.text = _space;
            _fieldImage.sprite = _fieldImages[(int)Field.Space];
        }
        else
        {
            _fieldText.text = _tokyo;
            _fieldImage.sprite = _fieldImages[(int)Field.Tokyo];
        }
    }

    void ChangeReturnFieldImage()
    {
        if (_fieldText.text == _tokyo)
        {
            _fieldText.text = _space;
            _fieldImage.sprite = _fieldImages[(int)Field.Space];
        }
        else if (_fieldText.text == _space)
        {
            _fieldText.text = _pacificOcean;
            _fieldImage.sprite = _fieldImages[(int)Field.PacificOcean];
        }
        else if (_fieldText.text == _pacificOcean)
        {
            _fieldText.text = _savannah;
            _fieldImage.sprite = _fieldImages[(int)Field.Savannah];
        }
        else
        {
            _fieldText.text = _tokyo;
            _fieldImage.sprite = _fieldImages[(int)Field.Tokyo];
        }
    }

    void StartGame(){
        Creature elephant, lion, zebra, dolphin, orca, human;
        elephant = new Creature(_elephant, _elephantPower, _savannah);
        lion = new Creature(_lion, _lionPower, _savannah);
        zebra = new Creature(_zebra, _zebraPower, _savannah);
        dolphin = new Creature(_dolphin, _dolphinPower, _pacificOcean);
        orca = new Creature(_orca, _orcaPower, _pacificOcean);
        human = new Creature(_human, _humanPower, _tokyo);

        Dictionary<int, Creature> creatures = new Dictionary<int, Creature>()
        {
            { 1, elephant },
            { 2, lion },
            { 3, zebra },
            { 4, dolphin },
            { 5, orca },
            { 6, human },
        };

        Dictionary<Sprite, string> fields = new Dictionary<Sprite, string>()
        {
            { _fieldImages[(int)Field.Tokyo], _tokyo },
            { _fieldImages[(int)Field.Savannah], _savannah },
            { _fieldImages[(int)Field.PacificOcean], _pacificOcean },
            { _fieldImages[(int)Field.Space], _space },
        };

        Dictionary<string, int> new_creatures = new Dictionary<string, int>()
        {
            { _elephantImage, 1 },
            { _lionImage, 2 },
            { _zebraImage, 3 },
            { _dolphinImage, 4 },
            { _orcaImage, 5 },
            { _humanImage, 6 },
        };

        Dictionary<int, Sprite> creatures_image = new Dictionary<int, Sprite>()
        {
            { 1, _characterBattleImages[(int)Character.Elephant] },
            { 2, _characterBattleImages[(int)Character.Lion] },
            { 3, _characterBattleImages[(int)Character.Zebra] },
            { 4, _characterBattleImages[(int)Character.Dolphin] },
            { 5, _characterBattleImages[(int)Character.Orca] },
            { 6, _characterBattleImages[(int)Character.Human] },
        };

        _mainGameObject.SetActive(false);
        _battleGameObject.SetActive(true);

        _playerCreatureNumber = new_creatures[_playerCreatureImage.sprite.name];
        _opponentCreatureNumber = new_creatures[_opponentCreatureImage.sprite.name];

        _playerCreatureBattleImage.sprite = creatures_image[_playerCreatureNumber];
        _opponentCreatureBattleImage.sprite = creatures_image[_opponentCreatureNumber];

        Creature player = creatures[_playerCreatureNumber];
        Creature opponent = creatures[_opponentCreatureNumber];

        string _field = fields[_fieldImage.sprite];

        JudgeBattle(player, opponent, _field);
        
        Invoke(nameof(EnabledVsImage), 1);

        Invoke(nameof(FinishBattle), 4);
    }

    void JudgeBattle(Creature player, Creature opponent, string field)
    {
        Dictionary<string, int> fieldScale = new Dictionary<string, int>()
        {
            {_tokyo, 15},
            {_savannah, 4},
            {_pacificOcean, 6},
            {_space, 2}
        };

        if (player.field == field)
        {
            _playerScaledPower = player.power * fieldScale[player.field];
        }
        else if (field == _space)
        {
            _playerScaledPower = player.power / fieldScale[_space];
        }
        else 
        {
            _playerScaledPower = player.power;
        }

        if (opponent.field == field)
        {
            _opponentScaledPower = opponent.power * fieldScale[opponent.field];
        }
        else if (field == _space)
        {
            _opponentScaledPower = opponent.power / fieldScale[_space];
        }
        else 
        {
            _opponentScaledPower = opponent.power;
        }

        _playerCreatureText.text = player.field + "\n" + _power + ":" +
            _playerScaledPower + "\n" + player.name;

        _opponentCreatureText.text = opponent.field + "\n" + _power + ":" +
            _opponentScaledPower + "\n" + opponent.name;

        if (_playerScaledPower > _opponentScaledPower)
        {
            _battleText.text = _winner + "\n" + player.name;
        }
        else if (_playerScaledPower < _opponentScaledPower)
        {
            _battleText.text = _winner + "\n" + opponent.name;
        }
        else
        {
            _battleText.text = _draw;
        }
    }

    void FinishBattle()
    {
        _battleText.enabled = true;
        _battleTextImage.enabled = true;
        _nextGameButton.gameObject.SetActive(true);
    }

    void EnabledVsImage()
    {
        _vsImage.enabled = false;
    }

    void NextGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void FinishGame()
    {
        Application.Quit();
    }
}