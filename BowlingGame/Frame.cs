﻿using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace BowlingGame
{
    public class Frame
    {
        public readonly int IndexOfFirstRoll;
        public int? Score { get; set; }
        public ReadOnlyCollection<int?> Rolls { get { return rolls.AsReadOnly(); } }
        public bool IsStrike => rolls.FirstOrDefault() == 10;
        public bool IsSpare => !IsStrike && rolls.ElementAtOrDefault(0) + rolls.ElementAtOrDefault(1) == 10;
        protected List<int?> rolls = new List<int?>();

        public Frame(int indexOfFirstRoll)
        {
            IndexOfFirstRoll = indexOfFirstRoll;
        }
        
        public virtual bool IsComplete()
        {
            return IsStrike || rolls.Count == 2;
        }

        public virtual void RegisterPins(int pins)
        {
            if (IsComplete()) throw new Exception("Cannot register pins in completed frame");
            if (rolls.Sum(x => x) + pins > 10) throw new ArgumentException("Number of pins in one frame cannot exceed 10");

            rolls.Add(pins);
        }
    }
}
