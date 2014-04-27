using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    public Texture2D EggTexture = null;
    public Texture2D SugarTexture = null;
    public Texture2D FlourTexture = null;
    public Texture2D ButterTexture = null;

    public BowlTrigger Bowl = null;

    public bool HasWon { get; set; }

    private float m_imageSize;
    private float m_currentY = 0.0f;

    private float m_startTime = 0.0f;
    private float m_endTime = 0.0f;

	// Use this for initialization
	void Start () {
        m_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetButton("Restart"))
        {
            Application.LoadLevel("Sandbox");
        }

	}

    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {
        m_imageSize = Screen.width / 16.0f;
        m_currentY = 0.0f;

        GUI.skin.label.fontSize = (int)(m_imageSize * 0.5f);

        GUI.skin.label.alignment = TextAnchor.UpperCenter;

        DrawIngredient(this.FlourTexture, (int)Bowl.m_noofFlour, (int)Bowl.RequiredFlour, "g");
        DrawIngredient(this.EggTexture, Bowl.m_noofEggs, Bowl.RequiredEggs, "");
        DrawIngredient(this.ButterTexture, Bowl.m_noofButter, Bowl.RequiredButter, "");
        DrawIngredient(this.SugarTexture, (int)Bowl.m_noofSugar, (int)Bowl.RequiredSugar, "g");

        GUI.skin.label.alignment = TextAnchor.MiddleCenter;

        if (HasWon)
        {
            int ingredientAccuracy = (int)Mathf.Abs((Bowl.RequiredFlour - Bowl.m_noofFlour) + 
                (Bowl.RequiredEggs - Bowl.m_noofEggs) + 
                (Bowl.RequiredButter - Bowl.m_noofButter) +
                (Bowl.RequiredSugar - Bowl.m_noofSugar));

            Rect titleRect = new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.25f);
            
            if (ingredientAccuracy <= 5)
            {
                GUI.Label(titleRect, "MASTER BAKER SUPREME 1337");
            }
            else if (ingredientAccuracy <= 50)
            {
                GUI.Label(titleRect, "That should be edible?");
            }
            else if (ingredientAccuracy <= 100)
            {
                GUI.Label(titleRect, "I'd give this one to the neighbors");
            }
            else
            {
                GUI.Label(titleRect, "Pretty sure the starving wouldn't even eat that...");
            }

            if (m_endTime == 0.0f)
            {
                m_endTime = Time.time;
            }

            int totalTime = 600 - (int)(m_endTime - m_startTime);
            totalTime = totalTime < 1 ? 1 : totalTime;

            int ingredientScalar = 200 - ingredientAccuracy;
            ingredientScalar = ingredientScalar < 1 ? 1 : ingredientScalar;

            int score = (int)(ingredientScalar * totalTime);

            titleRect.y += Screen.height * 0.25f;

            GUI.Label(titleRect, "Score " + score.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <param name="current"></param>
    /// <param name="required"></param>
    private void DrawIngredient(Texture2D image, int current, int required, string unit)
    {
        GUI.DrawTexture(new Rect(m_currentY, 0, m_imageSize, m_imageSize), image);
        GUI.Label(new Rect(m_currentY + m_imageSize, (m_imageSize * 0.25f), (Screen.width * 0.25f) - m_imageSize, m_imageSize), current + unit + "/" + required + unit);

        m_currentY += Screen.width * 0.25f;
    }
}
