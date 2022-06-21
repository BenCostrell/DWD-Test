using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardImageLocator : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas _cardSprites;

    private static readonly Dictionary<Suit, string> _suitSpriteStrings = new Dictionary<Suit, string>
    {
        {Suit.Clubs, "clubs" },
        {Suit.Diamonds, "diamonds" },
        {Suit.Hearts, "hearts" },
        {Suit.Spades, "spades" }
    };

    private static readonly Dictionary<Rank, string> _rankSpriteStrings = new Dictionary<Rank, string>
    {
        {Rank.Ace, "A" },
        {Rank.Two, "02" },
        {Rank.Three, "03" },
        {Rank.Four, "04" },
        {Rank.Five, "05" },
        {Rank.Six, "06" },
        {Rank.Seven, "07" },
        {Rank.Eight, "08" },
        {Rank.Nine, "09" },
        {Rank.Ten, "10" },
        {Rank.Jack, "J" },
        {Rank.Queen, "Q" },
        {Rank.King, "K" }
    };

    private const string SPRITE_STRING_PREFIX = "card";
    private const string SPRITE_STRING_SEPARATOR = "_";

    private string GetCardTypeSpriteString(CardType cardType)
    {
        var suitString = _suitSpriteStrings[cardType.Suit];
        var rankString = _rankSpriteStrings[cardType.Rank];
        return SPRITE_STRING_PREFIX + SPRITE_STRING_SEPARATOR + suitString + SPRITE_STRING_SEPARATOR + rankString;
    }

    public Sprite GetCardTypeSprite(CardType cardType)
    {
        var spriteString = GetCardTypeSpriteString(cardType);
        return _cardSprites.GetSprite(spriteString);
    }
}
