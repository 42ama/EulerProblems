using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;
using System.Text;
using System.IO;
using System.Reflection;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class FiftyFour
    {
        const string content =
            "В карточной игре покер ставка состоит из пяти карт и оценивается от самой младшей до самой старшей в следующем порядке:" +
            "\nСтаршая карта: Карта наибольшего достоинства." +
            "\nОдна пара: Две карты одного достоинства." +
            "\nДве пары: Две различные пары карт." +
            "\nТройка: Три карты одного достоинства." +
            "\nСтрейт: Все пять карт по порядку, любые масти." +
            "\nФлаш: Все пять карт одной масти." +
            "\nФул-хаус: Три карты одного достоинства и одна пара карт." +
            "\nКаре: Четыре карты одного достоинства." +
            "\nСтрейт-флаш: Любые пять карт одной масти по порядку." +
            "\nРоял-флаш: Десятка, валет, дама, король и туз одной масти." +
            "\nДостоинство карт оценивается по порядку:" +
            "\n2, 3, 4, 5, 6, 7, 8, 9, 10, валет, дама, король, туз." +
            "\nЕсли у двух игроков получились ставки одного порядка, то выигрывает тот, у кого карты старше: к примеру, две восьмерки выигрывают две пятерки. Если же достоинства карт у игроков одинаковы, к примеру, у обоих игроков пара дам, то сравнивают карту наивысшего достоинства; если же и эти карты одинаковы, сравнивают следующие две и т.д." +
            "\nФайл p054_poker.txt содержит одну тысячу различных ставок для игры двух игроков. В каждой строке файла приведены десять карт (отделенные одним пробелом): первые пять - карты 1-го игрока, оставшиеся пять - карты 2-го игрока.Можете считать, что все ставки верны (нет неверных символов или повторов карт), ставки каждого игрока не следуют в определенном порядке, и что при каждой ставке есть безусловный победитель." +
            "\nСколько ставок выиграл 1-й игрок?" +
            "\nПримечание: карты в текстовом файле обозначены в соответствии с английскими наименованиями достоинств и мастей: T - десятка, J - валет, Q - дама, K - король, A - туз; S - пики, C - трефы, H - червы, D - бубны.";

        static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Model\Resources\p054_poker.txt");

        /// <summary>
        /// Парсит файл p054_poker.txt по заданному пути в массив кортежей двух массивов строк, где
        /// первый массив - рука первого игрока, второй - рука второго игрока
        /// </summary>
        /// <param name="pathToFile">Путь к файлу p054_poker.txt</param>
        /// <returns>Массив кортежей двух массивов строк, где первый массив - рука первого игрока, второй - рука второго игрока</returns>
        private ValueTuple<string[], string[]>[] Parse(string pathToFile)
        {
            var notParsedText = File.ReadAllText(pathToFile);
            var s = notParsedText.Split('\n');
            var games = new ValueTuple<string[], string[]>[1000];
            for (int i = 0; i < s.Length; i++)
            {
                var bothHands = s[i].Split(' ');
                games[i].Item1 = bothHands[0..5];
                games[i].Item2 = bothHands[5..10];
            }
            return games;
        }

        private int Calculate()
        {
            var games = Parse(path);
            int wins = 0;
            foreach (var game in games)
            {
                var playerOne = new PokerHand(game.Item1);
                var playerTwo = new PokerHand(game.Item2);
                if (playerOne.CompareTo(playerTwo) > 0)
                {
                    wins++;
                }
            }
            return wins;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 54,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько ставок выиграл 1-й игрок?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }

    /// <summary>
    /// Масти карт
    /// </summary>
    enum Suit
    {
        Diamonds,
        Hearts,
        Spades,
        Clubs
    }

    /// <summary>
    /// Покерная карта, имеет масть и значение. Реализует IComparable<PokerCard>
    /// </summary>
    class PokerCard : IComparable<PokerCard>
    {
        public int Value { get; protected set; }
        public Suit Suit { get; protected set; }

        public PokerCard(char value, char suit)
        {
            if (Char.IsDigit(value))
            {
                Value = (int)Char.GetNumericValue(value);
            }
            else
            {
                Value = value switch
                {
                    'T' => 10,
                    'J' => 11,
                    'Q' => 12,
                    'K' => 13,
                    'A' => 14
                };
            }

            Suit = suit switch
            {
                'S' => Suit.Spades,
                'C' => Suit.Clubs,
                'H' => Suit.Hearts,
                'D' => Suit.Diamonds
            };
        }


        public PokerCard(int value, Suit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override bool Equals(object obj)
        {
            var otherCard = obj as PokerCard;
            if (Value == otherCard?.Value && Suit == otherCard?.Suit)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сравнивает два объекта <c>PokerCard</c> по значению карты - <c>Value</c>
        /// </summary>
        /// <param name="otherCard">Карта с которой происходит сравнение</param>
        /// <returns>1, если эта карта больше сравниваемой. 0, если они имеют одинаковое значение. 
        /// -1, если сравниваемая карта больше этой</returns>
        public int CompareTo(PokerCard otherCard)
        {
            return Value.CompareTo(otherCard.Value);
        }
    }

    /// <summary>
    /// Покерная рука - комбинация карт, хранит всю информацию о картах и комбинации конкретной руки.
    /// Реализует IComparable<PokerHand>
    /// </summary>
    class PokerHand : IComparable<PokerHand>
    {
        /// <summary>
        /// Карты хранящиеся в руке
        /// </summary>
        public ISet<PokerCard> Cards { get; protected set; }

        /// <summary>
        /// Сводная информация о достоинстве карт
        /// </summary>
        public IDictionary<int, int> ValueInformation { get; protected set; }

        /// <summary>
        /// Сводная информация о масти карт
        /// </summary>
        public IDictionary<Suit, int> SuitInformation { get; protected set; }

        /// <summary>
        /// Старшая комбинация хранящаяся в руке
        /// </summary>
        public Combinations.Combination Combination { get; protected set; }
        /// <summary>
        /// Карты составляющие старшую комбинацию
        /// </summary>
        public ISet<PokerCard> CombinationCards { get; protected set; }
        /// <summary>
        /// Карты не участвующие в составлении старшей комбинации
        /// </summary>
        public ISet<PokerCard> RestOfCards { get; protected set; }

        public PokerHand(string[] hand)
        {
            // сохраняем карты в "руку"
            Cards = new HashSet<PokerCard>();
            for (int i = 0; i < hand.Length; i++)
            {
                Cards.Add(new PokerCard(hand[i][0], hand[i][1]));
            }

            // сохраняем информацию о достоинстве карт
            ValueInformation = new Dictionary<int, int>();
            foreach (var card in Cards)
            {
                ValueInformation[card.Value] = Cards.Count(i => i.Value == card.Value);
            }

            // сохраняем информацию о масти карт
            SuitInformation = new Dictionary<Suit, int>();
            foreach (var card in Cards)
            {
                SuitInformation[card.Suit] = Cards.Count(i => i.Suit == card.Suit);
            }

            // сохраняем информацию о старшей комбинации
            var combinationInfo = Combinations.CalculateCombination(this);
            Combination = combinationInfo.combType;
            CombinationCards = combinationInfo.combination;
            RestOfCards = combinationInfo.restOfCards;
        }

        public PokerHand(ISet<PokerCard> cards)
        {
            // сохраняем карты в "руку"
            Cards = cards;

            // сохраняем информацию о достоинстве карт
            ValueInformation = new Dictionary<int, int>();
            foreach (var card in cards)
            {
                ValueInformation[card.Value] = cards.Count(i => i.Value == card.Value);
            }

            // сохраняем информацию о масти карт
            SuitInformation = new Dictionary<Suit, int>();
            foreach (var card in cards)
            {
                SuitInformation[card.Suit] = cards.Count(i => i.Suit == card.Suit);
            }

            // сохраняем информацию о старшей комбинации
            var combinationInfo = Combinations.CalculateCombination(this);
            Combination = combinationInfo.combType;
            CombinationCards = combinationInfo.combination;
            RestOfCards = combinationInfo.restOfCards;
        }

        /// <summary>
        /// Сравнивает экземпляры <c>PokerHand</c> по старшей комбинации. Если комбинации одинаковы
        /// считается значение каждой комбинации, если и значения одинаковые считается следующая комбинация
        /// в оставшихся картах
        /// </summary>
        /// <param name="otherHand">Экземпляр с которым происходит сравнение</param>
        /// <returns>1, если эта рука больше сравниваемой. 0, если они имеют одинаковое значение. 
        /// -1, если сравниваемая рука больше этой</returns>
        public int CompareTo(PokerHand otherHand)
        {
            if (Combination > otherHand.Combination)
            {
                return 1;
            }
            else if (Combination == otherHand.Combination)
            {
                int compareSameCombination = CompareSameCombination(otherHand);

                if (compareSameCombination == 0)
                {
                    var ourRestOfCardsHand = new PokerHand(RestOfCards);
                    var otherRestOfCardsHand = new PokerHand(otherHand.RestOfCards);

                    return ourRestOfCardsHand.CompareTo(otherRestOfCardsHand);
                }
                return compareSameCombination;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Сравнивает две руки с одинаковыми комбинациями. Сравнение происходит по
        /// самому большому достоинству
        /// </summary>
        /// <param name="otherHand">Рука с которой происходит сравнение</param>
        /// <returns>1, если эта рука больше сравниваемой. 0, если они имеют одинаковое значение. 
        /// -1, если сравниваемая рука больше этой</returns>
        private int CompareSameCombination(PokerHand otherHand)
        {
            int valueMax = CombinationCards.Max(i => i.Value);
            int otherHandMax = otherHand.CombinationCards.Max(i => i.Value);

            return valueMax.CompareTo(otherHandMax);
        }
    }

    /// <summary>
    /// Хранит информацию о покерных комбинациях и механизм для рассчета комбинации для конкретной покерной руки
    /// </summary>
    static class Combinations
    {
        /// <summary>
        /// Информация о всех возможных комбинациях, в порядке возрастания.
        /// Так что при приведении к Int и сравнении, информация о сравнении является правильной.
        /// </summary>
        public enum Combination
        {
            HighCard,
            OnePair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public static (Combination combType, ISet<PokerCard> combination, ISet<PokerCard> restOfCards) CalculateCombination(PokerHand hand)
        {
            ISet<PokerCard> combination;
            ISet<PokerCard> restOfCards;

            if (hand.Cards.Count == 0)
            {
                throw new ArgumentException("Множество cards должно содержать хотя бы один элемент");
            }

            // провряем все комбинации, от старшей к младшей
            if (IsRoyalFlush(hand, out combination, out restOfCards))
            {
                return (Combination.RoyalFlush, combination, restOfCards);
            }
            else if (IsStraightFlush(hand, out combination, out restOfCards))
            {
                return (Combination.StraightFlush, combination, restOfCards);
            }
            else if (IsFourOfAKind(hand, out combination, out restOfCards))
            {
                return (Combination.FourOfAKind, combination, restOfCards);
            }
            else if (IsFullHouse(hand, out combination, out restOfCards))
            {
                return (Combination.FullHouse, combination, restOfCards);
            }
            else if (IsFlush(hand, out combination, out restOfCards))
            {
                return (Combination.Flush, combination, restOfCards);
            }
            else if (IsStraight(hand, out combination, out restOfCards))
            {
                return (Combination.Straight, combination, restOfCards);
            }
            else if (IsThreeOfAKind(hand, out combination, out restOfCards))
            {
                return (Combination.ThreeOfAKind, combination, restOfCards);
            }
            else if (IsTwoPairs(hand, out combination, out restOfCards))
            {
                return (Combination.TwoPairs, combination, restOfCards);
            }
            else if (IsOnePair(hand, out combination, out restOfCards))
            {
                return (Combination.OnePair, combination, restOfCards);
            }
            else
            {
                GetHighCard(hand, out combination, out restOfCards);
                return (Combination.HighCard, combination, restOfCards);
            }
        }


        private static bool IsRoyalFlush(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Роял-флаш: Десятка, валет, дама, король и туз одной масти.
            if (hand.Cards.Count >= 5 && hand.SuitInformation.Count == 1)
            {
                int[] values = { 10, 11, 12, 13, 14 };
                foreach (var value in values)
                {
                    if (hand.Cards.FirstOrDefault(i => i.Value == value) == null)
                    {
                        combination = null;
                        restOfCards = null;
                        return false;
                    }
                }
                combination = hand.Cards;
                restOfCards = new HashSet<PokerCard>();
                return true;
            }
            combination = null;
            restOfCards = null;
            return false;
            
        }

        private static bool IsStraightFlush(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Стрейт-флаш: Любые пять карт одной масти по порядку.
            if (hand.Cards.Count >= 5)
            {
                if (IsFlush(hand, out _, out _) && IsStraight(hand, out _, out _))
                {
                    combination = hand.Cards;
                    restOfCards = new HashSet<PokerCard>();
                    return true;
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsFourOfAKind(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Каре: Четыре карты одного достоинства.
            if (hand.Cards.Count >= 4)
            {
                var matching = hand.ValueInformation.Where(kvp => kvp.Value == 4);
                if (matching.Count() >= 1)
                {
                    combination = new HashSet<PokerCard>();
                    restOfCards = new HashSet<PokerCard>();
                    foreach (var card in hand.Cards)
                    {
                        if (card.Value == matching.First().Key)
                        {
                            combination.Add(card);
                        }
                        else
                        {
                            restOfCards.Add(card);
                        }
                    }
                    return true;
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsFullHouse(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Фул-хаус: Три карты одного достоинства и одна пара карт.
            if (hand.Cards.Count == 5)
            {
                if (IsThreeOfAKind(hand, out combination, out restOfCards))
                {
                    var restOfCardsHand = new PokerHand(restOfCards);
                    ISet<PokerCard> secondCombination = new HashSet<PokerCard>();
                    if (IsOnePair(restOfCardsHand, out secondCombination, out restOfCards))
                    {
                        combination.UnionWith(secondCombination);
                        return true;
                    }
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsFlush(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Флаш: Все пять карт одной масти.
            if (hand.Cards.Count == 5)
            {
                if (hand.SuitInformation.Count == 1)
                {
                    combination = hand.Cards;
                    restOfCards = new HashSet<PokerCard>();
                    return true;
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsStraight(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Стрейт: Все пять карт по порядку, любые масти.
            if (hand.Cards.Count >= 5)
            {
                var minValue = hand.Cards.Min(i => i.Value);
                for (int value = minValue; value < minValue + 5; value++)
                {
                    if (hand.Cards.FirstOrDefault(i => i.Value == value) == null)
                    {
                        combination = null;
                        restOfCards = null;
                        return false;
                    }
                }
                combination = hand.Cards;
                restOfCards = new HashSet<PokerCard>();
                return true;
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsThreeOfAKind(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Тройка: Три карты одного достоинства.
            if (hand.Cards.Count >= 3)
            {
                var matching = hand.ValueInformation.Where(kvp => kvp.Value == 3);
                if (matching.Count() >= 1)
                {
                    combination = new HashSet<PokerCard>();
                    restOfCards = new HashSet<PokerCard>();
                    foreach (var card in hand.Cards)
                    {
                        if (card.Value == matching.Single().Key)
                        {
                            combination.Add(card);
                        }
                        else
                        {
                            restOfCards.Add(card);
                        }
                    }
                    return true;
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsTwoPairs(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Две пары: Две различные пары карт.
            if (hand.Cards.Count >= 4)
            {
                if (IsOnePair(hand, out combination, out restOfCards))
                {
                    var restOfCardsHand = new PokerHand(restOfCards);
                    ISet<PokerCard> secondCombination = new HashSet<PokerCard>();
                    if (IsOnePair(restOfCardsHand, out secondCombination, out restOfCards))
                    {
                        combination.UnionWith(secondCombination);
                        return true;
                    }
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static bool IsOnePair(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Одна пара: Две карты одного достоинства.
            if (hand.Cards.Count >= 2)
            {
                var matching = hand.ValueInformation.Where(kvp => kvp.Value == 2);
                if (matching.Count() >= 1)
                {
                    combination = new HashSet<PokerCard>();
                    restOfCards = new HashSet<PokerCard>();
                    foreach (var card in hand.Cards)
                    {
                        if (card.Value == matching.First().Key)
                        {
                            combination.Add(card);
                        }
                        else
                        {
                            restOfCards.Add(card);
                        }
                    }
                    return true;
                }
            }
            combination = null;
            restOfCards = null;
            return false;
        }

        private static void GetHighCard(PokerHand hand, out ISet<PokerCard> combination, out ISet<PokerCard> restOfCards)
        {
            // Старшая карта: Карта наибольшего достоинства.
            if (hand.Cards.Count >= 1)
            {
                int maxValue = hand.Cards.Max(i => i.Value);
                combination = new HashSet<PokerCard>();
                restOfCards = new HashSet<PokerCard>();
                foreach (var card in hand.Cards)
                {
                    if (card.Value == maxValue)
                    {
                        combination.Add(card);
                    }
                    else
                    {
                        restOfCards.Add(card);
                    }
                }
            }
            else
            {
                combination = null;
                restOfCards = null;
            }
        }
    }

}

