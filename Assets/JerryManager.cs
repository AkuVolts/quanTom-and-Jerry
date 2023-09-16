using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class JerryManager : MonoBehaviour
{
	public float coolDownTime;
	private bool _inToggleCooldown;
	private int _cacheJerryCertaintyChange;

	public Transform topLeftAnchor;
	public Transform bottomRightAnchor;

	private float _screenWidth;
	private float _screenHeight;
	private float _leftXExtent;
	private float _rightXExtent;
	private float _topYExtent;
	private float _bottomYExtent;

	private Vector2 _widthHeightVector;
	

	[SerializeField] private GameObject trueJerry;

	class Jerry
	{
		public Transform transform;
		public Vector2 velocity = new Vector2(0, 1);
		public float speed = 5;
		public Vector2 pastPosition = new Vector2(0,0);
	}
	// 10 is max level of certainty where jerry is running amok
	private int levelOfCertainty = 1;

	// we ensure that _jerries[0] is true jerry
	private List<Jerry> _jerries = new List<Jerry>();
	
	
    // Start is called before the first frame update
    void Start()
    {
	    _jerries.Add(new Jerry());
	    _jerries[0].transform = trueJerry.transform;
	    
	    _leftXExtent = topLeftAnchor.position.x;
	    _rightXExtent = bottomRightAnchor.position.x;
	    _topYExtent = topLeftAnchor.position.y;
	    _bottomYExtent = bottomRightAnchor.position.y;
	    
	    _screenWidth = _rightXExtent - _leftXExtent;
	    _screenHeight = _topYExtent - _bottomYExtent;
	    _widthHeightVector = new Vector2(_screenWidth, _screenHeight);
    }

    private void UpdateJerryPositions()
    {
	    foreach (var jerry in _jerries)
	    {
		    jerry.pastPosition = jerry.transform.position;
		    float randomRotate = Random.Range(-20f, 20f);
		    float randomSpeedChange = Random.Range(-0.2f, 0.2f);
		    jerry.velocity = RotateVector(jerry.velocity, randomRotate).normalized;
		    jerry.speed = Mathf.Clamp(jerry.speed + randomSpeedChange, 2f, 8f);
		    var addSpeed = jerry.velocity * jerry.speed * Time.fixedDeltaTime;
		    var newPos = new Vector2(
			    jerry.transform.position.x,
			    jerry.transform.position.y) + addSpeed;
		    
		    Vector2 normalizedPos = newPos - new Vector2(_leftXExtent, _bottomYExtent);
		    
		    normalizedPos.x = Mathf.Repeat(normalizedPos.x, _screenWidth);
		    normalizedPos.y = Mathf.Repeat(normalizedPos.y, _screenHeight);
		    
		    newPos = normalizedPos + new Vector2(_leftXExtent, _bottomYExtent);
		    
		    jerry.transform.position = new Vector3(newPos.x, newPos.y, 0f);
	    }
    }
    
    public Vector2 RotateVector(Vector2 v, float angle)
    {
	    float radian = angle*Mathf.Deg2Rad;
	    float _x = v.x*Mathf.Cos(radian) - v.y*Mathf.Sin(radian);
	    float _y = v.x*Mathf.Sin(radian) + v.y*Mathf.Cos(radian);
	    return new Vector2(_x,_y);
    }

    private void DuplicateJerries()
    {
	    
    }

    private void SpawnJerry(Vector2 targetPos)
    {
	    
    }

    private void DecimateJerries()
    {
	    
    }

    private IEnumerator CoolDownCoro()
    {
	    _inToggleCooldown = true;
	    yield return new WaitForSeconds(coolDownTime);
	    _inToggleCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
	    if (!_inToggleCooldown)
	    {
		    _cacheJerryCertaintyChange = 0;
		    if (Input.GetKeyDown(KeyCode.LeftArrow))
		    {
			    _cacheJerryCertaintyChange += 1;
		    }
		    if (Input.GetKeyDown(KeyCode.RightArrow))
		    {
			    _cacheJerryCertaintyChange -= 1;
		    }
		    if (_cacheJerryCertaintyChange != 0)
		    {
			    StartCoroutine(CoolDownCoro());
		    }
	    }
    }

	void FixedUpdate() {
		/*
		 * Every frame I want to
		 * check if the level of certainty has changed
		 * if true: generate/despawn jerry
		 * move jerry according to level of certainty
		 * 
		 */
		if (_cacheJerryCertaintyChange != 0)
		{
			_cacheJerryCertaintyChange = 0;
			
		}

		UpdateJerryPositions();

	}
	
}
