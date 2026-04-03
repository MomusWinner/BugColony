namespace BugColony.Scripts.Bugs.Behaviours.Eating
{
    public class BugCommonEating : IBugEating
    {
        private readonly BugState _bugState;
        
        public BugCommonEating(BugState bugState)
        {
            _bugState = bugState;
        }
        
        public void EatResource(IEatable resource)
        {
            _bugState.Energy += resource.Eat();
        }
    }
}