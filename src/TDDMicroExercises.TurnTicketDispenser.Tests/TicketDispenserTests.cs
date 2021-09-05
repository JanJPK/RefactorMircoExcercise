using NUnit.Framework;

namespace TDDMicroExercises.TurnTicketDispenser.Tests
{
    [TestFixture]
    public class TicketDispenserTests
    {

        [Test]
        public void GetTurnTicket_Gives_Tickets_With_Sequential_Numbers()
        {
            var target = new TicketDispenser();

            var ticket1 = target.GetTurnTicket();
            var ticket2 = target.GetTurnTicket();

            Assert.Greater(ticket2.TurnNumber, ticket1.TurnNumber);
        }

        [Test]
        public void GetTurnTicket_Gives_Tickets_With_Sequential_Numbers_From_Multiple_Dispensers()
        {
            var target1 = new TicketDispenser();
            var target2 = new TicketDispenser();

            var ticket1 = target1.GetTurnTicket();
            var ticket2 = target2.GetTurnTicket();

            Assert.Greater(ticket2.TurnNumber, ticket1.TurnNumber);
        }
    }
}
