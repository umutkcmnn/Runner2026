using UnityEngine;
using DG.Tweening;

public class Collectables : MonoBehaviour
{
    public CollectablesEnum CollectablesEnum;
    public int toBeAddedScore;
    public int toBeAddedHealth;
    public int toBeAddedSpeed;
    public GameObject Player;

    private void Start()
    {
        if(CollectablesEnum == CollectablesEnum.Coin)
        {
            Player = GameObject.FindFirstObjectByType<Playercontroller>().gameObject;
        }
    }

    private void Update()
    {
        if(CollectablesEnum == CollectablesEnum.Coin && Player.GetComponent<Playercontroller>().isMagnetActive)
        {
            if(Vector3.Distance(Player.transform.position, this.transform.position) < 5)
            {
                transform.DOMove(Player.transform.position, 0.5f);
            }
        }
    }
}
