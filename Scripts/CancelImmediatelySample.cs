using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniTaskCancelImmediately
{
    public class CancelImmediatelySample : MonoBehaviour
    {
        private readonly CancellationTokenSource cts = new();
        private async UniTaskVoid Start()
        {
            Debug.Log($"DelayAfterFrame:{Time.frameCount}");
            try
            {
                // cancelImmediatelyオプションを設定する
                await UniTask.Delay(1000, cancellationToken: cts.Token, cancelImmediately: true);
            } catch(OperationCanceledException)
            {
                Debug.Log($"CanceledFrame:{Time.frameCount}");
            }
        }

        private void LateUpdate()
        {
            cts.Cancel();
        }
    }
}
