using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class BallController : MonoBehaviour
{


    UIManager ui;
    public GameObject tailrender;
    public MeshRenderer Meshrender;
    // animation dead
    public GameObject deadanim_object;

    bool stopmove = false;
    // effectColison

    public GameObject effectcolision;
    // dead anim
    float tempSpeed;

    public static int CurDiem;
    // effe item

    public EffectScore effectItem;

     
    // Use this for initialization
    public bool Ismoveright;
    public Rigidbody2D mybody;
    public float speed;
    //score
    public Text score;
    public Text Highscore;
    public int diem = 0;
    public Text score_gameover;
    UnityArmatureComponent unityarmaturecomonent;
    float curSpeed;
    bool checkrotation;
    Animator myanim;

    //public GameObject[] scoremove1;
    // text mesh
    TextMesh textmesh;
    void Awake()
    {

        
        myanim = GetComponent<Animator>();
        unityarmaturecomonent = GetComponent<UnityArmatureComponent>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        ballgroup.ballGroup.ball.Add(this);
        Highscore.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString(); // lay diem score ban dau
        textmesh = GetComponent<TextMesh>();

        print(CurDiem);
        if (PlayerPrefs.GetInt("watchfullvideo") == 1)
        {
            diem = CurDiem;
        }
        if (PlayerPrefs.GetInt("watchfullvideo") == 0)
        {
            diem = 0;
        }
    }
    void Start()
    {

        score.text = "Score: " + diem.ToString();
        Ismoveright = true;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (!stopmove)
        {
            if (Ismoveright)
            {
                // Ismoveright = false;
                mybody.velocity = new Vector2(speed, speed);
            }
            else
            {
                mybody.velocity = new Vector2(-speed, speed);
            }
        }
        else
        {

            mybody.velocity = new Vector2(0, 0);

        }

        score_gameover.text = "Score: " + diem.ToString();

        if (mybody.velocity.x > 0)

            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan(mybody.velocity.y / mybody.velocity.x) * 180 / Mathf.PI);
        else
            if (mybody.velocity.x == 0)
            {
                mybody.AddForce(new Vector3(0, 0, 0));


            }
            else
                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan(mybody.velocity.y / mybody.velocity.x) * 180 / Mathf.PI + 180);


     
        if (scoremove.sm.isaddscore)
        {
            score.text = "Score : " + diem.ToString();
        }
    }




    private float temp = 0;
    private void Playanim(string nameAnim)
    {
        unityarmaturecomonent.animation.Play(nameAnim);

    }
    private void Stopanim(string nameAnim)
    {
        unityarmaturecomonent.animation.Stop(nameAnim);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "box")
        {

            if (Ismoveright)
                Ismoveright = false;
            else
            {
                Ismoveright = true;
            }
            //


            Instantiate(effectcolision, transform.position, Quaternion.identity);

            //if ((temp = Mathf.Pow(mybody.velocity.x, 2) + Mathf.Pow(mybody.velocity.y, 2)) < 10f)
            //{
            //    mybody.velocity *= Mathf.Sqrt(1f / temp);
            //}
            SoundManager.soundManager.playSound(3);


        }
    }


    IEnumerator quaydau()
    {
        Playanim("2quayphai30");
        yield return new WaitForSeconds(0.25f);
        Stopanim("2quayphai30");

    }
    IEnumerator playanimquaydau()
    {
        yield return new WaitForSeconds(0.25f);
        Playanim("1boi30");
    }
    void setmoveright(bool set)
    {
        Ismoveright = set;
    }
    private Vector2 vt = new Vector2();
    void ZenNew()
    {
        BallController b = Instantiate(ballgroup.ballGroup.ball[ballgroup.ballGroup.ball.Count - 1], vt, transform.rotation);
        b.mybody.velocity = velocity;

    }

    private Vector2 velocity;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "cong1")
        {

            diem = diem + 1;
            addScore();
            Destroy(other.gameObject);
            SoundManager.soundManager.playSound(2);

            EffectScore anim = Instantiate(effectItem, transform.position, effectItem.transform.rotation);
            anim.anim.animation.FadeIn("tim20");
            anim.textmesh.text = "+1";
        }
        else
            if (other.gameObject.tag == "cong2")
            {
                diem = diem + 2; addScore(); Destroy(other.gameObject); SoundManager.soundManager.playSound(2);
                EffectScore anim = Instantiate(effectItem, transform.position, effectItem.transform.rotation);

                anim.anim.animation.FadeIn("tron");
                anim.textmesh.text = "+2";

            }
            else
                if (other.gameObject.tag == "cong3")
                {
                    diem = diem + 3; addScore(); Destroy(other.gameObject); SoundManager.soundManager.playSound(2);
                    EffectScore anim = Instantiate(effectItem, transform.position, effectItem.transform.rotation);
                    anim.anim.animation.FadeIn("xoay");
                    anim.textmesh.text = "+3";

                }
                else

                    if (other.gameObject.tag == "cong4")
                    {
                        diem = diem + 4; addScore(); Destroy(other.gameObject); SoundManager.soundManager.playSound(2);
                        EffectScore anim = Instantiate(effectItem, transform.position, effectItem.transform.rotation);
                        anim.anim.animation.FadeIn("hoaNhen20");
                        anim.textmesh.text = "+4";

                    }
                    else
                        if (other.gameObject.tag == "cong5")
                        {
                            diem = diem + 5; addScore(); Destroy(other.gameObject); SoundManager.soundManager.playSound(2);
                            EffectScore anim = Instantiate(effectItem, transform.position, effectItem.transform.rotation);
                            anim.anim.animation.FadeIn("hoaTron20");
                            anim.textmesh.text = "+5";


                        }
        if (other.gameObject.tag == "box")
        {
            Meshrender.enabled = false;
            deadanim();
            tailrender.SetActive(false);
            CurDiem = diem;
            CameraShake.camshake.DoShake(); SoundManager.soundManager.music.mute = true; Invoke("callSoundplayerdead", 0.1f);

        }

        if (other.gameObject.tag == "enemy")
        {
            tailrender.SetActive(false);
            Meshrender.enabled = false;
            deadanim();
            CurDiem = diem;
            CameraShake.camshake.DoShake(); SoundManager.soundManager.music.mute = true; Invoke("callSoundplayerdead", 0.1f);
        }
        if (other.gameObject.tag == "duongbien")
        {

            deadanim();
            tailrender.SetActive(false);
            Meshrender.enabled = false;
            CurDiem = diem;
            CameraShake.camshake.DoShake(); SoundManager.soundManager.music.mute = true; Invoke("callSoundplayerdead", 0.1f);
        }

    }

    void deadanim()
    {
        Instantiate(deadanim_object, transform.position, Quaternion.identity);
        StartCoroutine(deadtimeDelay());
        stopmove = true;
    }
    IEnumerator deadtimeDelay()
    {
        yield return new WaitForSeconds(1.2f);
        PlayerDead();

    }

    void addScore()
    {
        if (diem > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", diem);
            Highscore.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        }
        speed += 0.15f;

    }



    void PlayerDead()
    {
        
        ShareRateAds.shareRateAds.ShowBaner();
        UIManager.uimanager.GameOver();
        Destroy(gameObject);
        

    }

    void callSoundplayerdead()
    { SoundManager.soundManager.playSound(4); }
}