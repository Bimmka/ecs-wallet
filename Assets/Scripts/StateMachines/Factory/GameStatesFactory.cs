using StateMachines.ApplicationGameStates.States.Interfaces;
using Zenject;

namespace StateMachines.Factory
{
    public class GameStatesFactory
    {
        private readonly DiContainer _container;

        public GameStatesFactory(DiContainer container)
        {
            _container = container;
        }

        public TState Create<TState>() where TState : IExitableState =>
            _container.Resolve<TState>();
    }
}