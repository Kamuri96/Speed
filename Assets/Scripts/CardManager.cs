using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<GameObject> enemyCardPrefabs; // 敵のトランプのカードのプレハブを格納するためのリスト
    public List<GameObject> playerCardPrefabs; // 自分のトランプのカードのプレハブを格納するためのリスト
    public Transform[] enemyCardSpawnPoints; // 敵のカードを生成する位置の配列
    public Transform[] playerCardSpawnPoints; // 自分のカードを生成する位置の配列

    void Start()
    {
        // トランプをシャッフル
        List<GameObject> shuffledEnemyCards = ShuffleCards(enemyCardPrefabs);
        List<GameObject> shuffledPlayerCards = ShuffleCards(playerCardPrefabs);

        // 敵と自分の手札に四枚のカードを配布
        List<GameObject> enemyHand = DistributeHand(shuffledEnemyCards, 4);
        List<GameObject> playerHand = DistributeHand(shuffledPlayerCards, 4);

        // 手札を指定された位置に配置する
        ArrangeCards(enemyHand, enemyCardSpawnPoints);
        ArrangeCards(playerHand, playerCardSpawnPoints);
    }

    // カードをシャッフルする関数
    List<GameObject> ShuffleCards(List<GameObject> cards)
    {
        List<GameObject> shuffledCards = new List<GameObject>(cards);
        int n = shuffledCards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject temp = shuffledCards[k];
            shuffledCards[k] = shuffledCards[n];
            shuffledCards[n] = temp;
        }
        return shuffledCards;
    }

    // カードを指定された枚数だけ配布する関数
    List<GameObject> DistributeHand(List<GameObject> deck, int numRequired)
    {
        List<GameObject> hand = new List<GameObject>();
        for (int i = 0; i < numRequired; i++)
        {
            hand.Add(deck[i]);
            deck.RemoveAt(0);
        }
        return hand;
    }

    // 手札を指定された位置に配置する関数
    void ArrangeCards(List<GameObject> hand, Transform[] spawnPoints)
    {
        float cardSpace = 0.1f; // カード同士の間隔

        for (int i = 0; i < hand.Count; i++)
        {
            Vector3 cardPosition = spawnPoints[i].position + new Vector3(i * cardSpace, 0f, 0f);
            Instantiate(hand[i], cardPosition, Quaternion.Euler(270,0,0));
        }
    }
}
