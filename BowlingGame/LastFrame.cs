namespace BowlingGame
{
    public class LastFrame : Frame
    {
        public LastFrame(int rollNumber):base(rollNumber) { }

        public override bool IsComplete()
        {
            if (IsStrike || IsSpare)
            {
                // If you get a strike or spare in last frame the frame must contain 3 rolls to be complete
                return rolls.Count == 3;
            }
            return rolls.Count == 2;
        }

        public override void RegisterPins(int pins)
        {
            if (IsComplete()) throw new Exception("Cannot register pins in completed frame");
            if (!IsStrike && rolls.Count == 1 && rolls.ElementAt(0) + pins > 10) throw new ArgumentException("Number of pins in one frame cannot exceed 10");

            rolls.Add(pins);
        }
    }
}
