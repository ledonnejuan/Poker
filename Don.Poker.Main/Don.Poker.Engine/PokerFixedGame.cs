﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Don.Poker.Engine
{
    public class PokerFixedGame : PokerGame
    {
        public override void StartGame()
        {
            GameStarted = true;
        }
    }
}
