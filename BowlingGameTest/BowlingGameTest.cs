using NUnit.Framework;

namespace BowlingGameTest
{
    public class Tests
    {
        private BowlingGame.BowlingGame uut;

        [SetUp]
        public void Setup()
        {
            uut = new BowlingGame.BowlingGame();
        }

        [Test]
        public void NormalFrame_ValidFrame_ShouldReturnSumOfRolls()
        {

            uut.AddRoll(3);
            uut.AddRoll(4);

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.Last().Score, Is.EqualTo(7));
        }

        [Test]
        public void NormalFrame_TooManyPinsInOneRoll_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => uut.AddRoll(11));
        }

        [Test]
        public void NormalFrame_MoreThan10PinsInFrame_ShouldThrowException()
        {
            uut.AddRoll(5);

            Assert.Throws<ArgumentException>(() => uut.AddRoll(6));
        }

        [Test]
        public void SpareFrame_EnoughRollsToCalculateBonus_ShouldReturn10PlusNextRoll()
        {
            // Roll a spare in first frame
            uut.AddRoll(7);
            uut.AddRoll(3);

            uut.AddRoll(4);

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.First().Score, Is.EqualTo(14));
        }

        [Test]
        public void SpareFrame_NotEnoughRollsToCalculateBonus_ShouldReturnNull()
        {
            // Roll a spare in first frame
            uut.AddRoll(7);
            uut.AddRoll(3);

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.First().Score, Is.EqualTo(null));
        }

        [Test]
        public void StrikeFrame_EnoughRollsToCalculateBonus_ShouldReturn10PlusNext2Rolls()
        {
            // Roll a strike in first frame
            uut.AddRoll(10);

            uut.AddRoll(3);
            uut.AddRoll(4);

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.First().Score, Is.EqualTo(17));
        }

        [Test]
        public void StrikeFrame_MultipleStrikesInRow_ShouldReturn10PlusNext2Rolls()
        {
            // Roll 3 strikes in a row
            uut.AddRoll(10); // 30
            uut.AddRoll(10); // 23
            uut.AddRoll(10); // 17
            uut.AddRoll(3); 
            uut.AddRoll(4); // 7

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.ElementAt(0).Score, Is.EqualTo(30));
            Assert.That(scoreBoard.ElementAt(1).Score, Is.EqualTo(53));
            Assert.That(scoreBoard.ElementAt(2).Score, Is.EqualTo(70));
            Assert.That(scoreBoard.ElementAt(3).Score, Is.EqualTo(77));

        }

        [Test]
        public void StrikeFrame_NotEnoughRollsToCalculateBonus_ShouldReturnNull()
        {
            // Roll a strike in first frame and only one more roll.
            uut.AddRoll(10);
            uut.AddRoll(3);

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.First().Score, Is.EqualTo(null));
        }


        [Test]
        public void PerfectGame_ShouldReturn300()
        {
            for (int i = 0; i < 12; i++)
            {
                uut.AddRoll(10);
            }

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.Last().Score, Is.EqualTo(300));
            Assert.That(scoreBoard.Count(), Is.EqualTo(10)); //10 frames
        }

        [Test]
        public void GutterGame_ShouldReturn0()
        {
            for (int i = 0; i < 20; i++)
            {
                uut.AddRoll(0);
            }

            var scoreBoard = uut.GetScoreboard();

            Assert.That(scoreBoard.Last().Score, Is.EqualTo(0));
        }

        [Test]
        public void InvalidGame_TooManyRolls_ShouldThrow()
        {
            for (int i = 0; i < 20; i++)
            {
                uut.AddRoll(3);
            }

            Assert.Throws<InvalidOperationException>(() => uut.AddRoll(3));
        }
    }
}