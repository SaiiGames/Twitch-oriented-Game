using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
	private Camera _cameraInstance;
	private SpriteRenderer _image;
	private Tweener _tweener;
	private LineRenderer _lineRenderer;
	private bool isDrag = false;
	public Sprite okImage, forbidImage,dragImage;
	private void Awake()
	{
		Cursor.visible = false;
		// Cursor.lockState = CursorLockMode.Locked;
		_cameraInstance = Camera.main;
		_image = GetComponent<SpriteRenderer>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		_image.color = new Color(255, 255, 255, 0);
	}

	private void Update()
	{
		Vector2 cursorPos = _cameraInstance.ScreenToWorldPoint(Input.mousePosition);
		transform.position = cursorPos;

		ChangeCursorSprite();
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && CharacterController2D.m_Grounded)
		{
			isDrag = true;			
			_lineRenderer.endColor = new Color(255, 255, 255, 255);
			_lineRenderer.SetPosition(0,transform.position);
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			_lineRenderer.SetPosition(1,transform.position);
			
		}
		else if (!Input.GetKey(KeyCode.Mouse0))
		{
			_lineRenderer.SetPosition(0,Vector2.zero);
			_lineRenderer.SetPosition(1,Vector2.zero);

			isDrag = false;
			DOTween.Kill(_tweener);
			_lineRenderer.endColor = new Color(255, 255, 255, 0);
		}
		
	}

	void ChangeCursorSprite()
	{
		if (isDrag)
		{
			_image.sprite = dragImage;
			_tweener = _image.DOFade(1f, 0.2f);

		}
		else if (!CharacterController2D.m_Grounded)
		{
			_image.color = new Color(255, 255, 255, 0.2f);
			_image.sprite = forbidImage;
		}
		else
		{
			_image.color = new Color(255, 255, 255, 1f);
			_tweener = _image.DOFade(0.2f, 0.2f);
			_image.sprite = okImage;
		}
	}

	
}
