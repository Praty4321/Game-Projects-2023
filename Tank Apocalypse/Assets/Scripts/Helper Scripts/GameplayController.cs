using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;
    public float min_ObstacleDelay = 10f, max_ObstacleDelay = 14f;
    private float halfGroundSize = 100f;
    private BaseController playerController;

    private Text scoreText;
    private int zombieKillCount;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameoverPanel;
    [SerializeField]
    private Text finalKillScore;


     void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();

        StartCoroutine("GenerateObstacles");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != null)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(min_ObstacleDelay, max_ObstacleDelay) / playerController.speed.z;
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);

        StartCoroutine("GenerateObstacles");


    }

    void CreateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);

        if (r >= 0 && r < 7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            AddObstacle(new Vector3(lanes[obstacleLane].transform.position.x, 0f, 
                zPos), Random.Range(0, obstaclePrefabs.Length));

            int zombieLane = 0;

            if (obstacleLane == 0) {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2 ;
            } else if (obstacleLane == 1){
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2 ;
            } else if (obstacleLane == 2){
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0 ;
            }

            AddZombies(new Vector3(lanes[zombieLane].transform.position.x, 0.15f, zPos)); 
        }
    }
    void AddObstacle(Vector3 position, int type)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch(type)
        {
            case 0: 
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f); 
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }

        obstacle.transform.position = position;

    }

    void AddZombies(Vector3 pos)
    {
        int count = Random.Range(0, 3) + 1;

        for(int i = 0; i < count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i);

            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos + shift * i, Quaternion.identity);
        }
    }

    public void IncreaseScore() // that will increase the score when we will kill a zombie.
    {
        zombieKillCount ++;
        scoreText.text = zombieKillCount.ToString(); //coz killcount is integer and scoretext.text will take a string only.
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;// this will actually pause the game. /* thats why we did 1st if statement in ShootingControl function in playerController. */
        
        pausePanel.SetActive(true); //on calling it will show the pausePanel.
         
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);//deactivating pause panel and
        
    }

    public void ExitGame() // when we exit we need to set the time scale back to 1 so that when we restart we will start the gameplay straight away

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Gameover()
    {
        /* Also we will add a fading animation to out gameover panel as FadeIn and FadeOut
         and for that we will use CANVAS GROUP component on the parent Gameover Panel so that we can animate all children at once.
        So, we will fade the alpha in canvas group component to add the disappearing animation.*/
        
        Time.timeScale = 0f;// Also do GameoverPanel > Inspector > Animator > Update Mode > Unscaled Time coz we set Time.locascale to 0 in GameOverFunction so in normal update mode our animation will not run.
        gameoverPanel.SetActive(true);
        finalKillScore.text = "Killed " + zombieKillCount.ToString();

        
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    

} //class end















































