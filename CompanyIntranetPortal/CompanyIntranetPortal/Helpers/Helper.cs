using CompanyIntranetPortal.Core.Enums;

namespace CompanyIntranetPortal.Helpers
{
    public static class Helper
    {
        public static string GetState(ApplicationState applicationState)
        {
            var result = string.Empty;
            switch (applicationState) 
            {
                case ApplicationState.Open:
                    result = "bg-info";
                    break;
                case ApplicationState.Pending:
                    result = "bg-warning text-dark";
                    break;
                case ApplicationState.Approved:
                    result = "bg-success";
                    break;
                case ApplicationState.Rejected:
                    result = "bg-danger";
                    break;
            }

            return result;
        }

        public static string GetState(TicketStates ticketStates)
        {
            var result = string.Empty;
            switch (ticketStates)
            {
                case TicketStates.Open:
                    result = "bg-info";
                    break;
                case TicketStates.Pending:
                    result = "bg-warning text-dark";
                    break;
                case TicketStates.Done:
                    result = "bg-success";
                    break;
                case TicketStates.Rejected:
                    result = "bg-danger";
                    break;
            }

            return result;
        }
    }
}
