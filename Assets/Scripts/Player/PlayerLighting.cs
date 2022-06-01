using UnityEngine;

public class PlayerLighting : MonoBehaviour {
    Transform player;
#pragma warning disable 108 // Member hides inherited member; missing new keyword
    public Light light;
#pragma warning restore 108 // Member hides inherited member; missing new keyword

    PlayerEmotions playerEmotions;

    void Start ()
    {
        light = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerEmotions = player.GetComponent<PlayerEmotions>();
    }
	
	void Update ()
    {
        if (playerEmotions != null)
            light.range = playerEmotions.currentSmile;
    }
}