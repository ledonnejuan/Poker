namespace Don.Poker.Engine
{
    public class Player
    {
        #region Ctor
        public Player(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        } 
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Hand Hand { get; private set; } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Add Card To Player's Hand
        /// </summary>
        /// <param name="card">Don.Poker.Engine.Card</param>
        public void AddCardToHand(Card card)
        {
            if (Hand == null)
                Hand = new Hand();
            Hand.AddCard(card);
        }

        /// <summary>
        /// Clear Player's Hand
        /// </summary>
        public void ClearHand()
        {
            Hand = null;
        } 
        #endregion
    }
}
