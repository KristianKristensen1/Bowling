using System.Collections.ObjectModel;

namespace BowlingGame
{
    public class BowlingGame
    {
        private const int maxNumberOfFrames = 10;
        private List<int> allRolls = new List<int>();
        private int totalScore;
        private List<Frame> frames;
        private Frame currentFrame;

        public BowlingGame()
        {
            currentFrame = new Frame(0);
            frames = new List<Frame> { currentFrame };
        }

        public bool IsFinished => frames.Count == maxNumberOfFrames && frames.Last().IsComplete();

        /// <summary>
        /// Used to register how many pins that has been knocked down by a roll.
        /// Assigns the pins to the current frame and updates scores of frames
        /// </summary>
        /// <param name="pins">How many pins were knocked down by the roll</param>
        /// <returns>Returns the frame nummber of the roll</returns>
        public void AddRoll(int pins)
        {
            if (IsFinished) throw new Exception("Game is finished. No more rolling please");
            if (pins > 10 || pins < 0) throw new ArgumentException("Invalid value of roll");

            if (currentFrame.IsComplete())
            {
                StartNewFrame();
            }

            currentFrame.RegisterPins(pins);
            allRolls.Add(pins);
            UpdateScores();
        }

        public ReadOnlyCollection<Frame> GetScoreboard()
        {
            return frames.AsReadOnly();
        }

        private void UpdateScores()
        {
            // Calculate score of frames not yet calculated
            var framesToCalculate = frames.Where(x => x.Score == null);

            foreach (var frame in framesToCalculate)
            {
                var valueOfFrame = CalculateValueOfFrame(frame);

                if (valueOfFrame == null) break;

                totalScore += valueOfFrame.Value;
                frame.Score = totalScore;
            }
        }

        private int? CalculateValueOfFrame(Frame frame)
        {
            if (!frame.IsComplete()) return null;

            if (frame.IsStrike)
            {
                return CalculateStrikeValue(frame.IndexOfFirstRoll);
            }

            if (frame.IsSpare)
            {
                return CalculateSpareValue(frame.IndexOfFirstRoll);
            }

            return frame.Rolls.Sum(x => x);
        }

        private int? CalculateStrikeValue(int rollNumber)
        {
            if (rollNumber + 3 > allRolls.Count) return null; // Not able to calculate value of strike yet

            return 10 + allRolls.ElementAt(rollNumber + 1) + allRolls.ElementAt(rollNumber + 2); // Add value of 2 subsequent rolls to 10
        }

        private int? CalculateSpareValue(int rollNumber)
        {
            if (rollNumber + 3 > allRolls.Count) return null; // Not able to calculate value of spare yet

            return 10 + allRolls.ElementAt(rollNumber + 2); // Add Value of first roll of next frame to 10
        }

        private void StartNewFrame()
        {
            var isLastFrame = frames.Count + 1 == maxNumberOfFrames;

            currentFrame = isLastFrame ? new LastFrame(allRolls.Count) : new Frame(allRolls.Count);
            frames.Add(currentFrame);
        }
    }
}
