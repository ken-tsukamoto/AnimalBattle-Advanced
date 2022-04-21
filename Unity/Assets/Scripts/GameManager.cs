using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    const string ElephantName = "ゾウ";
    const string LionName = "ライオン";
    const string ZebraName = "シマウマ";
    const string DolphinName = "イルカ";
    const string OrcaName = "シャチ";
    const string HumanName = "人間";

    const int ElephantPower = 100;
    const int LionPower = 80;
    const int ZebraPower = 30;
    const int DolphinPower = 30;
    const int OrcaPower = 80;
    const int HumanPower = 10;

    const string Tokyo = "東京";
    const string Savannah = "サバンナ";
    const string PacificOcean = "太平洋";
    const string Space = "宇宙";

    const string ElephantImage = "ゾウ2";
    const string LionImage = "ライオン2";
    const string ZebraImage = "シマウマ2";
    const string DolphinImage = "イルカ2";
    const string OrcaImage = "シャチ2";
    const string HumanImage = "人間2";

    const string Power = "攻撃力";
    const string Winner = "勝者";
    const string Draw = "Draw";

    const int FalseVsImageTime = 1;
    const int TrueSmokeAnimationTime = 1;
    const int FinishBattleTime = 2;

    [SerializeField] GameObject _mainGameObject;
    [SerializeField] GameObject _battleGameObject;
    [SerializeField] GameObject _playerCreatureSmoke;
    [SerializeField] GameObject _opponentCreatureSmoke;

    [SerializeField] Animator _playerBattleAnimator;
    [SerializeField] Animator _opponentBattleAnimator;

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
        CharacterDisplay(number);
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
        if (_fieldText.text == Tokyo)
        {
            _fieldText.text = Savannah;
            _fieldImage.sprite = _fieldImages[(int)Field.Savannah];
        }
        else if (_fieldText.text == Savannah)
        {
            _fieldText.text = PacificOcean;
            _fieldImage.sprite = _fieldImages[(int)Field.PacificOcean];
        }
        else if (_fieldText.text == PacificOcean)
        {
            _fieldText.text = Space;
            _fieldImage.sprite = _fieldImages[(int)Field.Space];
        }
        else
        {
            _fieldText.text = Tokyo;
            _fieldImage.sprite = _fieldImages[(int)Field.Tokyo];
        }
    }

    void ChangeReturnFieldImage()
    {
        if (_fieldText.text == Tokyo)
        {
            _fieldText.text = Space;
            _fieldImage.sprite = _fieldImages[(int)Field.Space];
        }
        else if (_fieldText.text == Space)
        {
            _fieldText.text = PacificOcean;
            _fieldImage.sprite = _fieldImages[(int)Field.PacificOcean];
        }
        else if (_fieldText.text == PacificOcean)
        {
            _fieldText.text = Savannah;
            _fieldImage.sprite = _fieldImages[(int)Field.Savannah];
        }
        else
        {
            _fieldText.text = Tokyo;
            _fieldImage.sprite = _fieldImages[(int)Field.Tokyo];
        }
    }

    void StartGame(){
        Creature elephant, lion, zebra, dolphin, orca, human;
        elephant = new Creature(ElephantName, ElephantPower, Savannah);
        lion = new Creature(LionName, LionPower, Savannah);
        zebra = new Creature(ZebraName, ZebraPower, Savannah);
        dolphin = new Creature(DolphinName, DolphinPower, PacificOcean);
        orca = new Creature(OrcaName, OrcaPower, PacificOcean);
        human = new Creature(HumanName, HumanPower, Tokyo);

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
            { _fieldImages[(int)Field.Tokyo], Tokyo },
            { _fieldImages[(int)Field.Savannah], Savannah },
            { _fieldImages[(int)Field.PacificOcean], PacificOcean },
            { _fieldImages[(int)Field.Space], Space },
        };

        Dictionary<string, int> new_creatures = new Dictionary<string, int>()
        {
            { ElephantImage, 1 },
            { LionImage, 2 },
            { ZebraImage, 3 },
            { DolphinImage, 4 },
            { OrcaImage, 5 },
            { HumanImage, 6 },
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

        StartCoroutine(BattleCoroutine());
    }

    void JudgeBattle(Creature player, Creature opponent, string field)
    {
        Dictionary<string, int> fieldScale = new Dictionary<string, int>()
        {
            {Tokyo, 15},
            {Savannah, 4},
            {PacificOcean, 6},
            {Space, 2}
        };

        if (player.field == field)
        {
            _playerScaledPower = player.power * fieldScale[player.field];
        }
        else if (field == Space)
        {
            _playerScaledPower = player.power / fieldScale[Space];
        }
        else 
        {
            _playerScaledPower = player.power;
        }

        if (opponent.field == field)
        {
            _opponentScaledPower = opponent.power * fieldScale[opponent.field];
        }
        else if (field == Space)
        {
            _opponentScaledPower = opponent.power / fieldScale[Space];
        }
        else 
        {
            _opponentScaledPower = opponent.power;
        }

        _playerCreatureText.text = player.field + "\n" + Power + ":" +
            _playerScaledPower + "\n" + player.name;

        _opponentCreatureText.text = opponent.field + "\n" + Power + ":" +
            _opponentScaledPower + "\n" + opponent.name;

        if (_playerScaledPower > _opponentScaledPower)
        {
            _battleText.text = Winner + "\n" + player.name;
        }
        else if (_playerScaledPower < _opponentScaledPower)
        {
            _battleText.text = Winner + "\n" + opponent.name;
        }
        else
        {
            _battleText.text = Draw;
        }
    }

    void NextGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void FinishGame()
    {
        Application.Quit();
    }

    IEnumerator BattleCoroutine(){
        _playerCreatureSmoke.SetActive(false);
        _opponentCreatureSmoke.SetActive(false);
        yield return new WaitForSeconds(FalseVsImageTime);
        _vsImage.enabled = false;
        _playerBattleAnimator.SetTrigger("PlayerBattleTrigger");
        _opponentBattleAnimator.SetTrigger("OpponentBattleTrigger");
        yield return new WaitForSeconds(TrueSmokeAnimationTime);
        _playerCreatureSmoke.SetActive(true);
        _opponentCreatureSmoke.SetActive(true);
        yield return new WaitForSeconds(FinishBattleTime);
        _playerCreatureSmoke.SetActive(false);
        _opponentCreatureSmoke.SetActive(false);
        _battleText.enabled = true;
        _battleTextImage.enabled = true;
        _nextGameButton.gameObject.SetActive(true);
    }
}