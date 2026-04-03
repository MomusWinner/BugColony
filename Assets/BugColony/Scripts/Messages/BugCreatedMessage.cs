using BugColony.Scripts.Bugs;

namespace BugColony.Scripts.Messages
{
    public class BugCreatedMessage
    {
        public Bug Bug { get; private set; }

        public BugCreatedMessage(Bug bug)
        {
            Bug = bug;
        }
    }
}