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
                await UniTask.Delay(1000, cancellationToken: cts.Token, cancelImmediately: true);
            } catch(OperationCanceledException e)
            {
                Debug.Log($"CanceledFrame:{Time.frameCount}");
            }
        }

        private void LateUpdate()
        {
            Debug.Log($"LateUpdateFrame:{Time.frameCount}");
            cts.Cancel();
        }
    }
}
