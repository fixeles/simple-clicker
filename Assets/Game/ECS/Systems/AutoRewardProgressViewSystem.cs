using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono;

namespace ECS
{
    public class AutoRewardProgressViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsFilterInject<Inc<ProgressComponent>> _filter;


        public void Init(IEcsSystems systems)
        {
            _sceneData.Value.AutoRewardProgressView.gameObject.SetActive(true);
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var progressComponent = ref _filter.Pools.Inc1.Get(entity);
                _sceneData.Value.AutoRewardProgressView.fillAmount = progressComponent.TimeLeft / progressComponent.LoopTime;
            }
        }
    }
}