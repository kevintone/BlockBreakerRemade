using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	
	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	
	// Use this for initialization
	void Start () { 
		isBreakable = (this.tag == "Breakable");
		//Keep track of breakable bricks
		if (isBreakable)
		{
			breakableCount++;
		}
		
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		AudioSource.PlayClipAtPoint(crack, transform.position, 0.3f);
		
		if (isBreakable)
		{
			HandleHits(); 
		}
	}
	
	void HandleHits()
	{
		timesHit++;
		int maxHit = hitSprites.Length + 1;
		
		
		if (timesHit >= maxHit)
		{
			breakableCount--;
			levelManager.brickDestroyed();
			GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
			smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
			Destroy(gameObject);
		} else
		{
			
			LoadSprites();
			
		}
	}
	
	void LoadSprites()
	{
		int spriteIndex = timesHit - 1;
		if(hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	//TODO Remove method when we can actually win!!!
	void SimulateWin()
	{
		levelManager.LoadNextLevel();
	}
}
