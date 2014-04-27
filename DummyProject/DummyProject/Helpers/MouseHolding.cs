using DummyProject.Objects;

namespace DummyProject.Helpers
{
    public class MouseHolding
    {
        public HoldableObject HoldableObject { get; set; } 
        
        public void Reset()
        {
            this.HoldableObject = null;
        }
    }
}
