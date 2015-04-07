namespace FIERepro.Controllers
{
    using System.Collections.Generic;

    public class PumpkinsByOwnerId : IQuery<IList<Pumpkin>>
    {
        public int OwnerId { get; private set; }

        public PumpkinsByOwnerId(int ownerId)
        {
            OwnerId = ownerId;
        }
    }
}