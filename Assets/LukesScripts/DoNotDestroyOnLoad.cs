﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    private void Start() => DontDestroyOnLoad(this);
}
