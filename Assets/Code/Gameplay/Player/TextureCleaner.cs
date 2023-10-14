using UnityEngine;
using UnityEngine.Rendering;
namespace NewTankio.Code.Gameplay.Player
{
    public sealed class TextureCleaner : MonoBehaviour
    {
        public RenderTexture RenderTexture;
        private CommandBuffer _commandBuffer;

        private void Start() 
            => _commandBuffer = new CommandBuffer();

        private void OnDisable()
        {
            _commandBuffer.SetRenderTarget(RenderTexture);
            _commandBuffer.ClearRenderTarget(true, true, Color.clear);
            Graphics.ExecuteCommandBuffer(_commandBuffer);
        }

        private void OnDestroy()
        {
            _commandBuffer.Release();
        }
    }
}
