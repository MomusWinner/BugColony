using BugColony.Scripts.Messages;
using BugColony.Scripts.Settings;
using MessagePipe;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.Foods
{
    public class FoodPool : IFoodFactory, IStartable
    {
        [Inject] private IObjectResolver _container;
        [Inject] private IPublisher<FoodCreatedMessage> _publisher;
        [Inject] private GameSettings _settings;

        private const int DefaultCapacity = 10;
        private const int MaxSize = 20;

        private IObjectPool<FoodView> _pool;
        private GameObject _parent;

        public void Start()
        {
            _pool = new ObjectPool<FoodView>(
                createFunc: CreateFood,
                actionOnGet: OnGetFromPool,
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyPooledObject,
                collectionCheck: true,
                defaultCapacity: DefaultCapacity,
                maxSize: MaxSize
            );
            
            _parent = new GameObject("Food Pool");
        }

        public FoodView Create()
        {
            var food = _pool.Get();
            food.transform.SetParent(null);
            food.Initialize(_pool, _settings.SpawnerSettings.Food.NutritionalValue);
            _publisher.Publish(new FoodCreatedMessage(food));
            return food;
        }

        private FoodView CreateFood()
        {
            FoodView food = _container.Instantiate(_settings.SpawnerSettings.Food.Prefab);
            return food;
        }

        private void OnGetFromPool(FoodView food)
        {
            food.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(FoodView food)
        {
            food.gameObject.SetActive(false);
            food.transform.SetParent(_parent.transform);
        }

        private void OnDestroyPooledObject(FoodView food)
        {
            Object.Destroy(food.gameObject);
        }
    }
}