using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Don.Poker.Engine.Rules
{
    public interface IRule
    {
        void EvaluateHand(Player player);
    }
}
