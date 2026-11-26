
using System;
using System.Collections.Generic;
using System.Text;

public static class Camicia
{
    public enum GameStatus
    {
        Finished,
        Loop
    }

    public record GameResult(GameStatus Status, int Tricks, int Cards);

    public static GameResult SimulateGame(string[] playerA, string[] playerB)
    {
        Queue<string>[] decks =
        {
            new Queue<string>(playerA),
            new Queue<string>(playerB)
        };

        List<string> pile = new List<string>();
        HashSet<string> seenStates = new HashSet<string>();

        int currentPlayer = 0;
        int penalty = 0;
        int tricks = 0;
        int cards = 0;

        int totalCards = playerA.Length + playerB.Length;

        string GetState()
        {
            StringBuilder state = new StringBuilder();

            for (int player = 0; player < 2; player++)
            {
                foreach (string card in decks[player])
                {
                    if (card == "J" || card == "Q" ||
                        card == "K" || card == "A")
                    {
                        state.Append(card);
                    }
                    else
                    {
                        state.Append('N');
                    }
                }

                state.Append('|');
            }

            state.Append(currentPlayer);

            return state.ToString();
        }

        seenStates.Add(GetState());

        while (true)
        {
            if (decks[currentPlayer].Count == 0)
            {
                int winner = 1 - currentPlayer;

                foreach (string card in pile)
                {
                    decks[winner].Enqueue(card);
                }

                pile.Clear();
                tricks++;
                penalty = 0;
                currentPlayer = winner;

                if (decks[winner].Count == totalCards)
                {
                    return new GameResult(
                        GameStatus.Finished, tricks, cards);
                }

                if (!seenStates.Add(GetState()))
                {
                    return new GameResult(
                        GameStatus.Loop, tricks, cards);
                }

                continue;
            }

            string playedCard = decks[currentPlayer].Dequeue();

            pile.Add(playedCard);
            cards++;

            int faceValue = playedCard switch
            {
                "J" => 1,
                "Q" => 2,
                "K" => 3,
                "A" => 4,
                _ => 0
            };

            if (faceValue > 0)
            {
                penalty = faceValue;
                currentPlayer = 1 - currentPlayer;
            }
            else if (penalty > 0)
            {
                penalty--;

                if (penalty == 0)
                {
                    int winner = 1 - currentPlayer;

                    foreach (string card in pile)
                    {
                        decks[winner].Enqueue(card);
                    }

                    pile.Clear();
                    tricks++;
                    currentPlayer = winner;

                    if (decks[winner].Count == totalCards)
                    {
                        return new GameResult(
                            GameStatus.Finished, tricks, cards);
                    }

                    if (!seenStates.Add(GetState()))
                    {
                        return new GameResult(
                            GameStatus.Loop, tricks, cards);
                    }
                }
            }
            else
            {
                currentPlayer = 1 - currentPlayer;
            }
        }
    }
}
