using UnityEngine;
using UnityEngine.UI;
using Affdex;
using System.Collections.Generic;

public class PlayerEmotions : ImageResultsListener
{
    public Slider zenSlider;
    public float currentDisgust;
    public float currentContempt;
    public float currentValence;
    public float currentAnger;
    public float currentSurprise;
    public float currentSmile;
    public float currentMouthOpen;
    public float currentEyeClosure;

    Transform playerIcon;
    Image playerIconImage;

    public override void onFaceFound(float timestamp, int faceId)
    {
        setIcon(255);
        if (Debug.isDebugBuild) Debug.Log("Found the face");
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        setIcon(20);
        if (Debug.isDebugBuild) Debug.Log("Lost the face");
        Transform player;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth.currentHealth > 0)
        {
            //Time.timeScale = 0;
        }
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
            faces[0].Emotions.TryGetValue(Emotions.Disgust, out currentDisgust);
            faces[0].Emotions.TryGetValue(Emotions.Contempt, out currentContempt);
            faces[0].Emotions.TryGetValue(Emotions.Valence, out currentValence);
            faces[0].Emotions.TryGetValue(Emotions.Anger, out currentAnger);
            faces[0].Emotions.TryGetValue(Emotions.Surprise, out currentSurprise);
            faces[0].Expressions.TryGetValue(Expressions.Smile, out currentSmile);
            faces[0].Expressions.TryGetValue(Expressions.MouthOpen, out currentMouthOpen);
            faces[0].Expressions.TryGetValue(Expressions.EyeClosure, out currentEyeClosure);
            if (currentValence >= 0) zenSlider.value = currentValence;
            else zenSlider.value = 0;
        }
    }

    private void setIcon(byte alpha)
    {
        playerIcon = GameObject.FindGameObjectWithTag("PlayerIcon").transform;
        playerIconImage = playerIcon.GetComponent<Image>();
        playerIconImage.color = new Color32(255, 255, 255, alpha);
    }
}