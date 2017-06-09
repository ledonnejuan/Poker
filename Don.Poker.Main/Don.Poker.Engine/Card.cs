using Don.Poker.Engine.Infrastructure;
namespace Don.Poker.Engine
{
    public class Card
    {
        #region Ctor
        public Card(Suit suit, Face face)
        {
            this.Suit = suit;
            this.Face = face;
        } 
        #endregion

        #region Properties
        public Suit Suit { get; set; }
        public Face Face { get; set; } 
        #endregion
    }
}
