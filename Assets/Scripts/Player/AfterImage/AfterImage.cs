using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer playerSR;
    private Transform player;
    [SerializeField] private SpriteRenderer SR;

    private float alpha;
    private float enableTime;
    [SerializeField]private float enableTimeDuration = 0.5f;

    private Color color;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        playerSR = player.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SR.sprite = playerSR.sprite;
        transform.localScale = player.localScale;
        alpha = 0.50f;
        color = playerSR.color;
        
        enableTime = Time.time;
    }

   
    private void Update()
    {
        alpha -= Time.deltaTime;
        color.a = alpha;

        SR.color = color;

        if (Time.time >= enableTime + enableTimeDuration)
        {
            gameObject.SetActive(false);
            AfterImagePool.AfterImagePoolInstance.AddToPool(this.gameObject);
        }
    }

    
}
