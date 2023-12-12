using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniTaskCancelImmediately
{
    public class SequentialTaskSample : MonoBehaviour
    {
        private bool isDuringNanka;
        async UniTask NankaAsync(CancellationToken cancellationToken)
        {
            isDuringNanka = true;

            try
            {
                await UniTask.Delay(10000, cancellationToken: cancellationToken, cancelImmediately: true);

                await UniTask.DelayFrame(10000, cancellationToken: cancellationToken, cancelImmediately: true);

                await UniTask.Yield(cancellationToken, cancelImmediately: true);
            }
            finally
            {
                isDuringNanka = false;
            }
        }

        private CancellationTokenSource cts = new();
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cts.Cancel();
                cts.Dispose();
                cts = new();
                NankaAsync(cts.Token).Forget();
            }

            Debug.Log(isDuringNanka);
        }
    }
}