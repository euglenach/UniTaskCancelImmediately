using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    
    async UniTask CountdownAsync(float seconds, CancellationToken cancellationToken)
    {
        // カウントダウンの開始時に文字を赤くする
        timeText.color = Color.red;

        try
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                // 時間を更新
                timeText.text = seconds.ToString("N1");
                seconds = Mathf.Max(0, seconds - Time.deltaTime);
                if(seconds <= 0) break;
                
                // cancelImmediately falseで待つ
                await UniTask.NextFrame(cancellationToken, cancelImmediately: true);
            }
        } finally
        {
            // カウントダウンが終わったら文字を白くする
            if(timeText) timeText.color = Color.white;
        }
    }

    [SerializeField] private Button button;
    private CancellationTokenSource timerCancellation;
    
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            timerCancellation?.Cancel();
            timerCancellation = new();
            CountdownAsync(10, timerCancellation.Token).Forget();
        });
    }
}
