using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(CardImageLocator))]
public class CardScrollView : MonoBehaviour
{
    [SerializeField]
    private CardUI _cardUIPrefab;
    [SerializeField]
    private RectTransform _cardUIParent;
    private CardImageLocator _cardImageLocator;

    private List<CardUI> _cardUIPool = new List<CardUI>();
    private List<CardUI> _activeCardUI = new List<CardUI>();
    private List<CardType> _cardPool;

    private const int CARD_UI_POOL_SIZE = 6;
    private const int HORIZONTAL_SPACING = 200;
    private const int X_OFFSET = -300;

    private int _lastDistanceUnit;

    private readonly List<CardType> _testCardPool = new List<CardType>
    {
        new CardType{Suit = Suit.Hearts, Rank = Rank.King},
        new CardType{Suit = Suit.Hearts, Rank = Rank.Two},
        new CardType{Suit = Suit.Spades, Rank = Rank.Jack},
        new CardType{Suit = Suit.Spades, Rank = Rank.Ace},
        new CardType{Suit = Suit.Diamonds, Rank = Rank.Nine},
        new CardType{Suit = Suit.Diamonds, Rank = Rank.Queen},
        new CardType{Suit = Suit.Clubs, Rank = Rank.Queen},
        new CardType{Suit = Suit.Clubs, Rank = Rank.Four}
    };


    private void Awake()
    {
        _cardImageLocator = GetComponent<CardImageLocator>();
        _cardUIPool = CreateCardUIPool(CARD_UI_POOL_SIZE);
    }

    void Start()
    {
        SetCardPool(_testCardPool);
    }

    private void Update()
    {
        UpdateCards();
    }


    private void UpdateCards()
    {
        var roundedPos = Mathf.FloorToInt(_cardUIParent.anchoredPosition.x);
        var totalDistanceUnits = roundedPos / HORIZONTAL_SPACING;
        if(_lastDistanceUnit == totalDistanceUnits)
        {
            return;
        }
        _lastDistanceUnit = totalDistanceUnits;
        var individualUIOffset = MathUtilities.Modulo(roundedPos / HORIZONTAL_SPACING, _cardUIPool.Count);
        var fullPoolDistanceUnits = roundedPos / (_cardUIPool.Count * HORIZONTAL_SPACING);
        for (int i = 0; i < _cardUIPool.Count; i++)
        {
            var cardUI = _cardUIPool[i];
            var offsetIndex = i - 1 - (_cardUIPool.Count * fullPoolDistanceUnits);
            if(_cardUIPool.Count - i <= individualUIOffset 
                && _cardUIParent.anchoredPosition.x > 0)
            {
                offsetIndex -= _cardUIPool.Count;
            }
            if(i < _cardUIPool.Count - individualUIOffset 
                && _cardUIParent.anchoredPosition.x <= -HORIZONTAL_SPACING 
                && individualUIOffset > 0) // probably a cleaner condition possible here but needed to address an edge case
            {
                offsetIndex += _cardUIPool.Count;
            }
            cardUI.SetPos(GetBaseUIPos(offsetIndex), offsetIndex);
            var cardIndex = MathUtilities.Modulo(offsetIndex, _cardPool.Count);
            SetCardUISprite(_cardPool[cardIndex], cardUI);
        }
    }

    private void SetCardPool(List<CardType> cardPool)
    {
        _cardPool = cardPool
            .OrderBy(cardType => (int)cardType.Suit)
            .ThenBy(cardType => (int)cardType.Rank).ToList();
        ResetUI();
    }

    private void ResetUI()
    {
        _cardUIParent.anchoredPosition = Vector2.zero;
        _cardUIPool.AddRange(_activeCardUI);
        _activeCardUI.Clear();
        for (int i = 0; i < _cardUIPool.Count; i++)
        {
            var cardUI = _cardUIPool[i];
            if(i > _cardPool.Count - 1)
            {
                cardUI.gameObject.SetActive(false);
                continue;
            }
            cardUI.gameObject.SetActive(true);
            SetCardUISprite(_cardPool[i], cardUI);
            cardUI.SetPos(GetBaseUIPos(i), i);
        }
    }

    private Vector2 GetBaseUIPos(int index)
    {
        return new Vector2(X_OFFSET + index * HORIZONTAL_SPACING, 0);
    }

    private List<CardUI> CreateCardUIPool(int quantity)
    {
        var pool = new List<CardUI>();
        for (int i = 0; i < quantity; i++)
        {
            var cardUI = Instantiate(_cardUIPrefab, _cardUIParent);
            cardUI.gameObject.SetActive(false);
            pool.Add(cardUI);
        }
        return pool;
    }

    private void SetCardUISprite(CardType cardType, CardUI cardUI)
    {
        var sprite = _cardImageLocator.GetCardTypeSprite(cardType);
        cardUI.SetSprite(sprite);
    }

}
