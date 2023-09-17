using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class JerryManager : MonoBehaviour
{
	public float coolDownTime;
	private bool _inToggleCooldown = false;
	private int _cacheJerryCountLevelDelta;

	private int _jerryCountLevel = 1;
	public int JerryCountLevelMinusOne => _jerryCountLevel - 1;

	private int _maxJerryCountLevel = 7;
	private int _minJerryCountLevel = 1;

	public Transform topLeftAnchor;
	public Transform bottomRightAnchor;

	private float _screenWidth;
	private float _screenHeight;
	private float _leftXExtent;
	private float _rightXExtent;
	private float _topYExtent;
	private float _bottomYExtent;

	[SerializeField] private GameObject jerryPrefab;

	[SerializeField] private GameObject trueJerry;

	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip cycleSound;

	class Jerry
	{
		public Transform Transform;
		public Vector2 Velocity = new Vector2(0, 1);
		public float Speed = 5;
	}
	// 10 is max level of certainty where jerry is running amok
	private int _levelOfCertainty = 1;

	// we ensure that _jerries[0] is true jerry
	private List<Jerry> _jerries = new List<Jerry>();
	
	
    // Start is called before the first frame update
    void Start()
    {
	    _jerries.Add(new Jerry());
	    _jerries[0].Transform = trueJerry.transform;

	    var topLeftPos = topLeftAnchor.position;
	    var topRightPos = bottomRightAnchor.position;
	    
	    _leftXExtent = topLeftPos.x;
	    _rightXExtent = topRightPos.x;
	    _topYExtent = topLeftPos.y;
	    _bottomYExtent = topRightPos.y;
	    
	    _screenWidth = _rightXExtent - _leftXExtent;
	    _screenHeight = _topYExtent - _bottomYExtent;
    }

    private void UpdateJerryPositions()
    {
	    foreach (var jerry in _jerries)
	    {
		    float randomRotate = Random.Range(-6f * (_maxJerryCountLevel - _jerryCountLevel) - 4f, 6f * (_maxJerryCountLevel - _jerryCountLevel) + 4f);
		    float randomSpeedChange = Random.Range(-0.2f * (_maxJerryCountLevel - _jerryCountLevel), 0.2f * (_maxJerryCountLevel - _jerryCountLevel));
		    jerry.Velocity = RotateVector(jerry.Velocity, randomRotate).normalized;
		    jerry.Speed = Mathf.Clamp(jerry.Speed + randomSpeedChange, 1f * (_maxJerryCountLevel - _jerryCountLevel) * 2, _jerryCountLevel * 2 - _jerryCountLevel * 1.6f);
		    var addSpeed = jerry.Velocity * (jerry.Speed * Time.fixedDeltaTime);
		    var position = jerry.Transform.position;
		    var newPos = new Vector2(position.x, position.y) + addSpeed;
		    
		    Vector2 normalizedPos = newPos - new Vector2(_leftXExtent, _bottomYExtent);
		    
		    normalizedPos.x = Mathf.Repeat(normalizedPos.x, _screenWidth);
		    normalizedPos.y = Mathf.Repeat(normalizedPos.y, _screenHeight);
		    
		    newPos = normalizedPos + new Vector2(_leftXExtent, _bottomYExtent);
		    
		    position = new Vector3(newPos.x, newPos.y, 0f);
		    jerry.Transform.position = position;
	    }
    }
    
    public Vector2 RotateVector(Vector2 v, float angle)
    {
	    float radian = angle*Mathf.Deg2Rad;
	    float x = v.x*Mathf.Cos(radian) - v.y*Mathf.Sin(radian);
	    float y = v.x*Mathf.Sin(radian) + v.y*Mathf.Cos(radian);
	    return new Vector2(x,y);
    }

    private void DuplicateJerries()
    {
	    Debug.Log("Duplicating Jerries");
		int jerryCount = _jerries.Count;
	    for (int i = 0; i < jerryCount; i++)
	    {
		    var newJerryObject = Instantiate(jerryPrefab, gameObject.transform);
		    newJerryObject.transform.position = _jerries[i].Transform.position;
		    var newJerry = new Jerry
		    {
			    Speed = _jerries[i].Speed,
			    Velocity = _jerries[i].Velocity,
			    Transform = newJerryObject.transform
		    };
		    _jerries.Add(newJerry);
	    }
    }

    private void DecimateJerries()
    {
	    Debug.Log("Decimating Jerries");
		var rnd = new System.Random();
	    var jerriesToDelete = _jerries
		    .Skip(1)
		    .OrderBy(x => rnd.Next())
		    .Take(_jerries.Count / 2);
	    foreach (var badJerry in jerriesToDelete)
	    {
		    Destroy(badJerry.Transform.gameObject);
		    _jerries.Remove(badJerry);
	    }
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
		    _cacheJerryCountLevelDelta = 0;
		    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.P))
		    {
			    _cacheJerryCountLevelDelta -= 1;
				audioSource.PlayOneShot(cycleSound);
		    }
		    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.O))
		    {
			    _cacheJerryCountLevelDelta += 1;
				audioSource.PlayOneShot(cycleSound);
		    }
		    if (_cacheJerryCountLevelDelta != 0)
		    {
			    StartCoroutine(CoolDownCoro());
		    } 
	    }
    }

	void FixedUpdate() {
		switch (_cacheJerryCountLevelDelta)
		{
			case 1:
				if (_jerryCountLevel < _maxJerryCountLevel)
				{
					DuplicateJerries();
					_cacheJerryCountLevelDelta = 0;
					_jerryCountLevel += 1;
				}
				break;
			case -1:
				if (_jerryCountLevel > _minJerryCountLevel)
				{
					DecimateJerries();
					_cacheJerryCountLevelDelta = 0;
					_jerryCountLevel -= 1;
				}

				break;
		}
		UpdateJerryPositions();
	}
	
}
