using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class setImage : MonoBehaviour {


    public Texture m_MainTexture; //m_MainTexture contains the actual image to be shown. Add using the Unity interface
    Renderer m_Renderer; //Render textures to objects

    public AudioClip sound; //works similar to m_MainTexture but for audio
                          
    public bool isSet = false;
    public float alphaValue = 0f;
    public bool checkEmpty;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start () {
    

    }

 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update () {
        //animation for fade in 
        if (isSet && !checkEmpty)
        {
            m_Renderer.material.color = new Color(1.0f, 1.0f, 1.0f, alphaValue);
            alphaValue = alphaValue + 0.02f;
            //stop animation
            if (alphaValue >= 1)
            {
                enabled = false;
                isSet = false;
            }
        }
        if (checkEmpty)
        {
            m_Renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        }
    }

 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    //Discard the name. It sets sll the image
    public void set_cover(string image_data) //get data from data script. If you check the unity interface under event trigger script, you'll need to add in the data path manually in each links
    {
        enabled = true;
        isSet = true;
        alphaValue = 0f;
        //get data from data script
        //load texture. Important to do in 2d. 3d doesnt work
        Texture2D m_MainTexture = Resources.Load<Texture2D>(image_data);

        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>(); //getcomponents gets thee component this script is attached to. Really useful stuff here!
    
        //Make sure to enable the Keywords
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture); //rendering data 
        if (image_data == "Images/Materials/empty")
        {
            checkEmpty = true;
        }
        if (image_data != "Images/Materials/empty")
        {
            checkEmpty = false;
        }
    }

 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Sets Sound
    public void set_sound(string sound_data)
    {
        AudioClip music = Resources.Load<AudioClip>(sound_data); //converts string sound_data to a audioclip gameObject
        var source = GetComponent<AudioSource>(); //gets audiosource component from Map
        if (source.isPlaying) //check to see if theres any sound playing so that sounds dont overlap
        {
            source.Stop();
        }
        source.clip = music; //add sound to clip component
        source.Play();       
    }

 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //The Reset Capusle. Turns Off all the audio source and get rids of all the images
    public void Reset()
    {
        GameObject map, cover;
        //Find Objects
        map = GameObject.Find("Map");
        cover = GameObject.Find("Cover");

        //Change Cover Back to Intro
        Texture2D m_MainTexture = Resources.Load<Texture2D>("Images/intro_music_cover");
        m_Renderer = cover.GetComponent<Renderer>();
        m_Renderer.material.SetTexture("_MainTex", m_MainTexture); //rendering data 

        //Stop Music
        var source = map.GetComponent<AudioSource>(); //gets audiosource component from Map
        m_MainTexture = Resources.Load<Texture2D>("Images/Materials/empty");
        source.Stop();

        //Change all Planes to plain
        GameObject holder;
        for(int i = 1; i<=5; i++)
        {
            holder = GameObject.Find("image" + i);
            m_Renderer = holder.GetComponent<Renderer>(); //getcomponents gets thee component this script is attached to. Really useful stuff here!
            m_Renderer.material.SetTexture("_MainTex", m_MainTexture); //rendering data 
            m_Renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f); //make it all transparent
        }
    }
}
